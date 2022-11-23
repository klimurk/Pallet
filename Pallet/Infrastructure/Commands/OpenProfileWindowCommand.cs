//using Pallet.Infrastructure.Commands.Base;
//using Pallet.Services.Managers.Interfaces;
//using Pallet.View.Windows;

//namespace Pallet.Infrastructure.Commands;

//internal class OpenNewUserWindowCommand : Command
//{
//    private readonly IManagerUser _ManagerUser;
//    private CreateUserWindow _Window;

//    protected override bool CanExecute(object parameter) => (_Window == null) && (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin);

//    protected override void Execute(object parameter)
//    {
//        var window = new CreateUserWindow();
        
//        _Window = window;
//        window.Closed += OnWindowClosed;
//        window.ShowDialog();
//    }

//    private void OnWindowClosed(object sender, EventArgs e)
//    {
//        ((Window)sender).Closed -= OnWindowClosed;
//        _Window = null;
//    }

//    public OpenNewUserWindowCommand()
//    {
//        _ManagerUser = (IManagerUser)App.Host.Services.GetService(typeof(IManagerUser));
//    }
//}