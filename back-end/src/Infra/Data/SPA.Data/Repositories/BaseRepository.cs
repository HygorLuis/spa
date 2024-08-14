using Microsoft.EntityFrameworkCore;
using SPA.Data.Context;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class BaseRepository<TEntity>(IApplicationDbContext _dbContext) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = _dbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public virtual async Task<TEntity?> FindByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    public virtual async Task AddAsync(TEntity entity, string? value = null) => await _dbSet.AddAsync(entity);
    public virtual void Update(TEntity entity) => _dbSet.Update(entity);
    public void Delete(TEntity entity) => _dbSet.Remove(entity);
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}