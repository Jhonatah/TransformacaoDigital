using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Services;

namespace TransformacaoDigital.ProcessoIndustrial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : BaseController
    {
        private readonly IUsuarioService usuarioService;

        public PerfisController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPerfis()
        {
            return Ok(await usuarioService.ListarPerfisAsync());
        }
    }
}
