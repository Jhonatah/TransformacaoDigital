using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios;
using TransformacaoDigital.ConsultoriaAssessoria.API.Services;
using TransformacaoDigital.ConsultoriaAssessoria.API.ViewModels;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    public class ContratosController : BaseController
    {
        private readonly IContratoRepositorio _contratoRepositorio;

        public ContratosController(IUsuarioService usuarioService,
            IContratoRepositorio contratoRepositorio) : base(usuarioService)
        {
            _contratoRepositorio = contratoRepositorio;
        }

        [HttpGet]
        [Route("{pagina:int}")]
        public async Task<IActionResult> ListarContratos(int pagina = 1)
        {
            return Response(await _contratoRepositorio.ListarAsync(pagina));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> LerContratoPorId(Guid id)
        {
            return Response(await _contratoRepositorio.LerPorIdAsync(id));
        }

        [HttpGet]
        [Route("{id:guid}/empresas")]
        public async Task<IActionResult> ListarEmpresasDoContrato(Guid id)
        {
            return Response(await _contratoRepositorio.ListarEmpresasAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarContrato(ContratoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _contratoRepositorio.CadastrarAsync(
                    new Models.Contrato(
                        viewModel.TipoContratoId,
                        viewModel.Nome,
                        viewModel.Descricao));

                return Response();
            }

            return ResponseBadRequest("Objeto inválido");

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> AlterarContrato(Guid id, ContratoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var norma = await _contratoRepositorio.LerPorIdAsync(id);

                if (norma == null)
                    return ResponseBadRequest("Objeto não encontrado");

                await _contratoRepositorio.AlterarAsync(id,
                    new Models.Contrato(
                        viewModel.TipoContratoId,
                        viewModel.Nome,
                        viewModel.Descricao));

                return Response();
            }

            return ResponseBadRequest("Objeto inválido");
        }
    }
}
