using Pallet.Infrastructure.Commands.Base;

namespace Pallet.Infrastructure.Commands;

internal class LambdaCommand : Command
{
    private readonly Func<object, bool> _CanExecute;
    private readonly Action<object> _Execute;

    public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
    {
        _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
        _CanExecute = CanExecute;
    }

    protected override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

    protected override void Execute(object parameter) => _Execute(parameter);
}