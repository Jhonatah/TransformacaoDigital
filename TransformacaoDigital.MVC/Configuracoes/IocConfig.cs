using Microsoft.Extensions.DependencyInjection;
using TransformacaoDigital.MVC.Services;
using TransformacaoDigital.MVC.Services.Implementacoes;

namespace TransformacaoDigital.MVC.Configuracoes
{
    public static class IocConfig
    {
        public static void SetIocServices(this IServiceCollection services)
        {
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<INormaService, NormaService>();
            services.AddScoped<INotificacaoService, NotificacaoService>();
            services.AddScoped<IServicosHttpBase, ServicosHttpBase>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
