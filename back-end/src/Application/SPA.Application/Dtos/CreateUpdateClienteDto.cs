using System.ComponentModel.DataAnnotations;

namespace SPA.Application.Dtos;

public class CreateUpdateClienteDto
{
    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Nome { get; set; }

    [EmailAddress(ErrorMessage = "O campo deve conter um endereço de e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 números.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O telefone deve conter 10 ou 11 dígitos.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Endereco { get; set; }

    public string? Observacao { get; set; }
}