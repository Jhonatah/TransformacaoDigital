using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TransformacaoDigital.MVC.Services;

namespace TransformacaoDigital.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly INotificacaoService NotificacaoService;

        protected BaseController(INotificacaoService notificacaoService)
        {
            NotificacaoService = notificacaoService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (NotificacaoService.TemNotificacao() == false) return;

            TempData["MensagensDeErros"] = JsonConvert.SerializeObject(NotificacaoService.Listar());
        }
    }
}
