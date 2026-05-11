using Microsoft.Extensions.DependencyInjection;
using NewsLetter.Ai.Agents;
using NewsLetter.Ai.Providers;
using NewsLetter.Ai.Providers.Abstractions;
using NewsLetter.Core.Agents.Abstractions;
using NewsLetter.Core.Enums;
using NewsLetter.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsLetter.Ai
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAgents (this IServiceCollection services)
        {
            services.AddKeyedTransient<IAgent<IEnumerable<Article>, string>, TitlteGeneratorAgent>(AgentType.TitleGenerator);
            services.AddKeyedTransient<IAgent<IEnumerable<Article>, string>, NewsLetterGeneratorAgent>(AgentType.NewsLetterGenerator);
            services.AddKeyedTransient<IPromptProvider, FilePromptProvider>(PromptProvider.File);

            return services;
        }
    }
}
