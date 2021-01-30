using System;
using System.Threading.Tasks;
using TransformacaoDigital.Mensageria.Dto;
using TransformacaoDigital.Mensageria.Services;
using TransformacaoDigital.ProcessoIndustrial.API.Dtos;
using TransformacaoDigital.ProcessoIndustrial.API.Models;
using TransformacaoDigital.ProcessoIndustrial.API.Repositorios;
using TransformacaoDigital.ProcessoIndustrial.API.ViewModels;

namespace TransformacaoDigital.ProcessoIndustrial.API.Services.Implementacoes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISenderService senderService;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio,
            ISenderService senderService)
        {
            this.senderService = senderService;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<object> LerPorIdAsync(Guid id)
        {
            return await _usuarioRepositorio.LerPorIdDtoAsync(id);
        }
        public async Task AlterarAsync(Guid usuarioId, UsuarioViewModel viewModel)
        {
            var model = await _usuarioRepositorio.LerPorIdAsync(usuarioId);

            if (model == null) return;

            model.Nome = viewModel.Nome;
            model.SetPerfil(viewModel.PerfilId);
            model.SetTipoUsuario(viewModel.TipoUsuarioId);
            
            await _usuarioRepositorio.AlterarAsync(model);
        }

        public async Task CadastrarAsync(NovoUsuarioViewModel viewModel)
        {
            var model = new Usuario(
                viewModel.Nome,
                viewModel.Email,
                string.Empty,
                viewModel.PerfilId,
                viewModel.TipoUsuarioId);

            var senhaProveisoria = model.SetSenhaProvisoria();

            await _usuarioRepositorio.CadastrarAsync(model);

            senderService.Send(Mensageria.QueueEnum.EnviarEmail, new EmailDto
            {
                Assunto = "Senha Temporaria",
                Mensagem = $"Prezado {model.Nome}, utilize a senha temporária <b>{senhaProveisoria}</b> para acessar o sistema. <br /> Atenciosamente, <br /> Equipe Textil",
                Destinatarios = model.Email
            });
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

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _usuarioRepositorio.EmailExisteAsync(email);
        }

        public async Task ReativarAsync(Guid id)
        {
            var model = await _usuarioRepositorio.LerPorIdAsync(id);

            if (model == null) return;

            model.Ativar();

            await _usuarioRepositorio.AlterarAsync(model);
        }

        public async Task AlterarSenhasASync(AlterarSenhaViewModel viewModel)
        {
            var model = await _usuarioRepositorio.LerPorIdAsync(viewModel.UsuarioId);
            
            if (model == null) throw new Exception("Usuário inexistente");

            var usuarioTemp = new Usuario(string.Empty, model.Email, viewModel.SenhaAtual, model.PerfilId, model.TipoUsuarioId);

            if (model.Senha != usuarioTemp.Senha) throw new Exception("Senha incorreta");

            model.SetSenha(viewModel.NovaSenha);

            await _usuarioRepositorio.AlterarAsync(model);
        }
    }
}
