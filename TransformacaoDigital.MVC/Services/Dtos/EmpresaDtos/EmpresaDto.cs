using System;

namespace TransformacaoDigital.MVC.Services.Dtos.EmpresaDtos
{
    public class EmpresaDto
    {
        public Guid Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }

        public ComumDto TipoEmpresa { get; set; }
    }
}
