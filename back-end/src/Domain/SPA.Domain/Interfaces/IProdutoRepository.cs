using SPA.Domain.Entities;

namespace SPA.Domain.Interfaces;
public interface IProdutoRepository :IBaseRepository<Produto>
{
    Produto? BuscarPorNome(string nome);
}