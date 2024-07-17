using AutoMapper;
using Serilog;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Application.Services;

public class UsuarioAppService(IMapper _mapper, IUsuarioService _usuarioService) : IUsuarioAppService
{
    public async Task<ServiceResult<ReadUsuarioDto>> CadastrarAsync(CreateUsuarioDto createUsuarioDto)
    {
        try
        {
            var usuario = _mapper.Map<Usuario>(createUsuarioDto);
            var result = await _usuarioService.CadastrarAsync(usuario, createUsuarioDto.Senha);
            if (!result.Succeeded)
            {
                var messages = result.Errors.Select(e => $"{e.Code}: {e.Description}");
                return ServiceResult<ReadUsuarioDto>.Failure(string.Join("\n", messages));
            }

            return ServiceResult<ReadUsuarioDto>.Success(_mapper.Map<ReadUsuarioDto>(usuario));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao cadastrar usuário: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao cadastrar usuário.");
        }
    }

    public async Task<ServiceResult<ReadUsuarioDto>> BuscarPorIdAsync(Guid idUsuario)
    {
        try
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(idUsuario);
            if (usuario == null)
                return ServiceResult<ReadUsuarioDto>.Failure("Usuário não encontrado!");

            return ServiceResult<ReadUsuarioDto>.Success(_mapper.Map<ReadUsuarioDto>(usuario));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao buscar usuário: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao buscar usuário.");
        }
    }
}