using System;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Dtos;
using TransformacaoDigital.ProcessoIndustrial.API.ViewModels;

namespace TransformacaoDigital.ProcessoIndustrial.API.Services
{
    public interface IUsuarioService
    {
        Task CadastrarAsync(NovoUsuarioViewModel viewModel);
        Task AlterarAsync(Guid usuarioId, UsuarioViewModel viewModel);
        Task DesativarAsync(Guid usuarioId);
        Task<object> LerPorIdAsync(Guid id);

        Task AlterarSenhasASync(AlterarSenhaViewModel viewModel);

        Task<Paginador<object>> ListarAsync(int pagina = 1, int registrosPorPagina = 10);
        Task<object> ListarTipoUsuariosAsync();
        Task<object> ListarPerfisAsync();
        Task<bool> EmailExisteAsync(string email);
        Task ReativarAsync(Guid id);
    }
}
