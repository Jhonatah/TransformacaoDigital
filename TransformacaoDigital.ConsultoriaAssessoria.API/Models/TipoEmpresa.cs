using System.Collections.Generic;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Models
{
    public class TipoEmpresa
    {
        public byte Id { get; private set; }
        public string Nome { get; set; }

        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
