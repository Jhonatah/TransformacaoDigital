using System;
using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.ProcessoIndustrial.API.ViewModels
{
    public class AlterarSenhaViewModel
    {
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NovaSenha { get; set; }
    }
}
