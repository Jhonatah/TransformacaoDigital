using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Services;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public BaseController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        private readonly IUsuarioService _usuarioService;
        private Guid _userId = Guid.Empty;
        protected Guid UsuarioId 
        { 
            get
            {
                if(_userId == Guid.Empty)
                {
                    _userId = _usuarioService.GerUserId(HttpContext.Request.Headers[HeaderNames.Authorization.Replace("Bearer ", "")]);
                }

                return _userId;
            }
        }

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
