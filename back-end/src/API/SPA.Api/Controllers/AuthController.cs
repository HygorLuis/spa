using Microsoft.AspNetCore.Mvc;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;

namespace SPA.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

[ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
public class AuthController(IAuthAppService _authAppService) : ControllerBase
{
    [HttpPost("GerarToken")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GerarTokenAsync([FromBody] LoginDto loginDto)
    {
        var result = await _authAppService.AutenticarAsync(loginDto);
        if (!result.Succeeded)
            return Unauthorized(result.ErrorMessage);

        return Ok(result.Data);
    }
}