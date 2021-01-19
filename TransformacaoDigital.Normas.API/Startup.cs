using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TransformacaoDigital.Filters.Middlewares;
using TransformacaoDigital.Mensageria;
using TransformacaoDigital.Normas.API.Repositorios;
using TransformacaoDigital.Normas.API.Repositorios.Implementacoes;
using TransformacaoDigital.Normas.API.Services;
using TransformacaoDigital.Normas.API.Services.Implementacoes;

namespace TransformacaoDigital.Normas.API
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
            services.AddDbContext<NormasContexto>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped<INormasRepositorio, NormasRepositorio>();
            services.AddScoped<INormaService, NormaService>();

            services.RegisterMoMServices(new ConfigurationServer
            {
                HostName = Configuration["RabbitMQConfig:HostName"],
                UserName = Configuration["RabbitMQConfig:UserName"],
                Password = Configuration["RabbitMQConfig:Password"],
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ValidarBearerMeddleWare>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
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
