using Microsoft.AspNetCore.Identity;
using SPA.Domain.Entities;

namespace SPA.Domain.Interfaces;

public interface IUsuarioService
{
    Task<IdentityResult> CadastrarAsync(Usuario usuario, string senha);
    Task<Usuario?> BuscarPorIdAsync(Guid idUsuario);
}