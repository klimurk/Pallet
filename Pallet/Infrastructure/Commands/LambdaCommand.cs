using Pallet.Infrastructure.Commands.Base;

namespace Pallet.Infrastructure.Commands;
/// <summary>
/// The lambda command for visualization.
/// </summary>
internal class LambdaCommand : Command
{
    private readonly Func<object, bool> _CanExecute;
    private readonly Action<object> _Execute;

    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaCommand"/> class.
    /// </summary>
    /// <param name="Execute">The execute.</param>
    /// <param name="CanExecute">The can execute.</param>
    public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
    {
        _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
        _CanExecute = CanExecute;
    }

    /// <summary>
    /// Can execute the command.
    /// </summary>
    /// <param name="p">The parameter.</param>
    /// <returns>A bool.</returns>
    protected override bool CanExecute(object p) => _CanExecute?.Invoke(p) ?? true;

    /// <summary>
    /// Function for command.
    /// </summary>
    /// <param name="p">The parameter.</param>
    protected override void Execute(object p) => _Execute(p);
}