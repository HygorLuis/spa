using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;

namespace SPA.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class UsuarioController(IUsuarioAppService _usuarioAppService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarAsync([FromBody] CreateUsuarioDto createUsuarioDto)
    {
        var result = await _usuarioAppService.CadastrarAsync(createUsuarioDto);
        if (!result.Succeeded)
            return BadRequest(result.ErrorMessage);

        return CreatedAtAction(nameof(BuscarPorIdAsync), new { idUsuario = result.Data.Id }, result.Data);
    }

    [HttpGet("{idUsuario}")]
    public async Task<IActionResult> BuscarPorIdAsync(string idUsuario)
    {
        if (string.IsNullOrWhiteSpace(idUsuario) || !Guid.TryParse(idUsuario, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do usuário deve ser um GUID válido e não vazio.");

        var result = await _usuarioAppService.BuscarPorIdAsync(idGuid);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return Ok(result.Data);
    }
}
