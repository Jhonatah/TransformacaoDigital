using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using TransformacaoDigital.Web.API.Enums;

namespace TransformacaoDigital.Web.API.Configuracoes
{
    public static class HttpsClientesConfigs
    {
        public static void AdicionarClientesHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(HttpClientesEnums.GetName<HttpClientesEnums>(HttpClientesEnums.Autenticacao), x =>
            {
                x.BaseAddress = new Uri(configuration["AppAutenticacao:HttpHost"]);
                x.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }
    }
}