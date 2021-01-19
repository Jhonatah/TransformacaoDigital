using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransformacaoDigital.Mensageria.Services;
using TransformacaoDigital.Mensageria.Services.Implementations;

namespace TransformacaoDigital.Mensageria.Middlewares
{
    public class MensageriaWorkerBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IReceiveService _receiveService;
        //private readonly ConfigQueueReceive _config;
        private IEventHandler _eventHandler;
        //private readonly IServiceScopeFactory _service;

        public MensageriaWorkerBackgroundService(//IServiceScopeFactory service,
            ILoggerFactory loggerFactory,
            IEventHandler eventHandler
           )
        {
            _logger = loggerFactory.CreateLogger<MensageriaWorkerBackgroundService>();
            _receiveService = new ReceiveService();

            _eventHandler = eventHandler;
            if (_eventHandler is null)
                new ArgumentNullException("Not difined instance of IEventHandler to MensageriaWorkerBackgroundService");

        }

        protected override string ServiceName => "MensageriaWorkerBackgroundService";

        public object Configuration { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{ServiceName} está iniciando. ##Debug");

            //using (var nreScope = _service.CreateScope())
            //{
            //    _receiveService = nreScope.ServiceProvider.GetService<IReceiveService>();
            //    _eventHandler = nreScope.ServiceProvider.GetService<IEventHandler>();

            while (!stoppingToken.IsCancellationRequested)
            {
                _receiveService.Subscribe(_eventHandler);
                _logger.LogInformation("MEnsagem Recuperada");

                await Task.Delay(_eventHandler.ExecutionMillisecondsDelay, stoppingToken);
            }
            //}            
        }
    }

    public class ConfigQueueReceive
    {
        public QueueEnum QueueName { get; set; }
        public IEventHandler HandlerEvent { get; set; }
        public int ExecutionMillisecondsDelay { get; set; } = 5000;
    }
}