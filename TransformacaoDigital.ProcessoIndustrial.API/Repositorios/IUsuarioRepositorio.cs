using System;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Dtos;
using TransformacaoDigital.ProcessoIndustrial.API.Models;

namespace TransformacaoDigital.ProcessoIndustrial.API.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Paginador<object>> ListarAsync(int pagina = 1, int registrosPorPagina = 10);

        Task CadastrarAsync(Usuario model);
        Task AlterarAsync(Usuario model);
        Task<Usuario> LerPorIdAsync(Guid usuarioId);


        Task<object> ListarTipoUsuariosAsync();
        Task<object> ListarPerfisAsync();
    }
}