namespace UrlShortener.Api.Aggregates.ShortenerAggregate;

public interface IShortenerService
{
    public Task<string?> CreateShortUrl(string longUrl, DateTime? expiresAt = null);
    public Task<string?> GetLongUrl(string shortUrl);
}