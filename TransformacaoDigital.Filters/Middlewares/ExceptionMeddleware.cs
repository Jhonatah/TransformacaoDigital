using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TransformacaoDigital.Filters.Middlewares
{
    public class ExceptionMeddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMeddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var t = ex.Message;
                throw;
            }
        }
    }
}
