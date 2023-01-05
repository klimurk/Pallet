using CodingSeb.Localization;
using MaterialDesignThemes.Wpf;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.View.SubViews;
using Pallet.ViewModels.Base;
using System.Windows.Controls;

namespace Pallet.ViewModels.Windows
{
    internal class LoginViewModel : ViewModel
    {
        #region Services

        private readonly IUserDialogService _UserDialogService;
        private readonly IManagerUser _UserManager;

        #endregion Services

        #region Fields

        /// <summary>
        /// User name input.
        /// </summary>
        public string? UserName
        {
            get => _UserName;
            set => Set(ref _UserName, value);
        }

        private string? _UserName;

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="UserManager">The user manager.</param>
        /// <param name="UserDialogService">The user dialog service.</param>
        public LoginViewModel()
        {
            _UserDialogService = App.Services.GetService(typeof(IUserDialogService)) as IUserDialogService;
            _UserManager = App.Services.GetService(typeof(IManagerUser)) as IManagerUser;
        }

        #region TryToLogInCommand

        private ICommand? _TryToLogInCommand;

        /// <summary>
        /// Try to log in command.
        /// </summary>
        public ICommand TryToLogInCommand => _TryToLogInCommand ??= new LambdaCommand(OnTryToLogInCommandExecuted, CanTryToLogInCommandExecute);

        /// <summary>
        /// Can command execute.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanTryToLogInCommandExecute(object arg) => true;

        /// <summary>
        /// Try to log in realization.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private async void OnTryToLogInCommandExecuted(object obj)
        {
            var param = (Tuple<object, object>)obj;

            // e.g. for two TextBox object
            var passwordBox = (PasswordBox)param.Item1;

            if (!await _UserManager.Login(UserName, passwordBox.Password))
            {
                _UserDialogService.ShowDialogErrorWindowBox(Loc.Tr("LoginInfo.WrongData", "Not localized"), Loc.Tr("LoginInfo.DialogTitle", "Not localized"));
            }
            else
            {
                _UserDialogService.ShowSnackbarInfo(Loc.Tr("LoginInfo.LoginSuccessful", "Not localized"));
                DialogHost.Close(MainControl.DialogName);
            }
        }

        #endregion TryToLogInCommand
    }
}