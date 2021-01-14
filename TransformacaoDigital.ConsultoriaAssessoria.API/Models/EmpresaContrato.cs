using System;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Models
{
    public class EmpresaContrato
    {
        protected EmpresaContrato() { }

        public EmpresaContrato(Guid empresaId, Guid contratoId, Guid usuarioId)
        {
            EmpresaId = empresaId;
            ContratoId = contratoId;
            UsuarioId = usuarioId;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid EmpresaId { get; private set; }
        public Guid ContratoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public DateTime DataAlteracao { get; private set; }
        public bool Ativo { get; private set; } = true;
        public bool Vigente { get; private set; } = true;

        public virtual Empresa Empresa { get; set; }
        public virtual Contrato Contrato { get; set; }

        public void SetAtivo(bool status)
        {
            Ativo = status;
            DataAlteracao = DateTime.Now;
        }
        public void SetVigencia(bool status)
        {
            Vigente = status;
            DataAlteracao = DateTime.Now;
        }
    }
}