using System;
using System.Collections.Generic;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Models
{
    public class Empresa
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UsuarioId { get; private set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public virtual TipoEmpresa TipoEmpresa { get; set; }

        public virtual ICollection<EmpresaContrato> Contratos { get; set; }
    }
}
