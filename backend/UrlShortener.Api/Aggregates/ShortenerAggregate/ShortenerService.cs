using UrlShortener.Domain.Aggregates.ShortenerAggregate;
using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.Api.Aggregates.ShortenerAggregate;

public class ShortenerService : IShortenerService
{
    private readonly ILogger<ShortenerService> _logger;
    private readonly IUrlRepository _urlRepository;

    public ShortenerService(
        ILogger<ShortenerService> logger,
        IUrlRepository urlRepository
    )
    {
        _logger = logger;
        _urlRepository = urlRepository;
    }

    private string CreateRandomShortUrl(Random random)
    {
        return new string(random.GetItems(ShortenerConstants.CharacterSet, ShortenerConstants.ShortUrlMaxSize));
    }

    public async Task<string?> CreateShortUrl(string longUrl, DateTime? expiresAt = null)
    {
        var urlEntityByLongUrl = await _urlRepository.GetByLongUrl(longUrl);
        var utcNow = DateTime.UtcNow;
        var renewedTime = utcNow.AddDays(ShortenerConstants.RenewalDays);
        if (urlEntityByLongUrl is not null)
        {
            if (urlEntityByLongUrl.ExpiresAt < utcNow)
                urlEntityByLongUrl.ExpiresAt = renewedTime;

            return urlEntityByLongUrl.ShortUrl;
        }

        var random = new Random();
        var shortUrl = CreateRandomShortUrl(random);
        var urlEntityByShortUrl = await _urlRepository.GetByShortUrl(shortUrl);
        for (var i = 1; urlEntityByShortUrl is not null && i <= 5; i++)
        {
            _logger.LogWarning(
                "Found a short url in database while creating a new one, trying again... {attempts}",
                new { Attempts = i }
            );
            shortUrl = CreateRandomShortUrl(random);
            urlEntityByShortUrl = await _urlRepository.GetByShortUrl(shortUrl);
        }

        if (expiresAt > renewedTime)
            expiresAt = renewedTime;
        else if (expiresAt < utcNow)
            expiresAt = renewedTime;

        var createdUrlEntity = await _urlRepository.Create(new UrlEntity
        {
            CreatedAt = utcNow,
            LongUrl = longUrl,
            ShortUrl = shortUrl,
            ExpiresAt = expiresAt ?? renewedTime
        });
        if (createdUrlEntity is null)
        {
            _logger.LogError(
                "Failed to create shortened url {errObj}",
                new { LongUrl = longUrl, ExpiresAt = expiresAt }
            );
            return null;
        }

        return createdUrlEntity.ShortUrl;
    }

    public async Task<string?> GetLongUrl(string shortUrl)
    {
        var urlEntity = await _urlRepository.GetByShortUrl(shortUrl);
        if (urlEntity is null || urlEntity.ExpiresAt < DateTime.UtcNow)
            return null;

        return urlEntity.LongUrl;
    }
}