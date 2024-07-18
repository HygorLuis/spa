using Microsoft.EntityFrameworkCore;
using SPA.Data.Context;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class BaseRepository<T>(PostgresDbContext _postgresDbContext) : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = _postgresDbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public virtual async Task<T?> FindByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    public virtual async Task AddAsync(T entity, string? value = null) => await _dbSet.AddAsync(entity);
    public virtual void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
    public async Task SaveChangesAsync() => await _postgresDbContext.SaveChangesAsync();
}