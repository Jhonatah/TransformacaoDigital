using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.Services.Dtos.EmpresaDtos;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Services
{
    public interface IEmpresaService
    {
        Task<Paginador<EmpresaDto>> ListarAsync(int pagina = 1);
        Task<EmpresaDto> LerPorIdAsync(Guid id);

        Task CadastrarAsync(NovaEmpresaViewModel viewModel);
        Task AlterarASync(AlterarEmpresaViewModel viewModel);

        Task<IEnumerable<ComumDto>> ListarTiposAsync();
    }
}