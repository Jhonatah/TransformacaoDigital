using System.Threading.Tasks;
using TransformacaoDigital.Normas.API.Models;
using TransformacaoDigital.Normas.API.Repositorios;
using TransformacaoDigital.Normas.API.ViewModels;

namespace TransformacaoDigital.Normas.API.Services.Implementacoes
{
    public class NormaService : INormaService
    {
        private readonly INormasRepositorio _normasRepositorio;

        public NormaService(INormasRepositorio normasRepositorio)
        {
            _normasRepositorio = normasRepositorio;
        }

        public void Cadastrar(NormaViewModel viewModel)
        {
            _normasRepositorio.Cadastrar(
                new Norma(
                    viewModel.TipoNormaId,
                    viewModel.Nome,
                    viewModel.Descricao));
        }

        public async Task CadastrarAsync(NormaViewModel viewModel)
        {
            await _normasRepositorio.CadastrarAsync(
                new Norma(
                    viewModel.TipoNormaId,
                    viewModel.Nome,
                    viewModel.Descricao));
        }
    }
}