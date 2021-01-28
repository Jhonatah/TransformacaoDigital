using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransformacaoDigital.Library;

namespace TransformacaoDigital.MVC.Controllers
{
    public class ValidadoresController : Controller
    {
        [AcceptVerbs("GET")]
        public IActionResult ValidarCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return Json(true);

            if (!cnpj.IsCnpj())
            {
                return Json($"CNPJ inválido");
            }

            return Json(true);
        }
    }
}
