using Microsoft.IdentityModel.Tokens;
using SPA.Application.Interfaces;
using SPA.CrossCutting;
using SPA.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPA.Application.Services;

public class JwtAppService : IJwtAppService
{
    private readonly int _expirationDays = 1;

    public string GenerateJwtToken(Usuario usuario)
    {
        var claims = new Claim[]
        {
            new (ClaimTypes.Name, usuario.Name),
            new (ClaimTypes.NameIdentifier, usuario.UserName),
            new (ClaimTypes.Email, usuario.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvironmentVars.JwtSecret));
        var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expirationDate = DateTime.UtcNow.AddDays(_expirationDays);
        var token = new JwtSecurityToken(
            issuer: EnvironmentVars.JwtIssuer,
            audience: EnvironmentVars.JwtAudience,
            expires: expirationDate,
            claims: claims,
            signingCredentials: signInCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}