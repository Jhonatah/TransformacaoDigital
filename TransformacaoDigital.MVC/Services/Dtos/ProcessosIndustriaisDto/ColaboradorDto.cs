using System;

namespace TransformacaoDigital.MVC.Services.Dtos.ProcessosIndustriaisDto
{
    public class ColaboradorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public ComumDto Perfil { get; set; }
        public ComumDto TipoUsuario { get; set; }
    }
}