using System;
using System.Net.Http;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class ServicosHttpBase : IServicosHttpBase
    {
        public ServicosHttpBase(IHttpClientFactory httpClientFactory, INotificacaoService notificacaoService, IUsuarioService usuarioService)
        {
            HttpClient = httpClientFactory.CreateClient("apigateway");

            NotificacaoService = notificacaoService;
            UsuarioService = usuarioService;

            if (UsuarioService == null)
                throw new ArgumentNullException("Implementação de IUsuarioService nula");
            if (NotificacaoService == null)
                throw new ArgumentNullException("Implementação de INotificacaoService nula");
        }

        public HttpClient HttpClient { get; private set; }

        public INotificacaoService NotificacaoService { get; private set; }

        public IUsuarioService UsuarioService { get; private set; }
    }
}