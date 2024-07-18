using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.Infrastructure.Repositories;

public class UrlRepository : IUrlRepository
{
    public Task<UrlEntity> Create(UrlEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<UrlEntity> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UrlEntity> Update(UrlEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(UrlEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<UrlEntity> Get(string shortUrl)
    {
        throw new NotImplementedException();
    }
}