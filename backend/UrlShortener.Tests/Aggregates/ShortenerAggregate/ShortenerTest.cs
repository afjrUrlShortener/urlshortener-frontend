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

    [Theory]
    [InlineData("https://www.google.com.br")]
    [InlineData("ftp://us-east.s3.amazon/bucket/1/123456")]
    public Task CreateShortUrl_ShouldReturnCreatedStatus_AndLocationInHeader_ShouldStartWithShortenerDomain(string url)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (services, httpClient) =>
        {
            var shortenerOptions = services.GetRequiredService<IOptions<ShortenerOptions>>().Value;

            // act
            using var response = await ShortenerHelper.CreateShortUrl(httpClient, url);
            var content = await ShortenerHelper.ReadFromShortUrlResponse(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(content);
            Assert.False(string.IsNullOrWhiteSpace(content.ShortUrl));
            Assert.True(response.Headers.TryGetValues(HeaderNames.Location, out var headerLocationValue));
            var urlToRedirect = headerLocationValue.SingleOrDefault();
            Assert.False(string.IsNullOrWhiteSpace(urlToRedirect));
            Assert.StartsWith(shortenerOptions.ShortenerDomain, urlToRedirect);
        });
    }

    [Theory]
    [InlineData("https://www.google.com.br")]
    [InlineData("https://www.amazon.com.br/products/computers?cpu=amd&graphicscard=nvidia")]
    [InlineData("http://www.microsoft.com")]
    [InlineData("ftp://us-east.s3.amazon/bucket/1/123456")]
    public Task CreateShortUrl_ShouldRedirectToLongUrl(string url)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            using var response = await ShortenerHelper.CreateShortUrl(httpClient, url);
            var content = await ShortenerHelper.ReadFromShortUrlResponse(response);

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

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("www.google.com.br")]
    [InlineData(".google.com.br")]
    [InlineData("google.com.br")]
    [InlineData("/www.amazon.com.br/products/computers?cpu=amd")]
    [InlineData("://www.amazon.com.br/products/computers?cpu=amd&graphicscard=nvidia")]
    [InlineData(":/www.amazon.com.br/products/computers?cpu=amd&graphicscard=nvidia")]
    public Task CreateShortUrl_ShouldReturnBadRequest(string url)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            // act
            using var response = await ShortenerHelper.CreateShortUrl(httpClient, url);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        });
    }

    //TODO: Add more tests
}