using System.Runtime.CompilerServices;

namespace Pallet.ViewModels.Base;

public abstract class ViewModel : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void Dispose()
    {
        Dispose(true);
    }

    private bool _Disposed;

    public ViewModel()
    {
        //AsyncInitialization().ConfigureAwait(false);
    }

    protected virtual Task AsyncInitialization() => new Task(() => { });

    protected virtual void Dispose(bool Disposing)
    {
        if (!Disposing || _Disposed) return;
        _Disposed = true;
    }

    ~ViewModel()
    {
        Dispose(false);
    }

    protected virtual void RefreshEvent(object? sender, EventArgs e) => OnPropertyChanged(default);

    protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

    protected virtual bool Set<T>(ref T? field, T? value, [CallerMemberName] string? PropertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }
}