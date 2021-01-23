using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo Inválido")]
        public string Senha { get; set; }
    }
}