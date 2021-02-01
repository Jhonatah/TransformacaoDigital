using System;
using System.Runtime.Serialization;

namespace TransformacaoDigital.MVC.Services.Dtos.NormasDto
{
    [DataContract]
    public class NormaDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public DateTime DataCadastro { get; set; }

        [DataMember]
        public ComumDto TipoNorma { get; set; }
    }
}