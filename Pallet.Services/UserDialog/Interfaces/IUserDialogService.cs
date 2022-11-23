using MaterialDesignThemes.Wpf;
using Pallet.BaseDatabase.Base.Interfaces;

namespace Pallet.Services.UserDialog.Interfaces
{
    public interface IUserDialogService
    {
        //SynchronizationContext _syncContext { get; set; }
        //Dispatcher dispatcher { get; set; }
        SnackbarMessageQueue MessageQueue { get; }

        Task<bool> ConfirmError(IDBTranslateble Error, IDBTranslateble Caption);

        Task<bool> ConfirmError(string Error, string Caption);

        Task<bool> ConfirmInformation(IDBTranslateble Information, IDBTranslateble Caption);

        Task<bool> ConfirmInformation(string Information, string Caption);

        Task<bool> ConfirmWarning(IDBTranslateble Warning, IDBTranslateble Caption);

        Task<bool> ConfirmWarning(string Warning, string Caption);


        void ShowDialogError(IDBTranslateble Error, IDBTranslateble Caption);

        void ShowDialogError(string Error, string Caption);

        void ShowDialogInformation(IDBTranslateble Information, IDBTranslateble Caption);

        void ShowDialogInformation(string Information, string Caption);

        void ShowDialogWarning(IDBTranslateble Warning, IDBTranslateble Caption);

        void ShowDialogWarning(string Warning, string Caption);

        void ShowError(IDBTranslateble Error);

        void ShowError(string Error);

        void ShowInformation(IDBTranslateble Information);

        void ShowInformation(string Information);

        void ShowWarning(IDBTranslateble Warning);

        void ShowWarning(string Warning);
    }
}