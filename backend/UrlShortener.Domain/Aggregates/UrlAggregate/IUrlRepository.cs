namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public interface IUrlRepository : IRepository<UrlEntity>
{
    Task<UrlEntity> Create();
}