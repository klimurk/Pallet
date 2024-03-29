﻿namespace Pallet.Infrastructure.Commands.Base;

/// <summary>
/// The command base class
/// </summary>
internal abstract class Command : DependencyObject, ICommand
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
        }
    }

    public event EventHandler? ExecutableChanged;

    public event EventHandler? CanExecuteChanged
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
    /// Executed the command by interface.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    void ICommand.Execute(object parameter)
    {
        if (!((ICommand)this).CanExecute(parameter)) return;
        Execute(parameter);
    }

    /// <summary>
    /// Can the command be execute.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>A bool.</returns>
    protected virtual bool CanExecute(object p) => true;

    /// <summary>
    /// Executes the command
    /// </summary>
    /// <param name="p">The p.</param>
    protected abstract void Execute(object p);
}