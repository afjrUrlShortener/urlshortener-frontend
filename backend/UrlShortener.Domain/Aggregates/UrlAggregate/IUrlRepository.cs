namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public interface IUrlRepository : IRepository<UrlEntity>
{
    public Task<UrlEntity> Get(string shortUrl);
}