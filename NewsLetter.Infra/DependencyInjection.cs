using Microsoft.Extensions.DependencyInjection;
using NewsLetter.Core.Repositories.Abstractions;
using NewsLetter.Core.Services.Abstractions;
using NewsLetter.Infra.Repositories;
using NewsLetter.Infra.Services;

namespace NewsLetter.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INewsLetterService, NewsLetterService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();

            return services;
        }
    }
}
