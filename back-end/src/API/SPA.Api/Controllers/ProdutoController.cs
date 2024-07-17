using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;

namespace SPA.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ProdutoController(IProdutoAppService _produtoAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> BuscarProdutosAsync()
    {
        var result = await _produtoAppService.BuscarProdutosAsync();
        if (!result.Succeeded)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    [HttpGet("{idProduto}")]
    public async Task<IActionResult> BuscarPorIdAsync(string idProduto)
    {
        if (string.IsNullOrWhiteSpace(idProduto) || !Guid.TryParse(idProduto, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do produto deve ser um GUID válido e não vazio.");

        var result = await _produtoAppService.BuscarPorIdAsync(idGuid);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return Ok(result.Data);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarAsync([FromBody] CreateUpdateProdutoDto createProdutoDto)
    {
        var result = await _produtoAppService.CadastrarAsync(createProdutoDto);
        if (!result.Succeeded)
            return BadRequest(result.ErrorMessage);

        return CreatedAtAction(nameof(BuscarPorIdAsync), new { idProduto = result.Data.Id }, result.Data);
    }

    [HttpPut("{idProduto}")]
    public async Task<IActionResult> AtualizarAsync(string idProduto, [FromBody] CreateUpdateProdutoDto updateProdutoDto)
    {
        if (string.IsNullOrWhiteSpace(idProduto) || !Guid.TryParse(idProduto, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do produto deve ser um GUID válido e não vazio.");

        var result = await _produtoAppService.AtualizarAsync(idGuid, updateProdutoDto);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return NoContent();
    }

    [HttpDelete("{idProduto}")]
    public async Task<IActionResult> ExcluirAsync(string idProduto)
    {
        if (string.IsNullOrWhiteSpace(idProduto) || !Guid.TryParse(idProduto, out var idGuid) || idGuid == Guid.Empty)
            return BadRequest("O ID do produto deve ser um GUID válido e não vazio.");

        var result = await _produtoAppService.ExcluirAsync(idGuid);
        if (!result.Succeeded)
            return NotFound(result.ErrorMessage);

        return NoContent();
    }
}