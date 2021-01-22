using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Services;
using TransformacaoDigital.ProcessoIndustrial.API.ViewModels;

namespace TransformacaoDigital.ProcessoIndustrial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("{pagina:int}")]
        public async Task<IActionResult> ListarUsuarios(int pagina = 1)
        {
            return Ok(await _usuarioService.ListarAsync(pagina));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarUsuario(NovoUsuarioViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _usuarioService.CadastrarAsync(viewModel);

            return Ok();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> AlterarUsuario(Guid id, UsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest();
            }

            await _usuarioService.AlterarAsync(id, viewModel);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DesativarUsuario(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _usuarioService.DesativarAsync(id);

            return Ok();
        }
    }
}