using System;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Dtos;
using TransformacaoDigital.ProcessoIndustrial.API.Models;
using TransformacaoDigital.ProcessoIndustrial.API.Repositorios;
using TransformacaoDigital.ProcessoIndustrial.API.ViewModels;

namespace TransformacaoDigital.ProcessoIndustrial.API.Services.Implementacoes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task AlterarAsync(Guid usuarioId, UsuarioViewModel viewModel)
        {
            var model = await _usuarioRepositorio.LerPorIdAsync(usuarioId);

            if (model == null) return;

            model.Nome = viewModel.Nome;
            model.SetPerfil(viewModel.PerfilId);
            
            await _usuarioRepositorio.AlterarAsync(model);
        }

        public async Task CadastrarAsync(NovoUsuarioViewModel viewModel)
        {
            var model = new Usuario(
                viewModel.Nome,
                viewModel.Email,
                viewModel.Senha,
                viewModel.PerfilId,
                viewModel.TipoUsuarioId);

            await _usuarioRepositorio.CadastrarAsync(model);
        }

        public async Task DesativarAsync(Guid usuarioId)
        {
            var model = await _usuarioRepositorio.LerPorIdAsync(usuarioId);

            if (model == null) return;

            model.SetDesativado();

            await _usuarioRepositorio.AlterarAsync(model);
        }

        public async Task<Paginador<object>> ListarAsync(int pagina = 1, int registrosPorPagina = 10)
        {
            return await _usuarioRepositorio.ListarAsync(pagina, registrosPorPagina);
        }
        public async Task<object> ListarTipoUsuariosAsync()
        {
            return await _usuarioRepositorio.ListarTipoUsuariosAsync();
        }

        public async Task<object> ListarPerfisAsync()
        {
            return await _usuarioRepositorio.ListarPerfisAsync();
        }
    }
}
