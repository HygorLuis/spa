using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SPA.Application.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "Campo obrigatório.")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "Campo obrigatório.")]
    [DataType(DataType.Password)]
    public string Senha
    {
        get => HttpUtility.UrlDecode(_senha);
        set => _senha = value;
    }
    private string _senha;
}