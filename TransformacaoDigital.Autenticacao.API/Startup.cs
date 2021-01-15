using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TransformacaoDigital.Autenticacao.API.Configuracoes;
using TransformacaoDigital.Autenticacao.API.Repositorios;
using TransformacaoDigital.Autenticacao.API.Repositorios.Implementacoes;
using TransformacaoDigital.Autenticacao.API.Services;

namespace TransformacaoDigital.Autenticacao.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<UsuariosContexto>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Rotas para Microsservi�o de Autentica��o.",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //x.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<JWTService>();

            services.SetJWT(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Autentica��o");

                //c.SwaggerEndpoint("/swagger/ConsultoriaAssessoria/swagger.json", "API Consultoria e Assessoria");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
