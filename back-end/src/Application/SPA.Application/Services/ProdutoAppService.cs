using AutoMapper;
using Serilog;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Application.Services;

public class ProdutoAppService(IMapper _mapper, IProdutoService _produtoService) : IProdutoAppService
{
    public async Task<ServiceResult<IEnumerable<ReadProdutoDto>>> BuscarProdutosAsync()
    {
        try
        {
            var produtos = await _produtoService.BuscarProdutosAsync();
            return ServiceResult<IEnumerable<ReadProdutoDto>>.Success(_mapper.Map<IEnumerable<ReadProdutoDto>>(produtos));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao buscar produtos: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao buscar produtos.");
        }
    }

    public async Task<ServiceResult<ReadProdutoDto>> BuscarPorIdAsync(Guid idProduto)
    {
        try
        {
            var produto = await _produtoService.BuscarPorIdAsync(idProduto);
            if (produto == null)
                return ServiceResult<ReadProdutoDto>.Failure("Produto não encontrado!");

            return ServiceResult<ReadProdutoDto>.Success(_mapper.Map<ReadProdutoDto>(produto));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao buscar produto: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao buscar produto.");
        }
    }

    public async Task<ServiceResult<ReadProdutoDto>> CadastrarAsync(CreateUpdateProdutoDto createProdutoDto)
    {
        try
        {
            var produto = _produtoService.BuscarPorNome(createProdutoDto.Nome);
            if (produto != null)
                return ServiceResult<ReadProdutoDto>.Failure($"Produto já cadastrado!");

            produto = _mapper.Map<Produto>(createProdutoDto);
            var produtoCadastrado = await _produtoService.CadastrarAsync(produto);

            return ServiceResult<ReadProdutoDto>.Success(_mapper.Map<ReadProdutoDto>(produtoCadastrado));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao cadastrar produto: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao cadastrar produto.");
        }
    }

    public async Task<ServiceResult<ReadProdutoDto>> AtualizarAsync(Guid idProduto, CreateUpdateProdutoDto updateProdutoDto)
    {
        try
        {
            var produto = await _produtoService.BuscarPorIdAsync(idProduto);
            if (produto == null)
                return ServiceResult<ReadProdutoDto>.Failure("Produto não encontrado!");

            _mapper.Map(updateProdutoDto, produto);
            await _produtoService.AtualizarAsync(produto);

            return ServiceResult<ReadProdutoDto>.Success();
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao atualizar produto: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao atualizar produto.");
        }
    }

    public async Task<ServiceResult<ReadProdutoDto>> ExcluirAsync(Guid idProduto)
    {
        try
        {
            var cliente = await _produtoService.BuscarPorIdAsync(idProduto);
            if (cliente == null)
                return ServiceResult<ReadProdutoDto>.Failure("Produto não encontrado!");

            await _produtoService.ExcluirAsync(cliente);
            return ServiceResult<ReadProdutoDto>.Success();
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao excluir produto: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao excluir produto.");
        }
    }
}