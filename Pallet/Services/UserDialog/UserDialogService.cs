using Pallet.Services.UserDialog.Interfaces;

namespace Pallet.Services.UserDialog;

public class UserDialogService : IUserDialogService
{
    #region Edit

    public bool Edit(object item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        return item switch
        {
            object => EditItem(item),
            _ => throw new ArgumentException("Not supported type"),
        };
    }

    private static bool EditItem(object item) => default;

    #endregion Edit

    #region Dialog

    public bool ConfirmInformation(string Information, string Caption) => MessageBox
        .Show(
            Information, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Information)
            == MessageBoxResult.Yes;

    public bool ConfirmWarning(string Warning, string Caption) => MessageBox
        .Show(
            Warning, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning)
            == MessageBoxResult.Yes;

    public bool ConfirmError(string Error, string Caption) => MessageBox
        .Show(
            Error, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Error)
            == MessageBoxResult.Yes;

    #endregion Dialog

    #region Show

    public void ShowInformation(string Information, string Caption) =>
        MessageBox.Show(
            Information, Caption,
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public void ShowWarning(string Warning, string Caption) =>
        MessageBox.Show(
            Warning, Caption,
            MessageBoxButton.OK,
            MessageBoxImage.Warning);

    public void ShowError(string Error, string Caption) =>
        MessageBox.Show(
            Error, Caption,
            MessageBoxButton.OK,
            MessageBoxImage.Error);

    #endregion Show
}