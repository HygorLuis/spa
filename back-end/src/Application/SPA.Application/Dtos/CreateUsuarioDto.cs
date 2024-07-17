using System.ComponentModel.DataAnnotations;

namespace SPA.Application.Dtos;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "Campo obrigatório.")]
    public string NomeCompleto { get; set; }

    [EmailAddress(ErrorMessage = "O campo deve conter um endereço de e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Usuario { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MinLength(8, ErrorMessage = "A senha deve conter pelo menos 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-zç])(?=.*[A-ZÇ])(?=.*\d)(?=.*\W).*$", ErrorMessage = "A senha deve conter pelo menos uma letra minúscula, uma letra maiúscula, um dígito e um caractere especial.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmarSenha { get; set; }
}