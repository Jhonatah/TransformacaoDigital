using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.Services.Dtos.EmpresaDtos;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class EmpresaService : ServiceBase, IEmpresaService
    {
        public EmpresaService(IServicosHttpBase servicosBase) : base(servicosBase)
        {
        }

        public async Task AlterarASync(AlterarEmpresaViewModel viewModel)
        {
            var tipos = await PutAsync<EmpresaDto>(string.Concat(RotasAPI.ConsultoriaAssessoria_Empresa, $"/{viewModel.Id.ToString()}"), new
            {
                viewModel.TipoEmpresaId,
                viewModel.NomeFantasia,
                viewModel.RazaoSocial,
                viewModel.Email                
            });

            if (tipos.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Objeto inválido");
            }
        }

        public async Task CadastrarAsync(NovaEmpresaViewModel viewModel)
        {
            var tipos = await PostAsync<EmpresaDto>(RotasAPI.ConsultoriaAssessoria_Empresa, new 
            { 
                viewModel.TipoEmpresaId,
                viewModel.NomeFantasia,
                viewModel.RazaoSocial,
                viewModel.CNPJ,
                viewModel.Email
            });

            if(tipos.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Objeto inválido");
            }
        }

        public async Task<EmpresaDto> LerPorIdAsync(Guid id)
        {
            var tipos = await GetAsync<EmpresaDto>(string.Format(RotasAPI.ConsultoriaAssessoria_EmpresaPorId, $"{id.ToString()}"));
            return tipos.ObjResult;
        }

        public async Task<Paginador<EmpresaDto>> ListarAsync(int pagina = 1)
        {
            var tipos = await GetAsync<Paginador<EmpresaDto>>(string.Concat(RotasAPI.ConsultoriaAssessoria_Empresa, $"/{pagina}"));
            return tipos.ObjResult;
        }

        public async Task<IEnumerable<ComumDto>> ListarTiposAsync()
        {
            var tipos = await GetAsync<List<ComumDto>>(RotasAPI.ConsultoriaAssessoria_Empresa_Tipos);
            return tipos.ObjResult;
        }
    }
}
