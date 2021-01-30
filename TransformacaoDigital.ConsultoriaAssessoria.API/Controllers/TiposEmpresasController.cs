using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios;
using TransformacaoDigital.ConsultoriaAssessoria.API.Services;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    public class TiposEmpresasController : BaseController
    {
        private readonly IEmpresaRepositorio empresaRepositorio;

        public TiposEmpresasController(
            IEmpresaRepositorio empresaRepositorio,
            IUsuarioService usuarioService) : base(usuarioService)
        {
            this.empresaRepositorio = empresaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEmpresas()
        {
            return Response(await empresaRepositorio.ListarTiposAsync());
        }
    }
}
