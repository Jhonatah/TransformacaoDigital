using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.ProcessoIndustrial.API.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        public byte PerfilId { get; set; }
        [Required]
        public byte TipoUsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
