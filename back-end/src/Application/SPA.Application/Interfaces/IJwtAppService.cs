using SPA.Domain.Entities;

namespace SPA.Application.Interfaces;

public interface IJwtAppService
{
    string GenerateJwtToken(Usuario usuario);
}