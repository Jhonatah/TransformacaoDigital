using System;

namespace TransformacaoDigital.Autenticacao.API.Dtos
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