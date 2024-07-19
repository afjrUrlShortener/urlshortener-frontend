namespace UrlShortener.Domain.Aggregates;

public interface IEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? DeletedAt { get; set; }
};