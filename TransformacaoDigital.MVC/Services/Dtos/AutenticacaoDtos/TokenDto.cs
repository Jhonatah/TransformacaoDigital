using System;

namespace TransformacaoDigital.MVC.Services.Dtos.AutenticacaoDtos
{

    public class TokenDto
    {
        public Guid UsuarioId { get; set; }

        public bool Autenticado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Token { get; set; }
    }
}
