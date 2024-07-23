using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using UrlShortener.Api.Aggregates.ShortenerAggregate;
using UrlShortener.Domain.Aggregates.ShortenerAggregate;
using UrlShortener.Domain.Aggregates.UrlAggregate;

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
    public Task AccessShortUrl_ShouldRedirectToLongUrl(string url)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            using var response = await ShortenerHelper.CreateShortUrl(httpClient, url);
            var content = await ShortenerHelper.ReadFromShortUrlResponse(response);

            // act
            using var redirectResponse = await ShortenerHelper.GetRedirectResponse(httpClient, content!.ShortUrl);

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

    [Theory]
    [InlineData("https://www.bing.com.br")]
    [InlineData("https://www.mercadolivre.com.br/products/computers?cpu=amd&graphicscard=nvidia")]
    [InlineData("http://www.uber.com")]
    [InlineData("ftp://br-south.s3.amazon/bucket/5/332")]
    public Task AccessShortUrl_ShouldReturnNotFound(string url)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (services, httpClient) =>
        {
            using var createResponse = await ShortenerHelper.CreateShortUrl(httpClient, url);
            var content = await ShortenerHelper.ReadFromShortUrlResponse(createResponse);
            var urlRepository = services.CreateScope().ServiceProvider.GetRequiredService<IUrlRepository>();
            await urlRepository.Delete(content!.ShortUrl);

            // act
            using var response = await ShortenerHelper.GetRedirectResponse(httpClient, content.ShortUrl);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        });
    }

    [Fact]
    public Task AccessShortUrl_WhenShortUrlIsBiggerThanMaxSize_ShouldReturnBadRequest()
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            var biggerShortUrl = "".PadRight(ShortenerConstants.ShortUrlMaxSize + 1, 'x');

            // act
            using var response = await ShortenerHelper.GetRedirectResponse(httpClient, biggerShortUrl);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public Task AccessShortUrl_WhenShortUrlIsNullOrEmpty_ShouldReturnMethodNotAllowed(string? shortUrl)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            // act
            using var response = await ShortenerHelper.GetRedirectResponse(httpClient, shortUrl);

            // assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        });
    }

    [Fact]
    public Task AccessShortUrl_WhenShortUrlIsEmptyAndUrlEncoded_ShouldReturnBadRequest()
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            var encodedShortUrl = WebUtility.UrlEncode(" ");

            // act
            using var response = await ShortenerHelper.GetRedirectResponse(httpClient, encodedShortUrl);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        });
    }

    [Theory]
    [InlineData("@XSds")]
    [InlineData("$23&5")]
    [InlineData(" 11 4")]
    public Task AccessShortUrl_WhenShortUrlIsComposedByInvalidCharacters_ShouldReturnBadRequest(string shortUrl)
    {
        // arrange
        return ShortenerHelper.CreateWebApiAndExecute(_fixture.SqlDatabase, async (_, httpClient) =>
        {
            // act
            using var response = await ShortenerHelper.GetRedirectResponse(httpClient, shortUrl);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        });
    }

    //TODO: Add more tests
}