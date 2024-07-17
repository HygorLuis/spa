using System.ComponentModel.DataAnnotations;

namespace SPA.Application.Dtos;

public class CreateUpdateClienteDto
{
    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Nome { get; set; }

    [EmailAddress(ErrorMessage = "O campo deve conter um endereço de e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 000.000.000-00.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O número de telefone deve estar no formato (00) 00000-0000 ou (00) 0000-0000.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Endereco { get; set; }

    public string? Observacao { get; set; }
}