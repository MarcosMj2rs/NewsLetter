using Microsoft.Extensions.Logging;
using NewsLetter.Core.Services.Abstractions;

namespace NewsLetter.Infra.Services
{
    public class EmailService(ILogger<EmailService> logger) : IEmailService
    {
        public async Task SendAsync(string toName,
                              string toEmail,
                              string subject,
                              string body,
                              CancellationToken cancellationToken = default)
        {
            // Simulate sending email
            await Task.Delay(200, cancellationToken);
            logger.LogInformation($"Enviando newsletter para {toName} ({toEmail}) com assunto '{subject}'");

        }
    }
}
