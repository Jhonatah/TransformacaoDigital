using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.Mensageria;
using TransformacaoDigital.Mensageria.Services;

namespace TransformacaoDigital.Filters.Middlewares
{
    public class LogarRequisicoesMiddleware
    {
        private readonly RequestDelegate _next;

        public LogarRequisicoesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var obj = context.RequestServices.GetService(typeof(ISenderService));

            if (obj == null)
            {
                await context.Response.WriteAsync("Injete a interface OSenderService");
                return;
            }

            var service = (ISenderService)obj;

            try
            {
                

                service.Send(QueueEnum.RequestsApiGateway, 
                    new 
                    { 
                        context.Request.Path,
                        context.Request.Query,
                        context.Request.Host,
                        context.Request.Headers
                    });

                await _next.Invoke(context);


                service.Send(QueueEnum.RequestsApiGateway,
                    new
                    {
                        context.Response.StatusCode,
                        context.Response.Headers
                    });
            }
            catch (Exception ex)
            {

                service.Send(QueueEnum.Exceptions,
                    new
                    {
                        Host = "Api.Gateway",
                        ex
                    });
                throw;
            }
        }
    }
}
