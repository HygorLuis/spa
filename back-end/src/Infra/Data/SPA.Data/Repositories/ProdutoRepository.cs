using SPA.Data.Context;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class ProdutoRepository(IApplicationDbContext _dbContext) : BaseRepository<Produto>(_dbContext), IProdutoRepository
{
    public Produto? BuscarPorNome(string nome) => _dbContext.Produtos.FirstOrDefault(p => p.Nome == nome);
}