using System;

namespace TransformacaoDigital.Normas.API.Models
{
    public class Norma
    {
        protected Norma()
        {

        }

        public Norma(byte tipoNomeId, string nome, string descricao)
        {
            TipoNormaId = tipoNomeId;
            Nome = nome;
            Descricao = descricao;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public byte TipoNormaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        public TipoNorma TipoNorma { get; private set; }
    }
}