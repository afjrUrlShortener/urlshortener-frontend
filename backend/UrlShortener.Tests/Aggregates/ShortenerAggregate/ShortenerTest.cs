using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using UrlShortener.Api.Aggregates.ShortenerAggregate;

namespace UrlShortener.Tests.Aggregates.ShortenerAggregate;

public class ShortenerTest : IClassFixture<CoreFixture>
{
    private readonly CoreFixture _fixture;

    public ShortenerTest(CoreFixture coreFixture)
    {
        _fixture = coreFixture;
    }

    [Fact]
    public Task CreateShortUrl_ShouldReturnCreatedStatus_AndContainShortenerDomainInHeader()
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (services, httpClient) =>
        {
            var shortenerOptions = services.GetRequiredService<IOptions<ShortenerOptions>>().Value;

            // act
            var (response, content) = await ShortenerHelper.CreateShortUrl(httpClient, "https://www.google.com.br");

            // assert
            Assert.NotNull(response);
            Assert.NotNull(content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.False(string.IsNullOrWhiteSpace(content.ShortUrl));
            Assert.True(response.Headers.TryGetValues(HeaderNames.Location, out var headerLocationValue));
            var urlToRedirect = headerLocationValue.SingleOrDefault();
            Assert.False(string.IsNullOrWhiteSpace(urlToRedirect));
            Assert.StartsWith(shortenerOptions.ShortenerDomain, urlToRedirect);
        });
    }

    [Theory]
    [InlineData("https://www.google.com.br")]
    [InlineData("https://www.amazon.com.br")]
    [InlineData("https://www.microsoft.com")]
    public Task CreateShortUrl_ShouldRedirectToLongUrl(string url)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            var (_, content) = await ShortenerHelper.CreateShortUrl(httpClient, url);

            // act
            using var redirectResponse = await httpClient.GetAsync($"api/v1/shortener/{content?.ShortUrl}");

            // assert
            Assert.Equal(HttpStatusCode.PermanentRedirect, redirectResponse.StatusCode);
            Assert.True(redirectResponse.Headers.TryGetValues(HeaderNames.Location, out var headerLocationValue));
            var urlToRedirect = headerLocationValue.SingleOrDefault();
            Assert.False(string.IsNullOrWhiteSpace(urlToRedirect));
            Assert.StartsWith(url, urlToRedirect);
        });
    }

    //TODO: Add more tests
}