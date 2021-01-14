using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.ViewModels
{
    public class EmpresaViewModel
    {
        [Required]
        public byte TipoEmpresaId { get; set; }

        [Required]
        public string RazaoSocial { get; set; }

        [Required]
        public string NomeFantasia { get; set; }

        [Required]
        public string CNPJ { get; set; }

        [Required]
        public string Email { get; set; }
    }
}