using SPA.Application.Dtos;

namespace SPA.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<ServiceResult<ReadUsuarioDto>> CadastrarAsync(CreateUsuarioDto createUsuarioDto);
    Task<ServiceResult<ReadUsuarioDto>> BuscarPorIdAsync(Guid idUsuario);
}