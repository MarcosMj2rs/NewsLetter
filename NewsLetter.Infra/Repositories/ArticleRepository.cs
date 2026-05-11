using NewsLetter.Core.Models;
using NewsLetter.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsLetter.Infra.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<IEnumerable<Article>> GetFromLastWeekAsync(CancellationToken cancellationToken = default)
        {
            await Task.Delay(150, cancellationToken);

            return
                [
                    new Article
                    (
                        Id: Guid.NewGuid(),
                        Title: "Dapper: Mapeando consultas complexas",
                        url: "https://blog.balta.io/dapper-mapeando-consultas-complexas",
                        Content: "Learn how to map complex queries using Dapper in .NET",
                        PublishDate: DateTime.UtcNow.AddDays(-1)
                    ),
                    new Article
                    (
                        Id: Guid.NewGuid(),
                        Title: "Performance com Dapper: Boas práticas",
                        url: "https://blog.balta.io/dapper-performance",
                        Content: "Otimize suas consultas com Dapper e melhore a performance da sua aplicação",
                        PublishDate: DateTime.UtcNow.AddDays(-3)
                    ),
                    new Article
                    (
                        Id: Guid.NewGuid(),
                        Title: "Dapper vs Entity Framework: Comparativo",
                        url: "https://blog.balta.io/dapper-vs-entity-framework",
                        Content: "Conheça as diferenças entre Dapper e Entity Framework para escolher a melhor opção",
                        PublishDate: DateTime.UtcNow.AddDays(-5)
                    ),
                    new Article
                    (
                        Id: Guid.NewGuid(),
                        Title: "Transações e Batching com Dapper",
                        url: "https://blog.balta.io/dapper-transactions-batching",
                        Content: "Implemente transações seguras e batching eficiente com Dapper",
                        PublishDate: DateTime.UtcNow.AddDays(-7)
                    ),
                    new Article
                    (
                        Id: Guid.NewGuid(),
                        Title: "Mapeamento Avançado com Dapper",
                        url: "https://blog.balta.io/dapper-advanced-mapping",
                        Content: "Explore técnicas avançadas de mapeamento de dados com Dapper ORM",
                        PublishDate: DateTime.UtcNow.AddDays(-2)
                    )
                ];
        }
    }
}
