namespace Sandbox.Core.Modules;

/// <summary>
/// Represents a core <see cref="Module"/>. 
/// </summary>
/// <param name="key">Specifies the module key.</param>
/// <param name="name">Specifies the module name.</param>
/// <param name="description">Specifies the module description.</param>
public abstract class CoreModule(string key, string name, string description)
    : Module(key, name, description);
