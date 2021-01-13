using System.Collections.Generic;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Models
{
    public class TipoContrato
    {
        public byte Id { get; private set; }
        public string Nome { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}