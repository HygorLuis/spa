using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Domain.Services;

public class ProdutoService(IProdutoRepository _produtoRepository) : IProdutoService
{
    public async Task<IEnumerable<Produto>> BuscarProdutosAsync() => await _produtoRepository.GetAllAsync();

    public async Task<Produto?> BuscarPorIdAsync(Guid idProduto) => await _produtoRepository.FindByIdAsync(idProduto);

    public Produto? BuscarPorNome(string nome) => _produtoRepository.BuscarPorNome(nome);

    public async Task<Produto> CadastrarAsync(Produto produto)
    {
        produto.DataCadastro = DateTime.UtcNow;

        await _produtoRepository.AddAsync(produto);
        await _produtoRepository.SaveChangesAsync();

        return produto;
    }

    public async Task AtualizarAsync(Produto produto)
    {
        _produtoRepository.Update(produto);
        await _produtoRepository.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Produto produto)
    {
        _produtoRepository.Delete(produto);
        await _produtoRepository.SaveChangesAsync();
    }
}