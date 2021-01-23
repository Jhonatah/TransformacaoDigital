using Microsoft.Extensions.Configuration;
using TransformacaoDigital.MVC.Configuracoes.Dtos;

namespace TransformacaoDigital.MVC.Configuracoes
{
    public static class AppSettingsConfig
    {
        public static void SetObjetosAppSettings(this IConfiguration configuration)
        {
            AppSettings.AppGateway = configuration.GetSection("AppGateway").Get<AppGateway>();
        }
    }
}