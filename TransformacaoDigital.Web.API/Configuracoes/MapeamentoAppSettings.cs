using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransformacaoDigital.Web.API.Configuracoes.Dtos;

namespace TransformacaoDigital.Web.API.Configuracoes
{
    public static class MapeamentoAppSettings
    {
        public static void AppSettingConfigIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppAutenticacao>(configuration.GetSection("AppAutenticacao"));
        }
    }
}