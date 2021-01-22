using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.ProcessoIndustrial.API.ViewModels
{
    public class NovoUsuarioViewModel
    {
        [Required]
        public byte PerfilId { get; set; }
        [Required]
        public byte TipoUsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
