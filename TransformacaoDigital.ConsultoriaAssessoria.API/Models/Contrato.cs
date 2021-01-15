using System;
using System.Collections.Generic;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Models
{
    public class Contrato
    {
        protected Contrato() { }

        public Contrato(byte tipoContratoId, string nome, string descricao)
        {
            TipoContratoId = tipoContratoId;
            Nome = nome;
            Descricao = descricao;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public byte TipoContratoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

        public virtual ICollection<EmpresaContrato> Empresas { get; set; }
    }
}