using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels.Base;
using System.Windows.Controls;

namespace Pallet.ViewModels.Windows
{
    internal class LoginViewModel : ViewModel
    {
        #region Services

        private readonly IUserDialogService _UserDialogService;
        private readonly IManagerUser _UserManager;
        private readonly MainWindowViewModel _MainWindowViewModel;

        #endregion Services

        #region Fields

        public string UserName { get => _UserName; set => Set(ref _UserName, value); }
        private string _UserName;

        #endregion Fields

        public LoginViewModel(
            IManagerUser UserManager, IUserDialogService UserDialogService
            )
        {
            _MainWindowViewModel = (MainWindowViewModel)App.Host.Services.GetService(typeof(MainWindowViewModel));
            _UserDialogService = UserDialogService;
            _UserManager = UserManager;
        }

        #region TryToLogInCommand

        private ICommand _TryToLogInCommand;

        public ICommand TryToLogInCommand => _TryToLogInCommand ??= new LambdaCommand(OnTryToLogInCommandExecuted, CanTryToLogInCommandExecute);

        private bool CanTryToLogInCommandExecute(object arg) => true;

        private void OnTryToLogInCommandExecuted(object obj)
        {
            var param = (Tuple<object, object>)obj;

            // e.g. for two TextBox object
            var passwordBox = (PasswordBox)param.Item1;
            var window = (Window)param.Item2;
            if (!_UserManager.Login(UserName, passwordBox.Password))
            {
                _UserDialogService
                    .ShowError("Login or password are incorrect", "Login");
            }
            else
            {
                _UserDialogService.ShowInformation("Login successful", "Login");
                _MainWindowViewModel.User = _UserManager.LoginedUser;
                window.Close();
            }
        }

        #endregion TryToLogInCommand
    }
}