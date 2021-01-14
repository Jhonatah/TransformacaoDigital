using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    public class TiposContratosController : BaseController
    {
        private readonly IContratoRepositorio _contratoRepositorio;

        public TiposContratosController(IContratoRepositorio contratoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTiposContratos()
        {
            return await Response(await _contratoRepositorio.ListarTiposAsync());
        }
    }
}
