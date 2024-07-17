namespace SPA.Application.Dtos;

public class ReadClienteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public string? Observacao { get; set; }
}