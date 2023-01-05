using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.Windows;

public class SimpleDialogViewModel : ViewModel
{
    public string Message
    {
        get => _Message;
        set => Set(ref _Message, value);
    }

    private string _Message;

    public string Caption
    {
        get => _Caption;
        set => Set(ref _Caption, value);
    }

    private string _Caption;

    public bool IsWarning
    {
        get => _IsWarning;
        set => Set(ref _IsWarning, value);
    }

    private bool _IsWarning;

    public bool IsError
    {
        get => _IsError;
        set => Set(ref _IsError, value);
    }

    private bool _IsError;

    public bool IsConfirm
    {
        get => _IsConfirm;
        set => Set(ref _IsConfirm, value);
    }

    private bool _IsConfirm;
}