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
    public class NormasController : BaseController
    {
        private readonly INormaService normaService;

        public NormasController(INotificacaoService notificacaoService,
            INormaService normaService) : base(notificacaoService)
        {
            this.normaService = normaService;
        }

        private async Task<IEnumerable<SelectListItem>> PopularListaAsync()
        {
            return (await normaService.ListarTiposAsync()).Select(x => new SelectListItem()
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
        public async Task<PartialViewResult> ListaNormas(int pagina = 1)
        {
            var registros = await normaService.ListarAsync(pagina);
            return PartialView("_ListaNormas", registros);
        }


        [HttpGet]
        public async Task<IActionResult> Novo()
        {
            var viewModel = new NovaNormaViewModel();
            viewModel.TiposNormas = await PopularListaAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(NovaNormaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await normaService.CadastrarAsync(viewModel);

                if (NotificacaoService.TemNotificacao() == false)
                    return RedirectToAction("Index");
            }

            if (viewModel == null) viewModel = new NovaNormaViewModel();

           viewModel.TiposNormas = await PopularListaAsync();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            var norma = await normaService.LerPorIdAsync(id);

            var viewModel = new AlterarNormaViewModel()
            {
                Id = norma.Id,
                Nome = norma.Nome,
                Descricao = norma.Descricao,
                TipoNormaId = norma.TipoNorma.Id
            };

            viewModel.TiposNormas = await PopularListaAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(AlterarNormaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await normaService.AlterarAsync(viewModel.Id, viewModel);

                if (NotificacaoService.TemNotificacao() == false)
                    return RedirectToAction("Index");
            }

            if (viewModel == null) viewModel = new AlterarNormaViewModel();

            viewModel.TiposNormas = await PopularListaAsync();
            return View(viewModel);
        }
    }
}