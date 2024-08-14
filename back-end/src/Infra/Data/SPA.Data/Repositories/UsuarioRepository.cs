using Microsoft.AspNetCore.Identity;
using SPA.Data.Context;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class UsuarioRepository(IApplicationDbContext _dbContext, UserManager<Usuario> _userManager) : BaseRepository<Usuario>(_dbContext), IUsuarioRepository
{
    public override async Task<IdentityResult> AddAsync(Usuario usuario, string senha) => await _userManager.CreateAsync(usuario, senha);

    public override async Task<Usuario?> FindByIdAsync(Guid idUsuario) => await _userManager.FindByIdAsync(idUsuario.ToString());
}