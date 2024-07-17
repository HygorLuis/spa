using System.ComponentModel.DataAnnotations;

namespace SPA.Application.Dtos;

public class CreateUpdateProdutoDto
{
    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser menor que zero.")]
    public int QuantidadeEstoque { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O valor de custo não pode ser menor que zero.")]
    public decimal ValorCusto { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O valor de venda não pode ser menor que zero.")]
    public decimal ValorVenda { get; set; }

    public string? Observacao { get; set; }
}