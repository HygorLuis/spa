using Microsoft.AspNetCore.Identity;
using Serilog;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;
using SPA.Domain.Entities;

namespace SPA.Application.Services;

public class AuthAppService(UserManager<Usuario> _userManager, IJwtAppService _jwtService) : IAuthAppService
{
    public async Task<ServiceResult<string>> AutenticarAsync(LoginDto loginDto)
    {
        try
        {
            var usuario = await _userManager.FindByNameAsync(loginDto.Usuario);
            if (usuario == null)
                return ServiceResult<string>.Failure("Usuário não encontrado!");

            if (usuario.InactivationDate.HasValue)
                return ServiceResult<string>.Failure("Usuário inativo!");

            if (!await _userManager.CheckPasswordAsync(usuario, loginDto.Senha))
                return ServiceResult<string>.Failure("Senha incorreta!");

            return ServiceResult<string>.Success(_jwtService.GenerateJwtToken(usuario));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao autenticar usuário: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao autenticar usuário.");
        }
    }
}