using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios;
using TransformacaoDigital.ConsultoriaAssessoria.API.Services;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    public class TiposContratosController : BaseController
    {
        private readonly IContratoRepositorio _contratoRepositorio;

        public TiposContratosController(IUsuarioService usuarioService,
            IContratoRepositorio contratoRepositorio) : base(usuarioService)
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
