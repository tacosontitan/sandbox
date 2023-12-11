using Microsoft.Extensions.DependencyInjection;
using Sandbox.Core;

namespace Sandbox.Terminal;

/// <summary>
/// Represents a console that writes to the standard output stream.
/// </summary>
public class TerminalConsumer
    : IConsumerService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TerminalConsumer"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public TerminalConsumer(IServiceProvider serviceProvider) =>
        ServiceProvider = serviceProvider;

    /// <inheritdoc/>
    public IServiceProvider ServiceProvider { get; }

    /// <inheritdoc />
    public Task<T> PromptForInput<T>(
        string prompt,
        CommunicationLevel communicationLevel,
        CancellationToken cancellationToken = default)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            return Task.FromResult<T>(default!);

        T result = (T)Convert.ChangeType(input, typeof(T));
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task SendMessage(string message, CommunicationLevel communicationLevel, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Invoke<TEvent>(CancellationToken cancellationToken = default) where TEvent : IEvent =>
        Invoke<TEvent>(cancellationToken);

    /// <inheritdoc />
    public Task Invoke<TEvent>(object? data, CancellationToken cancellationToken = default)
        where TEvent : IEvent =>
        Invoke<TEvent>(cancellationToken, data!);

    private async Task Invoke<TEvent>(CancellationToken cancellationToken, params object[] parameters) where TEvent : IEvent
    {
        TEvent @event = ActivatorUtilities.CreateInstance<TEvent>(ServiceProvider, parameters);
        await @event.Invoke(cancellationToken);
    }
}
