using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios;
using TransformacaoDigital.ConsultoriaAssessoria.API.ViewModels;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Controllers
{
    public class EmpresasController : BaseController
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresasController(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        [HttpGet]
        [Route("/{pagina:int}")]
        public async Task<IActionResult> ListarEmpresas(int pagina = 1)
        {
            return await Response(await _empresaRepositorio.ListarAsync(pagina));
        }

        [HttpGet]
        [Route("/{id:guid}")]
        public async Task<IActionResult> LerEmpresaPorId(Guid id)
        {
            return await Response(await _empresaRepositorio.LerPorIdAsync(id));
        }


        [HttpGet]
        [Route("/{id:guid/contratos}")]
        public async Task<IActionResult> ListarEmpresasDoContrato(Guid id)
        {
            return await Response(await _empresaRepositorio.LerPorIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarEmpresa(EmpresaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cnpjExiste = await _empresaRepositorio.CNPJExisteAsync(viewModel.CNPJ);

                if(cnpjExiste)
                {
                    return ResponseBadRequest($"O CNPJ {viewModel.CNPJ} já está cadastrado");
                }

                await _empresaRepositorio.CadastrarAsync(
                    new Models.Empresa(
                        Guid.NewGuid(),
                        viewModel.TipoEmpresaId,
                        viewModel.CNPJ,
                        viewModel.RazaoSocial,
                        viewModel.NomeFantasia,
                        viewModel.Email));

                return await Response();
            }

            return ResponseBadRequest("Objeto inválido");

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> AlterarEmpresa(Guid id, EmpresaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var norma = await _empresaRepositorio.LerPorIdAsync(id);

                if (norma == null)
                    return ResponseBadRequest("Objeto não encontrado");

                await _empresaRepositorio.AlterarAsync(id,
                    new Models.Empresa(
                        Guid.NewGuid(),
                        viewModel.TipoEmpresaId,
                        viewModel.CNPJ,
                        viewModel.RazaoSocial,
                        viewModel.NomeFantasia,
                        viewModel.Email));

                return await Response();
            }

            return ResponseBadRequest("Objeto inválido");
        }
    }
}
