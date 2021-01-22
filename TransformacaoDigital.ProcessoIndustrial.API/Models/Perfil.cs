using System.Collections.Generic;

namespace TransformacaoDigital.ProcessoIndustrial.API.Models
{
    public class Perfil
    {
        public byte Id { get; private set; }
        public string Nome { get; private set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}