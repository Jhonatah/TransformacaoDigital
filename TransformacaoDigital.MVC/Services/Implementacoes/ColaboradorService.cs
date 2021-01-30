using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransformacaoDigital.Library;
using TransformacaoDigital.Library.Enumerados;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.Services.Dtos.ProcessosIndustriaisDto;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class ColaboradorService : ServiceBase, IColaboradorService
    {
        public ColaboradorService(IServicosHttpBase servicosBase) : base(servicosBase)
        {
        }

        public async Task CadastrarAsync(NovoColaboradorViewModel viewModel)
        {
            var result = await PostAsync(RotasAPI.ProcessosIndustriais_Colaboradores, viewModel);

            if(result.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Colaborador inválido para cadastro");
            }
        }

        public async Task AlterarAsync(AlterarColaboradorViewModel viewModel)
        {
            var result = await PutAsync(string.Concat(RotasAPI.ProcessosIndustriais_Colaboradores, $"/{viewModel.Id}"), viewModel);

            if (result.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Colaborador inválido para cadastro");
            }
        }

        public async Task<bool> EmailExiteAsync(string email)
        {
            var result = await GetAsync<bool>(string.Format(RotasAPI.ProcessosIndustriais_Colaboradores_EmailExiste, email));

            return result.ObjResult;
        }

        public async Task<ColaboradorDto> LerPorIdAsync(Guid id)
        {
            var result = await GetAsync<ColaboradorDto>(string.Format(RotasAPI.ProcessosIndustriais_Colaboradores_LerPorID, id.ToString()));

            return result.Sucesso == false ? null : result.ObjResult;
        }

        public async Task<Paginador<ColaboradorDto>> ListarAsync(int pagina)
        {
            var result = await GetAsync<Paginador<ColaboradorDto>>(string.Concat(RotasAPI.ProcessosIndustriais_Colaboradores, $"/{pagina}"));
            return result.ObjResult;
        }

        public async Task<IEnumerable<ComumDto>> ListarPerfisAsync()
        {
            var result = await GetAsync<List<ComumDto>>(RotasAPI.ProcessosIndustriais_Perfis);
            return result.ObjResult;
        }

        public async Task<IEnumerable<ComumDto>> ListarTiposUsuariosAsync()
        {
            var result = await GetAsync<List<ComumDto>>(RotasAPI.ProcessosIndustriais_TiposColaboradores);
            return result.ObjResult;
        }

        public async Task DesativarAsync(Guid id)
        {
            var result = await DeleteAsync(string.Format(RotasAPI.ProcessosIndustriais_Colaboradores_Desativar, id.ToString()));

            if (result.Sucesso == false)
                ServicosBase.NotificacaoService.NotificarErro("Houve um erro na desativação. Tente novamente mais tarde");
        }

        public async Task ReativarAsync(Guid id)
        {
            var result = await PutAsync(string.Format(RotasAPI.ProcessosIndustriais_Colaboradores_Retivar, id.ToString()), null);

            if (result.Sucesso == false)
                ServicosBase.NotificacaoService.NotificarErro("Houve um erro na reativação. Tente novamente mais tarde");
        }

        public async Task AlterarSenhaAsync(AlterarSenhaViewModel viewModel)
        {
            var senhaAtual =
                Encriptador.Get().Encriptar(EncriptEnum.d8a49e8e450842848509f40ff9b3851b.GetName(), viewModel.SenhaAtual);

            var novaSenha =
                Encriptador.Get().Encriptar(EncriptEnum.d8a49e8e450842848509f40ff9b3851b.GetName(), viewModel.NovaSenha);

            var result = await PutAsync(string.Format(RotasAPI.ProcessosIndustriais_Colaboradores_AlterarSenha, ServicosBase.UsuarioService.GetId()), new 
            { 
                SenhaAtual = Convert.ToBase64String(Encoding.UTF8.GetBytes(senhaAtual)),
                NovaSenha = Convert.ToBase64String(Encoding.UTF8.GetBytes(novaSenha))
            });

            if(result.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Senhas inválidas");
            }
        }
    }
}