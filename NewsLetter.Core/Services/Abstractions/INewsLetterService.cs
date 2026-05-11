namespace NewsLetter.Core.Services.Abstractions
{
    public interface INewsLetterService
    {
        Task SendAsync(CancellationToken cancellationToken = default);
    }
}
