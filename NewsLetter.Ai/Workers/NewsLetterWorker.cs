using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsLetter.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsLetter.Ai.Workers
{
    public class NewsLetterWorker(
        ILogger<NewsLetterWorker> logger,
        IServiceScopeFactory scopeFactory) : BackgroundService
    {
        private readonly TimeSpan _scheduleTime = new(8, 0, 0); // 8:00 AM
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = scopeFactory.CreateScope();

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = GetNextSundayAtEight(now);

                //var delay = nextRun - now;
                var delay = TimeSpan.FromSeconds(5);
                logger.LogInformation("Próxima newsletter agendada para: {NextRun} (in {Delay})", nextRun, delay);

                try
                {
                    await Task.Delay(delay, stoppingToken);

                    logger.LogInformation("Task executada Domingo as: {Time}", DateTime.Now);
                    await DoWorkAsync(stoppingToken);
                }
                catch (OperationCanceledException) { break; }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

            }
        }

        private DateTime GetNextSundayAtEight(DateTime current)
        {
            var daysUntilSunday = ((int)DayOfWeek.Sunday - (int)current.DayOfWeek + 7) % 7;
            var nextSunday = current.Date.AddDays(daysUntilSunday).Add(_scheduleTime);

            if(nextSunday <= current)
                nextSunday = nextSunday.AddDays(7);

            return nextSunday;
        }

        private async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Iniciando o Worker...");
            using var scope = scopeFactory.CreateScope();
            var newsLetterService = scope.ServiceProvider.GetRequiredService<INewsLetterService>();
            await newsLetterService.SendAsync(cancellationToken);
        }
    }
}
