using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TransformacaoDigital.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostBuilderContext, config) =>
                {
                    config.AddJsonFile($"ocelot.json");
                })
                .ConfigureWebHostDefaults(config => 
                {
                    config.UseStartup<Startup>();
                });
    }
}