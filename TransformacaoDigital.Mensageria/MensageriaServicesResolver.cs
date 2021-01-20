using Microsoft.Extensions.DependencyInjection;
using TransformacaoDigital.Mensageria.Services;
using TransformacaoDigital.Mensageria.Services.Implementations;

namespace TransformacaoDigital.Mensageria
{
    public static class MensageriaServicesResolver
    {
        private static void _RegiterService(IServiceCollection services)
        {
            services.AddScoped<ISenderService, SenderService>();
            services.AddScoped<IReceiveService, ReceiveService>();
        }

        public static void RegisterMoMServices(
            this IServiceCollection services, 
            ConfigurationServer configurationServer)
        {
            ConfigurationServerRabbitMQ.SetConfig(
                configurationServer.HostName,
                configurationServer.port,
                configurationServer.UserName,
                configurationServer.Password);

            _RegiterService(services);
        }
    }

    public class ConfigurationServer
    {
        public string HostName { get; set; }
        public int port { get; set; } = 5672;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}