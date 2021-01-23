using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using TransformacaoDigital.MVC.Services.Dtos;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetValor(string type)
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == type);

            if (claim == null) return null;

            return claim.Value;
        }

        public CredenciaToken GetCredencial()
        {
            return new CredenciaToken
            { 
                Id = GetId(),
                Nome = GetValor("given_name"),
                Email = GetValor("unique_name")
            };
        }

        public Guid GetId()
        {
            return Guid.Parse(GetValor("jti"));
        }

        public string GetToken()
        {
            return GetValor("hash");
        }
    }
}
