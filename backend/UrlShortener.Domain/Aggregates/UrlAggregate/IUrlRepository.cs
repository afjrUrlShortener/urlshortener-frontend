namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public interface IUrlRepository : IRepository<UrlEntity>
{
    public Task<UrlEntity?> Get(string shortUrl);

    /// <summary>Soft deletes the entity from the database</summary>
    public Task<bool> Delete(string shortUrl);
}