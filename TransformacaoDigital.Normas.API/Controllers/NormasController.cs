using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.Normas.API.Models;
using TransformacaoDigital.Normas.API.Repositorios;
using TransformacaoDigital.Normas.API.ViewModels;

namespace TransformacaoDigital.Normas.API.Controllers
{
    [Route("api/[controller]")]
    public class NormasController : BaseNormasController
    {
        private readonly INormasRepositorio _normasRepositorio;

        public NormasController(INormasRepositorio normasRepositorio)
        {
            _normasRepositorio = normasRepositorio;
        }

        [HttpGet]
        [Route("/{pagina:int}")]
        public async Task<IActionResult> Listar(int pagina = 1)
        {
            return await Response(await _normasRepositorio.ListarAsync(pagina));
        }

        [HttpGet]
        [Route("/{id:guid}")]
        public async Task<IActionResult> LerPorId(Guid id)
        {
            return await Response(await _normasRepositorio.LerPorIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNorma(NormaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _normasRepositorio.CadastrarAsync(
                    new Norma(
                        viewModel.TipoNormaId,
                        viewModel.Nome,
                        viewModel.Descricao));

                return await Response();
            }

            return ResponseBadRequest("Objeto inválido");

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> AlterarNorma(Guid id, NormaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var norma = await _normasRepositorio.LerPorIdAsync(id);

                if (norma == null)
                    return ResponseBadRequest("Objeto não encontrado");

                await _normasRepositorio.AlterarAsync(id,
                    new Norma(
                        viewModel.TipoNormaId,
                        viewModel.Nome,
                        viewModel.Descricao));

                return await Response();
            }

            return ResponseBadRequest("Objeto inválido");
        }
    }
}