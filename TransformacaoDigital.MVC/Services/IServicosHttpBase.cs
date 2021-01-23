using System.Net.Http;

namespace TransformacaoDigital.MVC.Services
{
    public interface IServicosHttpBase
    {
        HttpClient HttpClient { get; }
        INotificacaoService NotificacaoService { get; }
        IUsuarioService UsuarioService { get; }
    }
}