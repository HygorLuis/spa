using Microsoft.AspNetCore.Identity;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Domain.Services;

public class UsuarioService(IUsuarioRepository _usuarioRepository) : IUsuarioService
{
    public async Task<IdentityResult> CadastrarAsync(Usuario usuario, string senha) => await _usuarioRepository.CreateAsync(usuario, senha);
    public async Task<Usuario?> BuscarPorIdAsync(Guid idUsuario) => await _usuarioRepository.FindByIdAsync(idUsuario);
}