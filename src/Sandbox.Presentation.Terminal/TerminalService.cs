using Microsoft.Extensions.Hosting;

using Sandbox.Core;
using Sandbox.Core.Diagnostics;
using Sandbox.Core.Modules;

namespace Sandbox.Terminal;

/// <summary>
/// Defines a service for triggering sandbox modules.
/// </summary>
public class TerminalService
    : BackgroundService
{
    private readonly IConsumerService _consumerService;
    private readonly IEnumerable<Module> _modules;

    /// <summary>
    /// Initializes a new instance of the <see cref="TerminalService"/> class.
    /// </summary>
    /// <param name="consumerService">The consumerService to use for interacting with the consumer.</param>
    /// <param name="coreModules">All of the core modules available to the sandbox.</param>
    /// <param name="modules">All of the modules available to the sandbox.</param>
    public TerminalService(
        IConsumerService consumerService,
        IEnumerable<CoreModule> coreModules,
        IEnumerable<Module> modules)
    {
        _consumerService = consumerService;
        _modules = coreModules.Concat(modules);
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!await TryProcessIteration(stoppingToken))
                break;

            await Task.Delay(100, stoppingToken).ConfigureAwait(false);
        }
    }

    private async Task<bool> TryProcessIteration(CancellationToken cancellationToken)
    {
        string? input = await _consumerService.PromptForInput<string?>("Enter a command: ", cancellationToken);
        if (string.IsNullOrWhiteSpace(input))
        {
            await _consumerService.Invoke<InvalidInputEvent>(input, cancellationToken).ConfigureAwait(false);
            return true;
        }

        Module? module = _modules.FirstOrDefault(m => m.Key.Equals(input, StringComparison.OrdinalIgnoreCase));
        if (module is null)
        {
            await _consumerService.Invoke<ModuleNotFoundEvent>(input, cancellationToken).ConfigureAwait(false);
            return true;
        }

        await _consumerService.SendMessage($"Invoking module '{module.Key}'.", cancellationToken).ConfigureAwait(false);
        await module.Invoke(cancellationToken);
        return true;
    }
}
