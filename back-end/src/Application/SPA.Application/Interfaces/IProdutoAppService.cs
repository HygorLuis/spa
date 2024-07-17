using SPA.Application.Dtos;

namespace SPA.Application.Interfaces;

public interface IProdutoAppService
{
    Task<ServiceResult<IEnumerable<ReadProdutoDto>>> BuscarProdutosAsync();
    Task<ServiceResult<ReadProdutoDto>> BuscarPorIdAsync(Guid idProduto);
    Task<ServiceResult<ReadProdutoDto>> CadastrarAsync(CreateUpdateProdutoDto createProdutoDto);
    Task<ServiceResult<ReadProdutoDto>> AtualizarAsync(Guid idProduto, CreateUpdateProdutoDto updateProdutoDto);
    Task<ServiceResult<ReadProdutoDto>> ExcluirAsync(Guid idProduto);
}