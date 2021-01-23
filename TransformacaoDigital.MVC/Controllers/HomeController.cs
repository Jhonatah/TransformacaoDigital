using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TransformacaoDigital.MVC.Models;
using TransformacaoDigital.MVC.Services;

namespace TransformacaoDigital.MVC.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(INotificacaoService notificacaoService) : base(notificacaoService)
        {
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
    }
}
