namespace Sandbox;

/// <summary>
/// Represents a consumable module within the sandbox.
/// </summary>
public abstract class ConsumableModule {

    #region Properties

    /// <summary>
    /// The key that invokes the module.
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// The display name for the module.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Describes what the module does.
    /// </summary>
    public string Description { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Creates a new instance of <see cref="ConsumableModule" /> with the specified key, name, and description.
    /// </summary>
    /// <param name="key">The key that invokes the module.</param>
    /// <param name="name">The display name for the module.</param>
    /// <param name="description">Describes what the module does.</param>
    public ConsumableModule(string key, string name, string description) {
        Key = key;
        Name = name;
        Description = description;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Invoke the consumable module.
    /// </summary>
    public abstract void Invoke();

    #endregion

    #region Protected Methods

    /// <summary>
    /// Invokes the message ready event for any observers.
    /// </summary>
    /// <param name="message">The message to post.</param>
    protected void PostMessage(string message) => MessageReady?.Invoke(this, message);

    #endregion

    #region Events

    /// <summary>
    /// Invoked when a message is ready for consumption.
    /// </summary>
    public event EventHandler<string>? MessageReady;

    #endregion

}