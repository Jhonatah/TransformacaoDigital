using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.Normas.API.ViewModels
{
    public class NormaViewModel
    { 
        [Required]
        public byte TipoNormaId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}