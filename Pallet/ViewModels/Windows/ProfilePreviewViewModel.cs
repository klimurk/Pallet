using Pallet.Database.Entities.Change.Profiles;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.Windows
{
    public class ProfilePreviewViewModel : ViewModel
    {
        private readonly IManagerProfiles _ManagerProfiles;
        private readonly IUserDialogService _UserDialogService;

        #region Fields

        #region SelectedProfile

        private Profile _SelectedProfile;

        public Profile SelectedProfile
        {
            get => _SelectedProfile;
            set => Set(ref _SelectedProfile, value);
        }

        #endregion SelectedProfile

        public bool IsSelectedProfileActive { get => _IsSelectedProfileActive; set => Set(ref _IsSelectedProfileActive, value); }

        private bool _IsSelectedProfileActive;

        #endregion Fields

        public ProfilePreviewViewModel(
            IManagerProfiles ManagerProfiles,
            IUserDialogService UserDialogService
            )
        {
            _ManagerProfiles = ManagerProfiles;
            _UserDialogService = UserDialogService;
            SelectedProfile = _ManagerProfiles.GetSelectedProfile();
            IsSelectedProfileActive = SelectedProfile == _ManagerProfiles.ActiveProfile;
            
        }

        public ICommand ActivateProfileCommand => _ActivateProfileCommand ??= new LambdaCommand(OnActivateProfileCommandExecuted, CanActivateProfileCommandExecute);

        private ICommand _ActivateProfileCommand;

        private static bool CanActivateProfileCommandExecute(object p) => true;

        private void OnActivateProfileCommandExecuted(object p)
        {
            if (!_UserDialogService.ConfirmWarning("Activate profile?", "Activate?")) return;

            _ManagerProfiles.ActivateSelectedProfile();
            ((Window)p).Close();
        }
    }
}