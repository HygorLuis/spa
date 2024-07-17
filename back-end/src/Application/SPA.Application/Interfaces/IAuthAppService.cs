using SPA.Application.Dtos;

namespace SPA.Application.Interfaces;

public interface IAuthAppService
{
    Task<ServiceResult<string>> AutenticarAsync(LoginDto loginDto);
}