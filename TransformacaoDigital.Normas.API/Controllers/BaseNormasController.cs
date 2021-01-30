using Microsoft.AspNetCore.Mvc;

namespace TransformacaoDigital.Normas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public abstract class BaseNormasController : ControllerBase
    {
        protected new IActionResult Response(object result = null)
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
