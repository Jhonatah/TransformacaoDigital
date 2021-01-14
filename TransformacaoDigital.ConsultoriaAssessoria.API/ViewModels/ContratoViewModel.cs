using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.ViewModels
{
    public class ContratoViewModel
    {
        [Required]
        public byte TipoContratoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}