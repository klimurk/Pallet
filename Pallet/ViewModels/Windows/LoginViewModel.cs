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

        /// <summary>
        /// Username input.
        /// </summary>
        public string UserName { get => _UserName; set => Set(ref _UserName, value); }

        private string _UserName;

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="UserManager">The user manager.</param>
        /// <param name="UserDialogService">The user dialog service.</param>
        public LoginViewModel(IManagerUser UserManager, IUserDialogService UserDialogService)
        {
            _MainWindowViewModel = App.Services.GetService(typeof(MainWindowViewModel)) as MainWindowViewModel;
            _UserDialogService = UserDialogService;
            _UserManager = UserManager;
        }

        #region TryToLogInCommand

        private ICommand _TryToLogInCommand;

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