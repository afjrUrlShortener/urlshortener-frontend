namespace UrlShortener.Domain.Aggregates;

public interface IRepository<T> where T : IEntity
{
    Task<T> Create(T entity);
    Task<T> Get(Guid id);
    Task<T> Update(T entity);
    Task<bool> Delete(T entity);
}