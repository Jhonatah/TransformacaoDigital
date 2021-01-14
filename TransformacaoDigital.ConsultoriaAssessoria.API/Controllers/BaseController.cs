using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected new async Task<IActionResult> Response(object result = null)
        {
            try
            {
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return ResponseBadRequest(ex.Message);
            }
        }

        protected IActionResult ResponseBadRequest(string mensagem)
        {
            return BadRequest(new
            {
                Error = mensagem
            });
        }
    }
}
