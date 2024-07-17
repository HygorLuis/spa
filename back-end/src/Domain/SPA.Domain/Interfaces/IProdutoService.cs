using SPA.Domain.Entities;

namespace SPA.Domain.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> BuscarProdutosAsync();
    Task<Produto?> BuscarPorIdAsync(Guid idProduto);
    Task<Produto> CadastrarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task ExcluirAsync(Produto produto);
    Produto? BuscarPorNome(string nome);
}