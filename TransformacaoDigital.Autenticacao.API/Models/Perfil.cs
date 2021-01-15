using System.Collections.Generic;

namespace TransformacaoDigital.Autenticacao.API.Models
{
    public class Perfil
    {
        protected Perfil()
        {

        }

        public byte Id { get; private set; }
        public string Nome { get; private set; }
        public virtual ICollection<Usuario> Usuarios { get; private set; }
    }
}
