using SPA.Application.Dtos;

namespace SPA.Application.Interfaces;

public interface IClienteAppService
{
    Task<ServiceResult<IEnumerable<ReadClienteDto>>> BuscarClientesAsync();
    Task<ServiceResult<ReadClienteDto>> BuscarPorIdAsync(Guid idUsuario);
    Task<ServiceResult<ReadClienteDto>> CadastrarAsync(CreateUpdateClienteDto createClienteDto);
    Task<ServiceResult<ReadClienteDto>> AtualizarAsync(Guid idCliente, CreateUpdateClienteDto updateClienteDto);
    Task<ServiceResult<ReadClienteDto>> ExcluirAsync(Guid idCliente);
}