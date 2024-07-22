namespace UrlShortener.Api.Aggregates.ShortenerAggregate;

public record CreateShortenedUrlRequest(string Url, DateTimeOffset? ExpiresAt = null);

public record CreateShortenedUrlResponse(string ShortUrl);