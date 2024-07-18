namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public class UrlEntity : IEntity
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? DeletedAt { get; set; }
    public required string LongUrl { get; set; }
    public required string ShortUrl { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
}