using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services;
using TransformacaoDigital.MVC.Services.Dtos;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Controllers
{
    [Authorize]
    public class ColaboradoresController : BaseController
    {
        private readonly IColaboradorService colaboradorService;

        public ColaboradoresController(
            IColaboradorService colaboradorService,
            INotificacaoService notificacaoService) : base(notificacaoService)
        {
            this.colaboradorService = colaboradorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        private async Task PopularListaAsync(NovoColaboradorViewModel viewModel)
        {
            viewModel.TiposUsuarios = (await colaboradorService.ListarTiposUsuariosAsync()).Select(x => new SelectListItem()
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            });

            viewModel.Perfis = (await colaboradorService.ListarPerfisAsync()).Select(x => new SelectListItem()
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            });
        }

        private async Task PopularListaAsync(AlterarColaboradorViewModel viewModel)
        {
            viewModel.TiposUsuarios = (await colaboradorService.ListarTiposUsuariosAsync()).Select(x => new SelectListItem()
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            });

            viewModel.Perfis = (await colaboradorService.ListarPerfisAsync()).Select(x => new SelectListItem()
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            });
        }

        public async Task<IActionResult> Novo()
        {
            var viewModel = new NovoColaboradorViewModel();

            await PopularListaAsync(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(NovoColaboradorViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await colaboradorService.CadastrarAsync(viewModel);

                if(NotificacaoService.TemNotificacao() == false)
                return RedirectToAction("Index");
            }

            await PopularListaAsync(viewModel);

            return View(viewModel);
        }

        public async Task<IActionResult> Alterar(Guid id)
        {
            var colaborador = await colaboradorService.LerPorIdAsync(id);

            var viewModel = new AlterarColaboradorViewModel()
            {
                Id = colaborador.Id,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                PerfilId = colaborador.Perfil.Id,
                TipoUsuarioId = colaborador.TipoUsuario.Id
            };

            await PopularListaAsync(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(AlterarColaboradorViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await colaboradorService.AlterarAsync(viewModel);

                if (NotificacaoService.TemNotificacao() == false)
                    return RedirectToAction("Index");
            }

            await PopularListaAsync(viewModel);
            return View(viewModel);
        }

        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await colaboradorService.AlterarSenhaAsync(viewModel);

                if (NotificacaoService.TemNotificacao() == false)
                    return RedirectToAction("Index", "Home");
            }

            return View(new AlterarSenhaViewModel());
        }

        public async Task<PartialViewResult> ListaColaboradores(int pagina = 1)
        {
            var registros = await colaboradorService.ListarAsync(pagina);
            return PartialView("_ListaColaboradores", registros);
        }

        [HttpDelete]
        public async Task<JsonResult> DesativarConta(Guid id)
        {
            await colaboradorService.DesativarAsync(id);

            return Json(null);
        }

        [HttpPost]
        public async Task<JsonResult> ReativarConta(Guid id)
        {
            await colaboradorService.ReativarAsync(id);
            return Json(null);
        }

        #region RemoteVadations

        [AcceptVerbs("GET")]
        public async Task<IActionResult> ValidarEmailExistente(string email)
        {
            if (string.IsNullOrEmpty(email)) return Json(true);

            if(await colaboradorService.EmailExiteAsync(email))
            {
                return Json($"Email já está sendo usando");
            }

            return Json(true);
        }

        #endregion
    }
}