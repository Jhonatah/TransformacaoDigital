using System;
using System.Threading.Tasks;
using TransformacaoDigital.Autenticacao.API.Models;

namespace TransformacaoDigital.Autenticacao.API.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task CadastrarAsync(Usuario usuario);

        Task<bool> AutenticarAsync(string email, string senha);

        Task<Usuario> LerPorIdAsync(Guid id);
        Task<Usuario> LerEmailAsync(string email);
    }
}
