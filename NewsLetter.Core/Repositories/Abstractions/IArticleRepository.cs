using NewsLetter.Core.Models;

namespace NewsLetter.Core.Repositories.Abstractions
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetFromLastWeekAsync(CancellationToken cancellationToken = default);
    }
}
