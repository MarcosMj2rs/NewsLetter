using NewsLetter.Core.Models;
using NewsLetter.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsLetter.Infra.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        public async Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await Task.Delay(100, cancellationToken);
            return
                [
                    new Subscriber (Name: "John Doe", Email: "john.doe@example.com"),
                    new Subscriber (Name: "Maria Silva", Email: "maria.silva@example.com"),
                    new Subscriber (Name: "Carlos Santos", Email: "carlos.santos@example.com"),
                    new Subscriber (Name: "Ana Costa", Email: "ana.costa@example.com"),
                    new Subscriber (Name: "Pedro Oliveira", Email: "pedro.oliveira@example.com")
                ];
        }
    }
}
