namespace Pallet.Services.UserDialog.Interfaces
{
    public interface IUserDialogService
    {
        bool Edit(object item);

        public bool ConfirmWarning(string Warning, string Caption);

        public bool ConfirmError(string Error, string Caption);

        public bool ConfirmInformation(string Information, string Caption);

        public void ShowInformation(string Information, string Caption);

        public void ShowWarning(string Warning, string Caption);

        public void ShowError(string Error, string Caption);
    }
}