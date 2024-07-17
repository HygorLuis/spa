namespace SPA.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int QuantidadeEstoque { get; set; }
    public decimal ValorCusto { get; set; }
    public decimal ValorVenda { get; set; }
    public string? Observacao { get; set; }
    public DateOnly DataCadastro { get; set; }
}