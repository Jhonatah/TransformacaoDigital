using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios;
using TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios.Implementacoes;
using TransformacaoDigital.ConsultoriaAssessoria.API.Services;
using TransformacaoDigital.Filters.Middlewares;

namespace TransformacaoDigital.ConsultoriaAssessoria.API
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
            services.AddDbContext<ConsultoriaAssessoriaContexto>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

            services.AddHttpClient("apiautenticacao", x =>
            {
                x.BaseAddress = new Uri(Configuration["AppAutenticacao:HostName"]);
            });

            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("ConsultoriaAssessoria", new OpenApiInfo
                {
                    Title = "Rotas para Microsserviço de Consultorias e Assessorias.",
                    Version = "v1"
                });
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rotas para Microsserviço de Consultorias e Assessorias.",
                    Version = "v1"
                });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Token Credencial",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });


                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //x.IncludeXmlComments(xmlPath);
            });


            services.AddScoped<IContratoRepositorio, ContratoRepositorio>();
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<ValidarBearerMiddleWare>();

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Consultoria e Assessoria");

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