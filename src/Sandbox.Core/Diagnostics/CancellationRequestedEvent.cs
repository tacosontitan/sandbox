namespace Sandbox.Core.Diagnostics;

/// <summary>
/// Represents that cancellation has been requested.
/// </summary>
/// <param name="consumer">A consumer service for relaying cancellation information.</param>
public class CancellationRequestedEvent(
    IConsumerService consumer)
    : IEvent
{
    private const string Message = "The consumer requested cancellation, stopping execution.";
    
    /// <inheritdoc/>
    public async Task Invoke(CancellationToken cancellationToken = default) => await consumer
        .Whisper(Message, cancellationToken)
        .ConfigureAwait(false);
}
