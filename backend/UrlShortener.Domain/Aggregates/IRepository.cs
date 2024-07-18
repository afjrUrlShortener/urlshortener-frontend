namespace UrlShortener.Domain.Aggregates;

public interface IRepository<T> where T : IEntity
{
    Task<T?> Create(T entity);
    ValueTask<T?> Get(Guid id);
    Task<T?> Update(T entity);

    /// <summary>Hard deletes the entity from the database</summary>
    Task<bool> Delete(T entity);
}