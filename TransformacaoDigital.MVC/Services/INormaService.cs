using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.Services.Dtos.NormasDto;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Services
{
    public interface INormaService
    {
        Task<Paginador<NormaDto>> ListarAsync(int pagina = 1);
        Task<NormaDto> LerPorIdAsync(Guid id);

        Task CadastrarAsync(NovaNormaViewModel viewModel);
        Task AlterarAsync(Guid id, AlterarNormaViewModel viewModel);


        Task<IEnumerable<ComumDto>> ListarTiposAsync();
    }
}