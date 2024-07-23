using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.Infrastructure.Aggregates.UrlAggregate;

public class UrlRepository : IUrlRepository
{
    private readonly UrlContext _dbContext;

    public UrlRepository(UrlContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UrlEntity?> Create(UrlEntity entity)
    {
        var createdEntity = await _dbContext.Urls.AddAsync(entity);
        var totalChanges = await _dbContext.SaveChangesAsync();
        if (totalChanges == 0)
            return null;

        return createdEntity.Entity;
    }

    public ValueTask<UrlEntity?> Get(Guid id) => _dbContext.Urls.FindAsync(id);

    public async Task<UrlEntity?> Update(UrlEntity entity)
    {
        var updatedEntity = _dbContext.Urls.Update(entity);
        var totalChanges = await _dbContext.SaveChangesAsync();
        if (totalChanges == 0)
            return null;

        return updatedEntity.Entity;
    }

    public async Task<bool> Delete(UrlEntity entity)
    {
        _dbContext.Urls.Remove(entity);
        var totalChanges = await _dbContext.SaveChangesAsync();
        return totalChanges == 1;
    }

    public Task<UrlEntity?> GetByLongUrl(string longUrl) => _dbContext.Urls.FirstOrDefaultAsync(
        x => x.LongUrl == longUrl && x.DeletedAt == null
    );

    public Task<UrlEntity?> GetByShortUrl(string shortUrl) => _dbContext.Urls.FirstOrDefaultAsync(
        x => x.ShortUrl == shortUrl && x.DeletedAt == null
    );

    public async Task<bool> Delete(string shortUrl)
    {
        var entity = await GetByShortUrl(shortUrl);
        if (entity is null)
            return false;

        entity.DeletedAt = DateTime.UtcNow;
        var updatedEntity = await Update(entity);
        return updatedEntity is not null;
    }
}