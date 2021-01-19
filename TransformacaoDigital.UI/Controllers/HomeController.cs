using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TransformacaoDigital.Mensageria.Services;
using TransformacaoDigital.UI.Models;

namespace TransformacaoDigital.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISenderService _senderService;

        public HomeController(ILogger<HomeController> logger,
            ISenderService senderService)
        {
            _logger = logger;
            _senderService = senderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult SendMensagem()
        {
            _senderService.Send(Mensageria.QueueEnum.ProcessosInternos, new { ID = 10, Nome = "Teste Com Objeto", Data = DateTime.Now });
            _senderService.Send(Mensageria.QueueEnum.Normas, new { TipoNormaId = 2, Nome = $"Mensagem - {DateTime.Now.ToString("yyyyMMddHHmmss")}", Descricao="Uma descrição Qualquer" });
            _senderService.Send(Mensageria.QueueEnum.ConsultoriasAssessorias, new { ID = 10, Nome = "Teste Com Objeto", Data = DateTime.Now });
            _senderService.Send(Mensageria.QueueEnum.IntegracaoERPProcessosInternos, new { ID = 10, Nome = "Teste Com Objeto", Data = DateTime.Now });

            return Json(null);
        }
    }
}
