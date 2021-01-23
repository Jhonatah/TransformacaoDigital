using System;
using System.Collections.Generic;
using System.Linq;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class NotificacaoService : INotificacaoService
    {
        public List<Notificacao> notificacaos;

        public NotificacaoService()
        {
            this.notificacaos = new List<Notificacao>();
        }

        public IReadOnlyList<Notificacao> Listar()
        {
            return notificacaos;
        }

        public void NotificarErro(string mensagem)
        {
            notificacaos.Add(new Notificacao
            {
                Mensagem = mensagem
            });
        }

        public void NotificarErro(Exception ex, bool mostarMensagemInnerException = true)
        {
            if(mostarMensagemInnerException)
            {
                NotificarErro(ex.Message);
            }
        }

        public bool TemNotificacao()
        {
            return notificacaos != null && notificacaos.Any();
        }

        public void Dispose()
        {
            notificacaos = new List<Notificacao>();
        }
    }

    public struct Notificacao
    {
        public string Mensagem { get; set; }
    }
}
