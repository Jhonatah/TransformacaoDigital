using System;
using System.Collections.Generic;
using TransformacaoDigital.MVC.Services.Implementacoes;

namespace TransformacaoDigital.MVC.Services
{
    public interface INotificacaoService : IDisposable
    {
        void NotificarErro(string mensagem);
        void NotificarErro(Exception ex, bool mostarMensagemInnerException = true);

        IReadOnlyList<Notificacao> Listar();

        bool TemNotificacao();
    }
}