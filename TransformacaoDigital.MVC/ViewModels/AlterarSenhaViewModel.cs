using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.MVC.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string SenhaAtual { get; set; }

        [Display(Name = "Nova Senha")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(8, ErrorMessage = "Tamanho mínimo de 8 caracteres")]
        public string NovaSenha { get; set; }

        [Display(Name = "Confirmar Nova Senha")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarNovaSenha { get; set; }
    }
}