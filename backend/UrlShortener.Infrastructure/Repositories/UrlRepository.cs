using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.Infrastructure.Repositories;

public class UrlRepository : IUrlRepository
{
    public Task<UrlEntity> Create()
    {
        throw new NotImplementedException();
    }
}