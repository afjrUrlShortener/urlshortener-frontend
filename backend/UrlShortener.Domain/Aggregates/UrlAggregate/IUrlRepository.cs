namespace UrlShortener.Domain.Aggregates.UrlAggregate;

public interface IUrlRepository : IRepository<UrlEntity>
{
    public Task<UrlEntity?> GetByLongUrl(string longUrl);
    public Task<UrlEntity?> GetByShortUrl(string shortUrl);

    /// <summary>Soft deletes the entity from the database</summary>
    public Task<bool> Delete(string shortUrl);
}