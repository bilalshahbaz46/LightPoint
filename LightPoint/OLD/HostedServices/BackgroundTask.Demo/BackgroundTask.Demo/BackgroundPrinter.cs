using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Demo
{
    public class BackgroundPrinter : IHostedService
    {
        private ILogger<BackgroundPrinter> _logger;
        private IWorker _worker;

        public BackgroundPrinter(ILogger<BackgroundPrinter> logger, IWorker worker)
        {
            this._logger = logger;
            this._worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this._worker.DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Printing Worker Stopping");

            return Task.CompletedTask;
        }
    }
}
