using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsLetter.Core.Agents.Abstractions;
using NewsLetter.Core.Enums;
using NewsLetter.Core.Models;
using NewsLetter.Core.Repositories.Abstractions;
using NewsLetter.Core.Services.Abstractions;

namespace NewsLetter.Infra.Services
{
    public class NewsLetterService(
                                    ILogger<NewsLetterService> logger,
                                    IArticleRepository articleRepository,

                                    [FromKeyedServices(AgentType.NewsLetterGenerator)]
                                    IAgent<IEnumerable<Article>, string> titleGenerateAgent,

                                    ISubscriberRepository subscriberRepository,
                                    IEmailService emailService)
                                    : INewsLetterService
    {
        public async Task SendAsync(CancellationToken cancellationToken = default)
        {
            //Recupera os post da semana
            logger.LogInformation("Recuperando os posts da semana...");

            var posts = await articleRepository.GetFromLastWeekAsync(cancellationToken);

            if (!posts.Any())
                return;

            //Gera título para a newsletter
            logger.LogInformation("Gerando título para a newsletter...");
            var subject = await titleGenerateAgent.RunAsync(posts, cancellationToken);

            //Gera o conteúdo da newsletter
            logger.LogInformation("Gerando conteúdo para a newsletter...");
            var body = await titleGenerateAgent.RunAsync(posts, cancellationToken);

            //Redupera os inscritos
            logger.LogInformation("Recuperando os inscritos...");
            var subscribers = await subscriberRepository.GetAllAsync(cancellationToken);

            //Envia email
            logger.LogInformation("Enviando email para inscritos...");

            foreach (var subscriber in subscribers)
                await emailService.SendAsync(subscriber.Name, subscriber.Email, subject, body, cancellationToken);

            logger.LogInformation("Serviço finalizado.");
        }
    }
}
