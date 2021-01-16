using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using TransformacaoDigital.Autenticacao.API.Models;
using TransformacaoDigital.Autenticacao.API.Repositorios;
using TransformacaoDigital.Autenticacao.API.Services;
using TransformacaoDigital.Library;
using TransformacaoDigital.Library.Enumerados;

namespace TransformacaoDigital.Autenticacao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class AutenticacaoController : ControllerBase
    {
        public AutenticacaoController(JWTService jWTService,
            IUsuarioRepositorio usuarioRepositorio)
        {
            _jwtService = jWTService;
            _usuarioRepositorio = usuarioRepositorio;
        }

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly JWTService _jwtService; 

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string token)
        {
            if (string.IsNullOrEmpty(token)) return BadRequest();

            var stringDecriptada = Encriptador.Get().Decriptar(EncriptEnum.c7b70d19b7db400c84de2b570b49c1fd.GetName(), token);

            if (string.IsNullOrEmpty(stringDecriptada)) return BadRequest();

            string[] dados = stringDecriptada.Split("|[[.]]|");

            if (dados == null || dados.Length != 2) return BadRequest();

            var usuarioTemp = new Usuario(string.Empty, dados[0], dados[1], 0);

            var autenticado = await _usuarioRepositorio.AutenticarAsync(usuarioTemp.Email, usuarioTemp.Senha);

            if (autenticado == false) return BadRequest();

            var usuario = await _usuarioRepositorio.LerEmailAsync(usuarioTemp.Email);

            return Ok(_jwtService.GerarToken(usuario));
        }

        [HttpPost]
        [Route("ValidateToken")]
        public IActionResult ValidToken(string token)
        {
            var valido = _jwtService.TokenValido(HttpContext.Request.Headers[HeaderNames.Authorization]);

            if (valido) return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("GetId")]
        public IActionResult GetUserId()
        {
            return Ok(User.GetClaim(JwtRegisteredClaimNames.Jti));
        }
    }
}
