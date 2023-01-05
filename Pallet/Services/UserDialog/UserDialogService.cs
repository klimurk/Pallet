using MaterialDesignThemes.Wpf;
using Pallet.BaseDatabase.Base.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.View.Dialogs;
using Pallet.View.SubViews;
using Pallet.ViewModels.Windows;

namespace Pallet.Services.UserDialog;

public class UserDialogService : IUserDialogService
{
    public SnackbarMessageQueue MessageQueue { get; private set; } = new(TimeSpan.FromSeconds(10), Application.Current.Dispatcher) { DiscardDuplicates = true };

    #region DialogHost messages

    public async Task<bool> ConfirmInformation(IDBTranslateble Information, IDBTranslateble Caption)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                return await ConfirmInformation(
                    Information.DescriptionEn, Caption.DescriptionEn);

            case "de":

                return await ConfirmInformation(
                    Information.DescriptionDe, Caption.DescriptionDe);

            default:

                return await ConfirmInformation(
                    Information.DescriptionLocal, Caption.DescriptionLocal);
        }
    }

    public async Task<bool> ConfirmInformation(string Information, string Caption)
    {
        var view = new SimpleDialog
        {
            DataContext = new SimpleDialogViewModel()
            {
                IsConfirm = true,
                Caption = Caption,
                Message = Information,
            }
        };
        return (bool)await DialogHost.Show(view, MainControl.DialogName);
    }

    public async Task<bool> ConfirmWarning(IDBTranslateble Warning, IDBTranslateble Caption)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                return await ConfirmWarning(Warning.DescriptionEn, Caption.DescriptionEn);

            case "de":

                return await ConfirmWarning(Warning.DescriptionDe, Caption.DescriptionDe);

            default:

                return await ConfirmWarning(Warning.DescriptionLocal, Caption.DescriptionLocal);
        }
    }

    public async Task<bool> ConfirmWarning(string Warning, string Caption)
    {
        var view = new SimpleDialog
        {
            DataContext = new SimpleDialogViewModel()
            {
                IsConfirm = true,
                Caption = Caption,
                Message = Warning,
                IsWarning = true
            }
        };
        return (bool)await DialogHost.Show(view, MainControl.DialogName);
    }

    public async Task<bool> ConfirmError(IDBTranslateble Error, IDBTranslateble Caption)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                return await ConfirmError(Error.DescriptionEn, Caption.DescriptionEn);

            case "de":

                return await ConfirmError(Error.DescriptionDe, Caption.DescriptionDe);

            default:

                return await ConfirmError(Error.DescriptionLocal, Caption.DescriptionLocal);
        }
    }

    public async Task<bool> ConfirmError(string Error, string Caption)
    {
        var view = new SimpleDialog
        {
            DataContext = new SimpleDialogViewModel()
            {
                IsConfirm = true,
                Caption = Caption,
                Message = Error,
                IsError = true
            }
        };
        return (bool)await DialogHost.Show(view, MainControl.DialogName);
    }


    public async void ShowDialogInformation(string Information, string Caption)
    {
        var view = new SimpleDialog
        {
            DataContext = new SimpleDialogViewModel()
            {
                Caption = Caption,
                Message = Information
            }
        };
        await DialogHost.Show(view, MainControl.DialogName);
    }

    public async void ShowDialogInformation(IDBTranslateble Information, IDBTranslateble Caption)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowDialogInformation(Information.DescriptionEn, Caption.DescriptionEn);

                break;

            case "de":
                ShowDialogInformation(Information.DescriptionDe, Caption.DescriptionDe);

                break;

            default:
                ShowDialogInformation(Information.DescriptionLocal, Caption.DescriptionLocal);

                break;
        }
    }

    public async void ShowDialogWarning(string Message, string Caption)
    {
        var view = new SimpleDialog
        {
            DataContext = new SimpleDialogViewModel()
            {
                Caption = Caption,
                Message = Message,
                IsWarning = true
            }
        };
        await DialogHost.Show(view, MainControl.DialogName);
    }

    public async void ShowDialogWarning(IDBTranslateble Message, IDBTranslateble Caption)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowDialogWarning(Message.DescriptionEn, Caption.DescriptionEn);
                break;

            case "de":
                ShowDialogWarning(Message.DescriptionDe, Caption.DescriptionDe);
                break;

            default:
                ShowDialogWarning(Message.DescriptionLocal, Caption.DescriptionLocal);
                break;
        }
    }

    public async void ShowDialogError(string Message, string Caption)
    {
        var view = new SimpleDialog
        {
            DataContext = new SimpleDialogViewModel()
            {
                Caption = Caption,
                Message = Message,
                IsError = true
            }
        };
        await DialogHost.Show(view, MainControl.DialogName);
    }

    public async void ShowDialogError(IDBTranslateble Message, IDBTranslateble Caption)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowDialogError(Message.DescriptionEn, Caption.DescriptionEn);
                break;

            case "de":
                ShowDialogError(Message.DescriptionDe, Caption.DescriptionDe);
                break;

            default:
                ShowDialogError(Message.DescriptionLocal, Caption.DescriptionLocal);
                break;
        }
    }
    #endregion DialogHost messages

    #region Snackbars

    public void ShowSnackbarInfo(IDBTranslateble Information)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowSnackbarInfo(Information.DescriptionEn);

                break;

            case "de":
                ShowSnackbarInfo(Information.DescriptionDe);

                break;

            default:
                ShowSnackbarInfo(Information.DescriptionLocal);

                break;
        }
    }

    public void ShowSnackbarInfo(string Information) => ShowSnackBar(new MessageToSnack() { Content = Information, Level = MessageToSnackLevel.Info, Duration = new(0, 0, 30), WithCloseButton = true });

    public void ShowSnackbarWarn(IDBTranslateble Warning)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowSnackbarWarn(Warning.DescriptionEn);
                break;

            case "de":
                ShowSnackbarWarn(Warning.DescriptionDe);
                break;

            default:
                ShowSnackbarWarn(Warning.DescriptionLocal);
                break;
        }
    }

    public void ShowSnackbarWarn(string Warning)
    {
        ShowSnackBar(new MessageToSnack() { Content = Warning, Level = MessageToSnackLevel.Warning, Duration = new(0, 1, 0), WithCloseButton = false });
    }

    public void ShowSnackbarError(IDBTranslateble Error)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowSnackbarError(Error.DescriptionEn);
                break;

            case "de":
                ShowSnackbarError(Error.DescriptionDe);
                break;

            default:
                ShowSnackbarError(Error.DescriptionLocal);
                break;
        }
    }

    public void ShowSnackbarError(string Error) => ShowSnackBar(new MessageToSnack() { Content = Error, Level = MessageToSnackLevel.Error, Duration = new(0, 1, 0), WithCloseButton = false });

    private void ShowSnackBar(string Text)
    {
        Application.Current.Dispatcher.Invoke(() =>
        MessageQueue.Enqueue(Text, new PackIcon { Kind = PackIconKind.Close }, () => { }))
        ;
    }

    private void ShowSnackBar(MessageToSnack snakMsg)
    {
        _snackedMessages.Add(snakMsg);

        if (snakMsg.WithCloseButton)
        {
            Application.Current.Dispatcher.Invoke(() => MessageQueue.Enqueue(snakMsg.Content, new PackIcon() { Kind = PackIconKind.Close }, (queue) => { }, MessageQueue, false, false, snakMsg.Duration));
        }
        else
        {
            Application.Current.Dispatcher.Invoke(() => MessageQueue.Enqueue(snakMsg.Content, null, null, null, false, false, snakMsg.Duration));
        }
        OnNewSnackBar();
    }

    public event EventHandler NewSnackBarEventHandler;

    private void OnNewSnackBar() => NewSnackBarEventHandler?.Invoke(this, EventArgs.Empty);

    private List<MessageToSnack> _snackedMessages = new(); // used when intercepting a snackbar message poping , to find its reference and be able to change the color with a trigger on the CurrentLevel

    private SnackbarMessage _message;

    public SnackbarMessage Message
    {
        get => _message;
        set
        {
            _message = value;
            if (_message == null)
            {
                CurrentMessageLevel = 0;
                return;
            }
            var localMessage = _snackedMessages.FirstOrDefault(m => m.Content.Equals(_message.Content.ToString()));
            if (localMessage == null)
            {
                CurrentMessageLevel = 0;
                return;
            }
            CurrentMessageLevel = localMessage.Level;
            _snackedMessages.Remove(localMessage);
        }
    }

    private MessageToSnackLevel _CurrentMessageLevel;

    public MessageToSnackLevel CurrentMessageLevel
    {
        get => _CurrentMessageLevel; private set
        {
            _CurrentMessageLevel = value;
            OnNewSnackBar();
        }
    }

    #endregion Snackbars

    #region WindowsDialogs

    public bool ConfirmErrorWindowBox(string Error, string Caption) => MessageBox
        .Show(Error, Caption,MessageBoxButton.YesNo,MessageBoxImage.Error)== MessageBoxResult.Yes;

    public bool ConfirmWarningWindowBox(string Warning, string Caption) => MessageBox
        .Show(Warning, Caption,MessageBoxButton.YesNo,MessageBoxImage.Warning)== MessageBoxResult.Yes;

    public bool ConfirmInformationWindowBox(string Information, string Caption) => MessageBox
        .Show(Information, Caption,MessageBoxButton.YesNo,MessageBoxImage.Information) == MessageBoxResult.Yes;

    public void ShowDialogErrorWindowBox(string Error, string Caption) => MessageBox
        .Show(Error, Caption,MessageBoxButton.OK,MessageBoxImage.Error);

    public void ShowDialogWarningWindowBox(string Warning, string Caption) => MessageBox
        .Show(Warning, Caption, MessageBoxButton.OK, MessageBoxImage.Warning);

    public void ShowDialogInformationWindowBox(string Information, string Caption) => MessageBox
        .Show(Information, Caption, MessageBoxButton.OK, MessageBoxImage.Information);

    #endregion WindowsDialogs
}