using Pallet.Database.Entities.Change.Profiles;
using Pallet.Infrastructure.Commands.Base;
using Pallet.Services.Managers;
using Pallet.Services.Managers.Interfaces;
using Pallet.View;

namespace Pallet.Infrastructure.Commands;

internal class OpenProfileWindowCommand : Command
{
    private ProfilePreviewWindow _Window;
    private readonly IManagerUser _ManagerUser;
    private readonly ManagerProfiles _ManagerProfiles;

    protected override bool CanExecute(object parameter) => (_Window == null) && (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

    protected override void Execute(object parameter)
    {
        _ManagerProfiles.SetSelectedProfile((Profile)parameter);

        var window = new ProfilePreviewWindow
        {
            Owner = Application.Current.MainWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        _Window = window;
        window.Closed += OnWindowClosed;
        window.ShowDialog();
    }

    private void OnWindowClosed(object sender, EventArgs e)
    {
        ((Window)sender).Closed -= OnWindowClosed;
        _Window = null;
    }

    public OpenProfileWindowCommand()
    {
        _ManagerUser = (IManagerUser)App.Host.Services.GetService(typeof(IManagerUser));
        _ManagerProfiles = (ManagerProfiles)App.Host.Services.GetService(typeof(IManagerProfiles));
    }
}