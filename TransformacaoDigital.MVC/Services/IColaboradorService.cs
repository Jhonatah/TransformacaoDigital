using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.Services.Dtos.ProcessosIndustriaisDto;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Services
{
    public interface IColaboradorService
    {
        Task<bool> EmailExiteAsync(string email);
        Task<ColaboradorDto> LerPorIdAsync(Guid id);
        Task<Paginador<ColaboradorDto>> ListarAsync(int pagina);
        Task CadastrarAsync(NovoColaboradorViewModel viewModel);
        Task AlterarAsync(AlterarColaboradorViewModel viewModel);

        Task AlterarSenhaAsync(AlterarSenhaViewModel viewModel);

        Task<IEnumerable<ComumDto>> ListarTiposUsuariosAsync();
        Task<IEnumerable<ComumDto>> ListarPerfisAsync();
        Task DesativarAsync(Guid id);
        Task ReativarAsync(Guid id);
    }
}