using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
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
