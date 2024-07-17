namespace SPA.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> FindByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    void Delete(T entity);
}