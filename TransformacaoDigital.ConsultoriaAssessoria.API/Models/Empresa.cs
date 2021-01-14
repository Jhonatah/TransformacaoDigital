using System;
using System.Collections.Generic;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Models
{
    public class Empresa
    {
        protected Empresa() 
        {
            Contratos = new HashSet<EmpresaContrato>();
        }

        public Empresa(Guid usuarioId, byte tipoEmpresaId, string cNPJ, string razaoSocial, string nomeFantasia, string email)
            : this()
        {
            UsuarioId = usuarioId;
            TipoEmpresaId = tipoEmpresaId;
            CNPJ = cNPJ;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Email = email;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UsuarioId { get; private set; }
        public byte TipoEmpresaId { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        public virtual TipoEmpresa TipoEmpresa { get; set; }

        public virtual ICollection<EmpresaContrato> Contratos { get; set; }
    }
}
