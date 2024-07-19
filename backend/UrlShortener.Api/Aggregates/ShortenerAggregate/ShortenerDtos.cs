namespace UrlShortener.Api.Aggregates.ShortenerAggregate;

public record CreateShortenedUrlRequest(string Url, DateTimeOffset? ExpiresAt);

public record CreateShortenedUrlResponse(string Url);