using System;
using System.Text;
using TransformacaoDigital.Library;

namespace TransformacaoDigital.Autenticacao.API.Models
{
    public class Usuario
    {
        private const string _salt = "fshfuihef983h93TCC_TransformacaoDigital2021Dfh3w8833802hfh_____fkdjbfb33i";


        protected Usuario() { }

        public Usuario(string nome, string email, string senha, byte perfilId)
        {
            Nome = nome;
            Email = email;
            PerfilId = perfilId;

            SetSenha(senha);
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public byte PerfilId { get; private set; }
        public string Nome { get; set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; private set; }
        public string Salt { get; private set; }
        public bool Ativo { get; private set; } = true;

        public virtual Perfil Perfil { get; set; }

        private void SetSalt()
        {
            if (string.IsNullOrEmpty(Salt))
            {
                Salt = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(".", Email, _salt)));
                Salt = Encriptador.Get().EncriptarSemReversao(Salt);
            }
        }

        public void SetSenha(string senha)
        {
            SetSalt();

            Senha = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(".", Salt, senha)));
            Senha = Encriptador.Get().EncriptarSemReversao(Senha);
        }

        public void SetDesativado()
        {
            DataAlteracao = DateTime.Now;
            Ativo = false;
        }
    }
}
