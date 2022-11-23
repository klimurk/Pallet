using MaterialDesignThemes.Wpf;
using Pallet.BaseDatabase.Base.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.View.Dialogs;
using Pallet.ViewModels.Windows;

namespace Pallet.Services.UserDialog;

public class UserDialogService : IUserDialogService
{
    public SnackbarMessageQueue MessageQueue { get; private set; }

    public UserDialogService()
    {
        MessageQueue = new(TimeSpan.FromSeconds(10), Application.Current.Dispatcher) { DiscardDuplicates = true };
    }

    #region Dialog

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

    //public bool ConfirmInformation(string Information, string Caption) => MessageBox
    //    .Show(
    //        Information, Caption,
    //        MessageBoxButton.YesNo,
    //        MessageBoxImage.Information)
    //        == MessageBoxResult.Yes;
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
        return (bool)await DialogHost.Show(view, MainWindow.DialogName);
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

    //public bool ConfirmWarning(string Warning, string Caption) => MessageBox
    //    .Show(
    //        Warning, Caption,
    //        MessageBoxButton.YesNo,
    //        MessageBoxImage.Warning)
    //        == MessageBoxResult.Yes;
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
        return (bool)await DialogHost.Show(view, MainWindow.DialogName);
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

    //public bool ConfirmError(string Error, string Caption) => MessageBox
    //    .Show(
    //        Error, Caption,
    //        MessageBoxButton.YesNo,
    //        MessageBoxImage.Error)
    //        == MessageBoxResult.Yes;
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
        return (bool)await DialogHost.Show(view, MainWindow.DialogName);
    }

    #endregion Dialog

    #region Show

    public void ShowInformation(IDBTranslateble Information)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowInformation(Information.DescriptionEn);

                break;

            case "de":
                ShowInformation(Information.DescriptionDe);

                break;

            default:
                ShowInformation(Information.DescriptionLocal);

                break;
        }
    }

    public void ShowInformation(string Information)
    {
        ShowSnackBar(Information);
    }

    public void ShowWarning(IDBTranslateble Warning)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowWarning(Warning.DescriptionEn);
                break;

            case "de":
                ShowWarning(Warning.DescriptionDe);
                break;

            default:
                ShowWarning(Warning.DescriptionLocal);
                break;
        }
    }

    public void ShowWarning(string Warning)
    {
        ShowSnackBar(Warning);
    }

    public void ShowError(IDBTranslateble Error)
    {
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                ShowError(Error.DescriptionEn);
                break;

            case "de":
                ShowError(Error.DescriptionDe);
                break;

            default:
                ShowError(Error.DescriptionLocal);
                break;
        }
    }

    public void ShowError(string Error)
    {
        ShowSnackBar(Error);
    }

    private void ShowSnackBar(string Text)
    {

        Application.Current.Dispatcher.Invoke(() =>
        MessageQueue.Enqueue(Text, new PackIcon { Kind = PackIconKind.Alarm }, () => { }))
        ;
    }

    //public void ShowDialogInformation(string Information, string Caption)
    //{
    //    MessageBox.Show(
    //        Information, Caption,
    //        MessageBoxButton.OK,
    //        MessageBoxImage.Information);
    //}
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
        await DialogHost.Show(view, MainWindow.DialogName);
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

    //public async void ShowDialogWarning(string Warning, string Caption)
    //{
    //    MessageBox.Show(
    //        Warning, Caption,
    //        MessageBoxButton.OK,
    //        MessageBoxImage.Warning);
    //}
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
        await DialogHost.Show(view, MainWindow.DialogName);
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

    //public async void ShowDialogError(string Error, string Caption)
    //{
    //    MessageBox.Show(
    //        Error, Caption,
    //        MessageBoxButton.OK,
    //        MessageBoxImage.Error);
    //}
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
        await DialogHost.Show(view, MainWindow.DialogName);
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

    #endregion Show
}