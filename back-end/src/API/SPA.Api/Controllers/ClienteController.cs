using Microsoft.AspNetCore.Mvc;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;

namespace SPA.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ClienteController(IClienteAppService _clienteAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> BuscarClientesAsync()
    {
        var result = await _clienteAppService.BuscarClientesAsync();
        if (!result.Succeeded)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    [HttpGet("{idCliente}")]
    public async Task<IActionResult> BuscarPorIdAsync(string idCliente)
    {
        if (string.IsNullOrWhiteSpace(idCliente) || !Guid.TryParse(idCliente, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do cliente deve ser um GUID válido e não vazio.");

        var result = await _clienteAppService.BuscarPorIdAsync(idGuid);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return Ok(result.Data);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarAsync([FromBody] CreateUpdateClienteDto createClienteDto)
    {
        var result = await _clienteAppService.CadastrarAsync(createClienteDto);
        if (!result.Succeeded)
            return BadRequest(result.ErrorMessage);

        return CreatedAtAction(nameof(BuscarPorIdAsync), new { idCliente = result.Data.Id }, result.Data);
    }

    [HttpPut("{idCliente}")]
    public async Task<IActionResult> AtualizarAsync(string idCliente, [FromBody] CreateUpdateClienteDto updateClienteDto)
    {
        if (string.IsNullOrWhiteSpace(idCliente) || !Guid.TryParse(idCliente, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do cliente deve ser um GUID válido e não vazio.");

        var result = await _clienteAppService.AtualizarAsync(idGuid, updateClienteDto);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return NoContent();
    }

    [HttpDelete("{idCliente}")]
    public async Task<IActionResult> ExcluirAsync(string idCliente)
    {
        if (string.IsNullOrWhiteSpace(idCliente) || !Guid.TryParse(idCliente, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do cliente deve ser um GUID válido e não vazio.");

        var result = await _clienteAppService.ExcluirAsync(idGuid);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return NoContent();
    }
}