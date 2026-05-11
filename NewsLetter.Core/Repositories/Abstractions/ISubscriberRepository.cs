using NewsLetter.Core.Models;

namespace NewsLetter.Core.Repositories.Abstractions
{
    public interface ISubscriberRepository
    {
        Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
