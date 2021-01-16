using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TransformacaoDigital.Library;

namespace TransformacaoDigital.Filters.Middlewares
{
    public class ValidarBearerMeddleWare
    {
        private readonly RequestDelegate _next;
        private string _hostAPI;
        private string _hostRouteValidateToken;

        public ValidarBearerMeddleWare(RequestDelegate next)
        {
            _next = next;
        }

        private string GetToken(string tokenAutenticacao, IConfiguration configuration)
        {
            var service = new GeradorTokenCrossServices();
            string origem = configuration["OrigemDomain"];

            if (string.IsNullOrEmpty(origem))
                throw new ArgumentNullException("Objeto OrigemDomain não definido no appSettings");

            return tokenAutenticacao;// service.GetToken(tokenAutenticacao, origem);
        }

        private HttpClient _GenerateCliente(string tokenAutenticacao, IConfiguration configuration)
        {
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_hostAPI);
            cliente.DefaultRequestHeaders
                .Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetToken(tokenAutenticacao.Replace("Bearer ", ""), configuration));

            return cliente;
        }

        private void _SetVariaveis(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("Objeto IConfiguration não definido");

            _hostAPI = configuration["AppAutenticacao:HostName"];
            _hostRouteValidateToken = configuration["AppAutenticacao:RouteValidateToken"];

            if (string.IsNullOrEmpty(_hostAPI))
                throw new ArgumentNullException("Objeto AppAutenticacao:HostName não definido no appSettings");

            if (string.IsNullOrEmpty(_hostRouteValidateToken))
                throw new ArgumentNullException("Objeto AppAutenticacao:RouteValidateToken não definido no appSettings");

        }

        private void RejeitarRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path.Value.Contains("swagger"))
            {
                await _next.Invoke(context);
                return;
            }

            var config = (IConfiguration)context.RequestServices.GetService(typeof(IConfiguration));

            _SetVariaveis(config);

            var autorizacao = context.Request.Headers[HeaderNames.Authorization];

            if (string.IsNullOrEmpty(autorizacao))
            {
                RejeitarRequest(context);
                return;
            }

            var cliente = _GenerateCliente(autorizacao, config);

            var task = await cliente.PostAsync(_hostRouteValidateToken, null);

            if (task.StatusCode != HttpStatusCode.OK)
            {
                RejeitarRequest(context);
                return;
            }
            else await _next.Invoke(context);
        }
    }
}
