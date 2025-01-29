using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;


namespace Cronos_Samples
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProcessingService _processingService;
        private int _processFrequency;

        public Worker(ILogger<Worker> logger, IProcessingService processingService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _processingService = processingService ?? throw new ArgumentNullException(nameof(processingService));
            _processFrequency = 5000;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogDebug($"Begin {nameof(ExecuteAsync)}");
                await _processingService.RunAsync();
                _logger.LogDebug($"End {nameof(ExecuteAsync)}");
                await Task.Delay(_processFrequency, cancellationToken);
            }
        }
    }
}
