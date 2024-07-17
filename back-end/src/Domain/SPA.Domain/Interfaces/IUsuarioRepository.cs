using Microsoft.AspNetCore.Identity;
using SPA.Domain.Entities;

namespace SPA.Domain.Interfaces;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<IdentityResult> CreateAsync(Usuario usuario, string senha);
}