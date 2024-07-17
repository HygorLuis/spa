using Microsoft.AspNetCore.Identity;
using SPA.Data.Context;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class UsuarioRepository(PostgresDbContext _postgresDbContext, UserManager<Usuario> _userManager) : BaseRepository<Usuario>(_postgresDbContext), IUsuarioRepository
{
    public async Task<IdentityResult> CreateAsync(Usuario usuario, string senha)
    {
        usuario.RegistrationDate = DateTime.UtcNow;
        return await _userManager.CreateAsync(usuario, senha);
    }

    public override async Task<Usuario?> FindByIdAsync(Guid idUsuario) => await _userManager.FindByIdAsync(idUsuario.ToString());
}