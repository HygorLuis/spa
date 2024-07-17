using Microsoft.EntityFrameworkCore;
using SPA.Data.Context;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class BaseRepository<T>(PostgresDbContext postgresDbContext) : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = postgresDbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public virtual async Task<T?> FindByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public virtual async Task UpdateAsync(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
}