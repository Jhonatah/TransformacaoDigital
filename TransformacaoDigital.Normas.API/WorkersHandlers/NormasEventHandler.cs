using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TransformacaoDigital.Mensageria;
using TransformacaoDigital.Mensageria.Middlewares;
using TransformacaoDigital.Mensageria.Services;
using TransformacaoDigital.Mensageria.Services.Implementations;
using TransformacaoDigital.Normas.API.Services;
using TransformacaoDigital.Normas.API.ViewModels;

namespace TransformacaoDigital.Normas.API.WorkersHandlers
{
    public class NormasEventHandler : BackgroundService, IEventHandler, IDisposable
    {
        private INormaService _normaService;
        private IReceiveService _receiveService;
        private readonly IServiceScopeFactory _serviceProvider;

        public NormasEventHandler(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public QueueEnum QueueToProcess => QueueEnum.Normas;

        public int ExecutionMillisecondsDelay => 10000;

        protected override string ServiceName => "NormasEventHandler";

        private void InicializarServicos()
        {
            var scope = _serviceProvider.CreateScope();
            
            _normaService = scope.ServiceProvider.GetService<INormaService>();
            _receiveService = new ReceiveService();
        }

        public CommandResponse HandlerMessage(string message)
        {

            var viewModel = JsonSerializer.Deserialize<NormaViewModel>(message);

            if (viewModel != null &&
                !string.IsNullOrEmpty(viewModel.Descricao) &&
                !string.IsNullOrEmpty(viewModel.Nome) &&
                viewModel.TipoNormaId > 0)
            {
                _normaService.Cadastrar(viewModel);

                return CommandResponse.OK_Result;
            }

            return CommandResponse.Fail_Result;
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
