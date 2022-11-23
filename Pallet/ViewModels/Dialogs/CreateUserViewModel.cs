using CodingSeb.Localization;
using MaterialDesignThemes.Wpf;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Language;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.Windows
{
    public class CreateUserViewModel : ViewModel
    {
        private readonly IManagerUser _IManagerUser;
        private readonly IManagerLanguage? _ManagerLanguage;
        private readonly IUserDialogService _UserDialogService;
        private string _Name;

        public string OldName;

        public string Name
        {
            get => _Name ??= OldName;
            set => Set(ref _Name, value);
        }

        private string _Password;

        public string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }

        private string _Description;

        public string Description
        {
            get => _Description;
            set => Set(ref _Description, value);
        }

        private string _SelectedRole;

        public string SelectedRole
        {
            get => _SelectedRole;
            set => Set(ref _SelectedRole, value);
        }

        public List<string> Roles => Enum.GetNames(typeof(IManagerUser.UserRoleNum)).ToList();

        public CreateUserViewModel()
        {
            _IManagerUser = App.Services.GetService(typeof(IManagerUser)) as IManagerUser;
            _ManagerLanguage = App.Services.GetService(typeof(IManagerLanguage)) as IManagerLanguage;
            _UserDialogService = App.Services.GetService(typeof(IUserDialogService)) as IUserDialogService;
        }

        #region CreateNewUserCommand

        private ICommand _CreateNewUserCommand;

        /// <summary>
        /// CreateNewUser command.
        /// </summary>
        public ICommand CreateNewUserCommand => _CreateNewUserCommand ??= new LambdaCommand(OnCreateNewUserCommandExecuted, CanCreateNewUserCommandExecute);

        /// <summary>
        /// Can execute default command .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanCreateNewUserCommandExecute(object arg) => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(SelectedRole);

        /// <summary>
        /// CreateNewUser function.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private async void OnCreateNewUserCommandExecuted(object obj)
        {
            if (_IManagerUser.Users.Any(s => s.Name == OldName))
            {
                if (_IManagerUser.ModifyUser(OldName, Name, Password, SelectedRole, Description))
                {
                    _UserDialogService.ShowInformation(Loc.Tr("UserInfo.Info.Modified", "Not localized"));
                    DialogHost.Close(MainWindow.DialogName);
                }
                else
                {
                    _UserDialogService.ShowError(Loc.Tr("UserInfo.Errors.NotModified", "Not localized"));
                }
            }
            else
            {
                if (_IManagerUser.RegisterNewUser(Name, Password, SelectedRole, Description))
                {
                    _UserDialogService.ShowInformation(Loc.Tr("UserInfo.Info.Created", "Not localized"));
                    DialogHost.Close(MainWindow.DialogName);
                }
                else
                {
                    _UserDialogService.ShowError(Loc.Tr("UserInfo.Errors.NotCreated", "Not localized"));
                }
            }
        }

        #endregion CreateNewUserCommand
    }
}