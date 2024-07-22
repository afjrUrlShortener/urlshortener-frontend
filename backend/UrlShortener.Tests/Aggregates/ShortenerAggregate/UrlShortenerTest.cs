using System.Net;
using Microsoft.Net.Http.Headers;

namespace UrlShortener.Tests.Aggregates.ShortenerAggregate;

public class UrlShortenerTest : IClassFixture<CoreFixture>
{
    private readonly CoreFixture _fixture;
    private const string TestUrl = "https://www.google.com.br";

    public UrlShortenerTest(CoreFixture coreFixture)
    {
        _fixture = coreFixture;
    }

    [Fact]
    public Task ShouldCreateShortUrlAndReturnCreatedWithShortenerDomainInHeader()
    {
        return UrlShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async httpClient =>
        {
            var (response, content) = await UrlShortenerHelper.CreateShortUrl(httpClient, TestUrl);
            Assert.NotNull(response);
            Assert.NotNull(content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.False(string.IsNullOrWhiteSpace(content.ShortUrl));

            Assert.True(response.Headers.TryGetValues(HeaderNames.Location, out var redirectToEnum));
            var redirectTo = redirectToEnum.SingleOrDefault();
            Assert.False(string.IsNullOrWhiteSpace(redirectTo));
            Assert.True(redirectTo.StartsWith("http") || redirectTo.StartsWith("https"));
        });
    }

    [Fact]
    public Task ShouldCreateShortUrlAndRedirectToLongUrl()
    {
        return UrlShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async httpClient =>
        {
            var (_, content) = await UrlShortenerHelper.CreateShortUrl(httpClient, TestUrl);
            using var redirectResponse = await httpClient.GetAsync($"api/v1/shortener/{content?.ShortUrl}");
            Assert.Equal(HttpStatusCode.PermanentRedirect, redirectResponse.StatusCode);

            Assert.True(redirectResponse.Headers.TryGetValues(HeaderNames.Location, out var redirectTo));
            Assert.False(string.IsNullOrWhiteSpace(redirectTo.SingleOrDefault()));
        });
    }

    //TODO: Add more tests
}