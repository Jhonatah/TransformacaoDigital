using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Controllers
{
    public class EmpresasController : BaseController
    {
        private readonly IEmpresaService empresaService;

        public EmpresasController(
            IEmpresaService empresaService,
            INotificacaoService notificacaoService) : base(notificacaoService)
        {
            this.empresaService = empresaService;
        }

        private async Task<IEnumerable<SelectListItem>> PopularListaAsync()
        {
            return (await empresaService.ListarTiposAsync()).Select(x => new SelectListItem()
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> ListaEmpresas(int pagina = 1)
        {
            var registros = await empresaService.ListarAsync(pagina);
            return PartialView("_ListaEmpresas", registros);
        }



        [HttpGet]
        public async Task<IActionResult> Novo()
        {
            var viewModel = new NovaEmpresaViewModel();
            viewModel.TiposEmpresas = await PopularListaAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(NovaEmpresaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await empresaService.CadastrarAsync(viewModel);

                if (NotificacaoService.TemNotificacao() == false)
                    return RedirectToAction("Index");
            }

            if (viewModel == null) viewModel = new NovaEmpresaViewModel();

            viewModel.TiposEmpresas = await PopularListaAsync();

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            var model = await empresaService.LerPorIdAsync(id);

            var viewModel = new AlterarEmpresaViewModel()
            {
                Id = model.Id,
                TipoEmpresaId = model.TipoEmpresa.Id,
                Email = model.Email,
                NomeFantasia = model.NomeFantasia,
                RazaoSocial = model.RazaoSocial,
                CNPJ = model.CNPJ
            };

            viewModel.TiposEmpresas = await PopularListaAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(AlterarEmpresaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await empresaService.AlterarASync(viewModel);

                if (NotificacaoService.TemNotificacao() == false)
                    return RedirectToAction("Index");
            }

            if (viewModel == null) viewModel = new AlterarEmpresaViewModel();

            viewModel.TiposEmpresas = await PopularListaAsync();
            return View(viewModel);
        }
    }
}