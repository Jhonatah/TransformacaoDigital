using System;

namespace TransformacaoDigital.Library.Dtos
{
    public class CrossToken
    {
        public CrossToken(string token, string origem)
        {
            Token = token;
            Origem = origem;
        }

        public string Token { get; set; }
        public string Origem { get; set; }
        public DateTime DataGeracao { get; set; } = DateTime.Now;

        public bool EstaValido()
        {
            return
                !string.IsNullOrEmpty(Token) &&
                !string.IsNullOrEmpty(Origem) &&
                DataGeracao > DateTime.Now.AddMinutes(5);
        }
    }
}