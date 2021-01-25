using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.Services.Dtos.NormasDto;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class NormaService : ServiceBase, INormaService
    {
        public NormaService(IServicosHttpBase servicosBase) : base(servicosBase)
        {
        }

        public async Task AlterarAsync(Guid id, AlterarNormaViewModel viewModel)
        {
            var response = await PutAsync(string.Concat(RotasAPI.Normas, $"/{id}"), new
            {
                id,
                viewModel.Nome,
                viewModel.Descricao,
                viewModel.TipoNormaId
            });

            if (response.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Objeto de cadastro inválido");
            }
        }

        public async Task CadastrarAsync(NovaNormaViewModel viewModel)
        {
            var response = await PostAsync(RotasAPI.Normas, new
            {
                viewModel.Nome,
                viewModel.Descricao,
                viewModel.TipoNormaId
            });

            if(response.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Objeto de cadastro inválido");
            }
        }

        public async Task<NormaDto> LerPorIdAsync(Guid id)
        {
            var tipos = await GetAsync<NormaDto>(string.Format(RotasAPI.Normas_PorId, id.ToString()));
            return tipos.ObjResult;
        }

        public async Task<Paginador<NormaDto>> ListarAsync(int pagina = 1)
        {
            var tipos = await GetAsync<Paginador<NormaDto>>(string.Concat(RotasAPI.Normas, $"/{pagina}"));
            return tipos.ObjResult;
        }

        public async Task<IEnumerable<ComumDto>> ListarTiposAsync()
        {
            var tipos = await GetAsync<List<ComumDto>>(RotasAPI.Normas_Tipos);
            return tipos.ObjResult;
        }
    }
}