using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TransformacaoDigital.Mensageria;
using TransformacaoDigital.Mensageria.Dto;
using TransformacaoDigital.Mensageria.Middlewares;
using TransformacaoDigital.Mensageria.Services;
using TransformacaoDigital.Mensageria.Services.Implementations;
using TransformacaoDigital.MVC.Configuracoes.Dtos;

namespace TransformacaoDigital.MVC.WorkerHandlers
{
    public class EmailEventHandler : BackgroundService, IEventHandler
    {
        private IReceiveService _receiveService;
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly SendGridClient _clienteSendGrid;
        private SendGridConfig _sendGridConfig;

        public EmailEventHandler(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _sendGridConfig = AppSettings.SendGrid;
            _clienteSendGrid = new SendGridClient(AppSettings.SendGrid.APIKey);
        }

        public int ExecutionMillisecondsDelay => 60000;

        public QueueEnum QueueToProcess => QueueEnum.EnviarEmail;

        protected override string ServiceName => "Envio de Email";

        private void InicializarServicos()
        {
            var scope = _serviceProvider.CreateScope();

            _receiveService = new ReceiveService();
        }

        public CommandResponse HandlerMessage(string message)
        {
            try
            {
                var email = JsonSerializer.Deserialize<EmailDto>(message);

                if (email == null || email.Valido() == false)
                    return CommandResponse.Fail_Result;

                var mensagem = new SendGridMessage()
                {
                    From = new EmailAddress(_sendGridConfig.EmailEnvio),
                    Subject = email.Assunto,
                    PlainTextContent = email.Assunto,
                    HtmlContent = email.Mensagem
                };

                mensagem.AddTos(email.Destinatarios.Split(';').Select(x => new EmailAddress(x)).ToList());

                var task = Task.Run(() => _clienteSendGrid.SendEmailAsync(mensagem));

                task.Wait();

                return CommandResponse.OK_Result;
            }
            catch (Exception)
            {
                return CommandResponse.Fail_Result;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _receiveService = new ReceiveService();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    InicializarServicos();

                    _receiveService.Subscribe(this);

                    await Task.Delay(this.ExecutionMillisecondsDelay, stoppingToken);
                }
                catch (Exception ex)
                {
                    var t = ex.Message;
                }
            }
        }
    }
}
