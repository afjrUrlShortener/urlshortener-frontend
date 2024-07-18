namespace UrlShortener.Domain.Aggregates;

public interface IEntity
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? DeletedAt { get; set; }
};