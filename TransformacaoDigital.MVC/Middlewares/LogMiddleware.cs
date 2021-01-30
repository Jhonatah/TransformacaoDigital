using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.Mensageria;
using TransformacaoDigital.Mensageria.Services;

namespace TransformacaoDigital.MVC.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
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

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                service.Send(QueueEnum.Exceptions,
                    new
                    {
                        Host = "MVC - Erros",
                        ex
                    });
                throw;
            }
        }
    }
}
