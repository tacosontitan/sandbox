namespace Sandbox;

/// <summary>
/// Represents a description for a <see cref="Module"/>.
/// </summary>
/// <param name="Key">The key for invoking the <see cref="Module"/>.</param>
/// <param name="Name">The display name of the <see cref="Module"/>.</param>
/// <param name="Description">The description of the <see cref="Module"/>.</param>
/// <param name="Type">The <see cref="System.Type"/> of the <see cref="Module"/>.</param>
public record ModuleDescription(string Key, string Name, string Description, Type Type);