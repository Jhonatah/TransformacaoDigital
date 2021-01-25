using System;

namespace TransformacaoDigital.MVC.Services.Dtos.NormasDto
{
    public class NormaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }

        public ComumDto TipoNorma { get; set; }
    }
}