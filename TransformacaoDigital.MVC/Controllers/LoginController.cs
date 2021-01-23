using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services;
using TransformacaoDigital.MVC.ViewModels;

namespace TransformacaoDigital.MVC.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IAutenticacaoService autenticacaoService;

        public LoginController(INotificacaoService notificacaoService,
            IAutenticacaoService autenticacaoService) : base(notificacaoService)
        {
            this.autenticacaoService = autenticacaoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var credenciais = await autenticacaoService.LoginAsync(loginViewModel.Email, loginViewModel.Senha);

                if(credenciais != null && credenciais.Autenticado)
                {
                    var token = new JwtSecurityToken(credenciais.Token);

                    ClaimsIdentity ci = new ClaimsIdentity(token.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ci.AddClaim(new Claim("hash", credenciais.Token));

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(ci),
                        new AuthenticationProperties()
                        { 
                            IsPersistent = false,
                            ExpiresUtc = credenciais.DataExpiracao
                        });
                }
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel
            {
                Email = loginViewModel.Email
            });
        }


        [HttpGet]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}