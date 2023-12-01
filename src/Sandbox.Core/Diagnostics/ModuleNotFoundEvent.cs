namespace Sandbox.Core.Diagnostics;

/// <summary>
/// Represents that no module was found.
/// </summary>
/// <param name="specifiedKey">The key that was specified.</param>
/// <param name="consumerService">A service for interacting with consumers.</param>
public class ModuleNotFoundEvent(
    string specifiedKey,
    IConsumerService consumerService)
    : IEvent
{
    /// <inheritdoc/>
    public async Task Invoke(CancellationToken cancellationToken = default)
    {
        string message = $"No module was found with the specified key `{specifiedKey}`.";
        await consumerService
            .Whisper(message, cancellationToken)
            .ConfigureAwait(false);
    }
}
