using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.Normas.API.Repositorios;

namespace TransformacaoDigital.Normas.API.Controllers
{
    public class TipoNormasController : BaseNormasController
    {
        private readonly INormasRepositorio _normasRepositorio;

        public TipoNormasController(INormasRepositorio normasRepositorio)
        {
            _normasRepositorio = normasRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Response(await _normasRepositorio.ListarTiposNormasAsync());
        }
    }
}