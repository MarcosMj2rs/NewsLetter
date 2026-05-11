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
    public class TitlteGeneratorAgent(ILogger<TitlteGeneratorAgent> logger,
                                      [FromKeyedServices(PromptProvider.File)] IPromptProvider promptProvider)
                                      : IAgent<IEnumerable<Article>, string>
    {
        private const string AgentName = "TitleGeneratorAgent";
        private const string Prompt = "Gere um título para newasletter semanal com base neste JSON: ";
        private const float Temperature = 0.7f;

        public async Task<string> RunAsync(IEnumerable<Article> data, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Gerando o título da newsletter...", data.Count());

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
                                  Description = "Agente especialisa em gerar título para newsletter via E-mail",
                                  ChatOptions = new ChatOptions
                                  {
                                      ModelId = AiModels.GEMINI_20_FLASH,
                                      Temperature = Temperature,
                                      Instructions = instructions,
                                      MaxOutputTokens = 1000
                                  }
                              });

            //var prompt = $"{Prompt} {JsonSerializer.Serialize(data)}";
            var simplified = data.Select(a => new { a.Title, a.Content, a.url });
            var prompt = $"{Prompt} {JsonSerializer.Serialize(simplified)}";
            var response = await agent.RunAsync<string>(message: prompt, cancellationToken: cancellationToken);

            logger.LogInformation("Título da newsletter gerado com sucesso!");
            logger.LogInformation("---");
            logger.LogInformation(response.Result);
            logger.LogInformation("---");

            return response.Result;
        }
    }
}
