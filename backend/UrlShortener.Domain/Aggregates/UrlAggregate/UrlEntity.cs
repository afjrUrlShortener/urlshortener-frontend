namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public class UrlEntity : IEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? DeletedAt { get; set; }
    public required string LongUrl { get; set; }
    public required string ShortUrl { get; set; }
    public DateTime? ExpiresAt { get; set; }
}