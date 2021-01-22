using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Services;

namespace TransformacaoDigital.ProcessoIndustrial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public TipoUsuariosController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTiposUsuarios()
        {
            return Ok(await usuarioService.ListarTipoUsuariosAsync());
        }
    }
}
