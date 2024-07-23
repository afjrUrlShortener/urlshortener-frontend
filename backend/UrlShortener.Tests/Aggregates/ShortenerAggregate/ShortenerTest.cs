using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using UrlShortener.Api.Aggregates.ShortenerAggregate;

namespace UrlShortener.Tests.Aggregates.ShortenerAggregate;

public class ShortenerTest : IClassFixture<CoreFixture>
{
    private readonly CoreFixture _fixture;
    private const string TestUrl = "https://www.google.com.br";

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
            var (response, content) = await ShortenerHelper.CreateShortUrl(httpClient, TestUrl);

            // assert
            Assert.NotNull(response);
            Assert.NotNull(content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.False(string.IsNullOrWhiteSpace(content.ShortUrl));
            Assert.True(response.Headers.TryGetValues(HeaderNames.Location, out var redirectToEnum));
            var redirectTo = redirectToEnum.SingleOrDefault();
            Assert.False(string.IsNullOrWhiteSpace(redirectTo));
            Assert.StartsWith(shortenerOptions.ShortenerDomain, redirectTo);
        });
    }

    [Fact]
    public Task CreateShortUrl_ShouldRedirectToLongUrl()
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            var (_, content) = await ShortenerHelper.CreateShortUrl(httpClient, TestUrl);

            // act
            using var redirectResponse = await httpClient.GetAsync($"api/v1/shortener/{content?.ShortUrl}");

            // assert
            Assert.Equal(HttpStatusCode.PermanentRedirect, redirectResponse.StatusCode);
            Assert.True(redirectResponse.Headers.TryGetValues(HeaderNames.Location, out var redirectTo));
            Assert.False(string.IsNullOrWhiteSpace(redirectTo.SingleOrDefault()));
        });
    }

    //TODO: Add more tests
}