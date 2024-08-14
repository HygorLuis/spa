using Microsoft.EntityFrameworkCore;
using SPA.Domain.Entities;

namespace SPA.Data.Context;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task SaveChangesAsync();

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
}