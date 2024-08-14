namespace SPA.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> FindByIdAsync(Guid id);
    Task AddAsync(TEntity entity, string? value = null);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();
}