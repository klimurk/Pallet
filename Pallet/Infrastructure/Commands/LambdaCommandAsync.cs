using Pallet.Infrastructure.Commands.Base;

namespace Pallet.Infrastructure.Commands;

/// <summary>
/// The lambda command async for visualization.
/// </summary>
internal class LambdaCommandAsync : Command
{
    private readonly ActionAsync<object> _Execute;
    private readonly Func<object, bool> _CanExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaCommandAsync"/> class.
    /// </summary>
    /// <param name="Execute">The execute.</param>
    /// <param name="CanExecute">The can execute.</param>
    public LambdaCommandAsync(ActionAsync Execute, Func<bool> CanExecute = null)
        : this(async p => await Execute(), CanExecute is null ? null : p => CanExecute())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaCommandAsync"/> class.
    /// </summary>
    /// <param name="Execute">The execute.</param>
    /// <param name="CanExecute">The can execute.</param>
    public LambdaCommandAsync(ActionAsync<object> Execute, Func<object, bool>? CanExecute = null)
    {
        _Execute = Execute;
        _CanExecute = CanExecute;
    }

    /// <summary>
    /// Can execute the command.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>A bool.</returns>
    protected override bool CanExecute(object p) => _CanExecute?.Invoke(p) ?? true;

    /// <summary>
    /// Function for command.
    /// </summary>
    /// <param name="p">The p.</param>
    protected override void Execute(object p) => _Execute(p);
}