namespace ChainOfResponsibility.FizzBuzz;

/// <summary>
/// Represents the `fizz` case of fizz buzz as a <see cref="ChainedProcess{T}" />.
/// </summary>
internal sealed class FizzWorker : ChainedProcess<int> {
    
    #region Constructor

    /// <summary>
    /// Creates a new instance of <see cref="FizzWorker" />.
    /// </summary>
    public FizzWorker(ChainedProcess<int> processor) : base(processor) { }

    #endregion

    #region Public Methods

    /// <summary>
    /// If the input is a multiple of 3 then output `fizz`, otherwise pass to the chain.
    /// </summary>
    public override void Process(int value) {
        if (value % 3 == 0)
            OnProcessingComplete("fizz");
        else
            base.Process(value);
    }

    #endregion

}