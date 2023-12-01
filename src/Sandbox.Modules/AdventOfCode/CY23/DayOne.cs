using Sandbox.Core;
using Sandbox.Core.Diagnostics;

namespace Sandbox.Modules.AdventOfCode.CY23;

/// <summary>
/// Represents the day one event for Advent of Code 2023.
/// </summary>
/// <see href="https://adventofcode.com/2023/day/1" />
public class DayOne
    : Module
{
    private readonly IConsumerService _consumer;

    /// <summary>
    /// Initializes a new instance of the <see cref="DayOne"/> class.
    /// </summary>
    /// <param name="consumer">The consumer service.</param>
    public DayOne(IConsumerService consumer) : base(
        key: "aoc23-day1",
        name: "Advent of Code - Day 1 (Trebuchet)",
        description: "Solves the day one puzzle for Advent of Code 2023.") =>
        _consumer = consumer;

    /// <inheritdoc/>
    public override async Task Invoke(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            await _consumer.Invoke<CancellationRequestedEvent>(cancellationToken);
            return;
        }
        
        await _consumer.SendMessage("👋 Hello, world!", cancellationToken).ConfigureAwait(false);
    }
}
