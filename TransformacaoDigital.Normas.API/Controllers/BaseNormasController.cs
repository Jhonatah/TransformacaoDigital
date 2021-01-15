using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TransformacaoDigital.Normas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ConsultoriaAssessoria")]
    public abstract class BaseNormasController : ControllerBase
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

            //await EnviarNotificacoesLogASync();

            //if (OperacaoValida())
            //{
            //    return Ok(new
            //    {
            //        result,
            //        mensagens = ServicosBase.NotificacaoService.Listar()
            //    });
            //}

            //return BadRequest(new
            //{
            //    errors = ServicosBase.NotificacaoService.Listar().Where(w => w.Tipo == NotificacaoEnum.Erro)
            //});
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
