namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public class UrlEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string LongUrl { get; set; }
    public required string ShortUrl { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}