using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;

namespace SPA.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

[ProducesResponseType<string>(StatusCodes.Status401Unauthorized)]
[ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
public class ProdutoController(IProdutoAppService _produtoAppService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<ReadProdutoDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscarProdutosAsync()
    {
        var result = await _produtoAppService.BuscarProdutosAsync();
        if (!result.Succeeded)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    [HttpGet("{idProduto}")]
    [ProducesResponseType<ReadProdutoDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
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