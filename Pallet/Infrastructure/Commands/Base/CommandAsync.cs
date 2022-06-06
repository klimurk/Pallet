namespace Pallet.Infrastructure.Commands.Base;

/// <summary>
/// The command base async
/// </summary>
internal abstract class CommandAsync : ICommand
{
    private bool _Executable = true;

    /// <summary>
    /// Gets or sets a value indicating whether executable.
    /// </summary>
    public bool Executable
    {
        get => _Executable;
        set
        {
            if (_Executable == value) return;
            _Executable = value;
            ExecutableChanged?.Invoke(this, EventArgs.Empty);
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public event EventHandler ExecutableChanged;

    event EventHandler ICommand.CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Can execute the command by interface.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>A bool.</returns>
    bool ICommand.CanExecute(object parameter) => _Executable && CanExecute(parameter);

    /// <summary>
    /// Executes the command by interface.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    async void ICommand.Execute(object parameter)
    {
        if (!((ICommand)this).CanExecute(parameter)) return;
        try
        {
            Executable = false;
            await ExecuteAsync(parameter);
        }
        catch
        {
            Executable = true;
            throw;
        }
    }

    /// <summary>
    /// Can the command executed.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>A bool.</returns>
    protected virtual bool CanExecute(object p) => true;

    /// <summary>
    /// Executes command async.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>A Task.</returns>
    protected abstract Task ExecuteAsync(object p);
}