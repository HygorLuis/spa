using SPA.Data.Context;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class ProdutoRepository(PostgresDbContext _postgresDbContext) : BaseRepository<Produto>(_postgresDbContext), IProdutoRepository
{
    public Produto? BuscarPorNome(string nome) => _postgresDbContext.Produtos.FirstOrDefault(p => p.Nome == nome);
}