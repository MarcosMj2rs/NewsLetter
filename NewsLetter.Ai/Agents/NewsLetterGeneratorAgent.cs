using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsLetter.Ai.Models;
using NewsLetter.Ai.Providers.Abstractions;
using NewsLetter.Core;
using NewsLetter.Core.Agents.Abstractions;
using NewsLetter.Core.Enums;
using NewsLetter.Core.Models;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json;

namespace NewsLetter.Ai.Agents
{
    public class NewsLetterGeneratorAgent(ILogger<NewsLetterGeneratorAgent> logger,
                                          [FromKeyedServices(PromptProvider.File)] IPromptProvider promptProvider)
                                          : IAgent<IEnumerable<Article>, string>
    {
        private const string AgentName = "NewsLetterGeneratorAgent";
        private const string Prompt = "Gere um conteúdo para newasletter com base neste JSON: ";
        private const float Temperature = 0.7f;

        public async Task<string> RunAsync(IEnumerable<Article> data, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Gerando o conteúdo da newsletter...", data.Count());

            var client = new OpenAIClient(new ApiKeyCredential(Configuration.OpenAi.ApiKey),
                new OpenAIClientOptions
                {
                    Endpoint = new Uri("https://generativelanguage.googleapis.com/v1beta/openai/")
                }
            );

            var instructions = await promptProvider.GetPromptAsync(AgentName, cancellationToken);

            var agent = client.GetChatClient(AiModels.GEMINI_20_FLASH)
                              .AsAIAgent(new ChatClientAgentOptions
                              {
                                  Name = AgentName,
                                  Description = "Agente especialisa em gerar conteúdo para newsletter via E-mail",
                                  ChatOptions = new ChatOptions
                                  {
                                      ModelId = AiModels.GEMINI_20_FLASH,
                                      Temperature = Temperature,
                                      Instructions = instructions,
                                      MaxOutputTokens = 1000
                                  }
                              });

            //var prompt = $"{Prompt} {JsonSerializer.Serialize(data)}";
            var simplified = data.Select(a => new { a.Title, a.url });
            var prompt = $"{Prompt} {JsonSerializer.Serialize(simplified)}";

            // Retry com backoff exponencial
            var maxRetries = 3;
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    var response = await agent.RunAsync<string>(message: prompt, cancellationToken: cancellationToken);

                    logger.LogInformation("Conteúdo da newsletter gerado com sucesso!");
                    logger.LogInformation("---");
                    logger.LogInformation(response.Result);
                    logger.LogInformation("---");

                    return response.Result;
                }
                catch (ClientResultException ex) when (ex.Status == 429)
                {
                    if (attempt == maxRetries - 1)
                    {
                        logger.LogError("Limite de requisições atingido após {MaxRetries} tentativas.", maxRetries);
                        throw;
                    }

                    var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt + 1)); // 2s, 4s, 8s
                    logger.LogWarning("Rate limit atingido. Aguardando {Delay}s antes de tentar novamente... (tentativa {Attempt}/{MaxRetries})",
                        delay.TotalSeconds, attempt + 1, maxRetries);

                    await Task.Delay(delay, cancellationToken);
                }
            }

            throw new Exception("Falha ao gerar conteúdo após múltiplas tentativas.");
        }
    }
}
