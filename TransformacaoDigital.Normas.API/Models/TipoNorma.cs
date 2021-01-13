using System.Collections.Generic;

namespace TransformacaoDigital.Normas.API.Models
{
    public class TipoNorma
    {
        protected TipoNorma() 
        {
            Normas = new HashSet<Norma>();
        }

        public TipoNorma(string nome) : this()
        {
            Nome = nome;
        }

        public byte Id { get; private set; }
        public string Nome { get; private set; }

        public virtual ICollection<Norma> Normas { get; private set; }
    }
}
