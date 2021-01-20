using Microsoft.Extensions.DependencyInjection;
using TransformacaoDigital.Web.API.Services;
using TransformacaoDigital.Web.API.Services.Implementacoes;

namespace TransformacaoDigital.Web.API.Configuracoes
{
    public static class IocServicesConfig
    {
        public static void AdicionarIocServices(this IServiceCollection services)
        {
            //Autenticacao Services
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
        }
    }
}