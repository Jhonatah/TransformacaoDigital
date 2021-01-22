using System.Collections.Generic;

namespace TransformacaoDigital.ProcessoIndustrial.API.Models
{
    public class TipoUsuario
    {
        public byte Id { get; private set; }
        public string Nome { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}