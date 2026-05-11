namespace NewsLetter.Ai.Providers.Abstractions
{
    public interface IPromptProvider
    {
        Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken = default);
    }
}
