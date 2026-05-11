using NewsLetter.Ai.Providers.Abstractions;

namespace NewsLetter.Ai.Providers
{
    public class FilePromptProvider : IPromptProvider
    {
        public async Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken = default)
        {
            var assembly = typeof(FilePromptProvider).Assembly;

            var resourceName = $"NewsLetter.Ai.Prompts.{agentName}.md";

            await using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream is null)
                throw new FileNotFoundException($"Prompt file for agent '{agentName}' not found as embedded resource.");

            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync(cancellationToken);
        }
    }
}
