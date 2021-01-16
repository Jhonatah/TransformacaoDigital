using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Text.Json;
using TransformacaoDigital.Library;
using TransformacaoDigital.Library.Dtos;
using TransformacaoDigital.Library.Enumerados;

namespace TransformacaoDigital.Autenticacao.API.Filtros
{
    public class AutorizacaoCustomizada : AuthorizeAttribute, IAuthorizationFilter
    {
        private void NegarRequisicao(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        private bool OrigemValida(AuthorizationFilterContext context, CrossToken cross)
        {
            return cross.Origem == context.HttpContext.Request.PathBase;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenEncript = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            if(string.IsNullOrEmpty(tokenEncript))
            {
                NegarRequisicao(context);
                return;
            }

            try
            {
                var token = Encriptador.Get().Decriptar(EncriptEnum.ea16acb359604973ba2b17498ba2d8dc.GetName(), tokenEncript);
                var crossToken = JsonSerializer.Deserialize<CrossToken>(token);

                if(crossToken == null || crossToken.EstaValido() || !OrigemValida(context, crossToken))
                {
                    NegarRequisicao(context);
                    return;
                }
            }
            catch (Exception ex)
            {
                NegarRequisicao(context);
            }
        }
    }
}
