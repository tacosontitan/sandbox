using Microsoft.Extensions.DependencyInjection;
using Sandbox.Core;

namespace Sandbox.Terminal;

/// <summary>
/// Defines extension methods for simplifying console injection.
/// </summary>
public static class TerminalExtensions
{
    /// <summary>
    /// Adds terminal services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with console services added.</returns>
    public static IServiceCollection AddTerminal(this IServiceCollection services) => services
        .AddTransient<IConsumerService, TerminalConsumer>()
        .AddHostedService<TerminalService>();
}
