using Microsoft.Extensions.Logging;

namespace Sandbox.Core.Diagnostics;

/// <summary>
/// Represents that invalid input was received.
/// </summary>
/// <param name="inputReceived">The input that was received.</param>
/// <param name="consumerService">A service for interacting with consumers.</param>
public class InvalidInputEvent(
    object? inputReceived,
    IConsumerService consumerService)
    : IEvent
{
    /// <inheritdoc/>
    public async Task Invoke(CancellationToken cancellationToken = default)
    {
        string dataString = inputReceived?.ToString() ?? "null";
        string message = $"The specified input `{dataString}` is invalid.";
        await consumerService
            .Whisper(message, cancellationToken)
            .ConfigureAwait(false);
    }
}
