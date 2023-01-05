using CodingSeb.Localization;
using MaterialDesignThemes.Wpf;
using Pallet.Infrastructure.Commands;
using Pallet.InternalDatabase.Entities.Users;
using Pallet.Services.Language;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.View.Dialogs;
using Pallet.View.SubViews;
using Pallet.View.Windows;
using Pallet.ViewModels.Base;
using Pallet.ViewModels.Windows;
using System.Collections.Specialized;

namespace Pallet.ViewModels.SubView;

public class UsersViewModel : ViewModel
{
    #region Services

    private readonly IManagerUser _ManagerUser;

    private readonly IUserDialogService _UserDialogService;

    #endregion Services

    #region Fields

    private readonly CollectionViewSource UsersViewSourse;

    public ICollectionView? UsersView => UsersViewSourse.View;

    #endregion Fields

    #region Ctor

    public UsersViewModel()
    {
        _ManagerUser = App.Services.GetService(typeof(IManagerUser)) as IManagerUser;
        _UserDialogService = App.Services.GetService(typeof(IUserDialogService)) as IUserDialogService;
        UsersViewSourse = new()
        {
            Source = _ManagerUser.Users,
            SortDescriptions = { new SortDescription(nameof(User.Name), ListSortDirection.Descending) }
        };

        _ManagerUser.Users.CollectionChanged += Users_CollectionChanged;
    }

    #endregion Ctor

    #region Events

    private void Users_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (User user in e.OldItems)
            {
                user.UserChanged -= User_UserChanged;
            }
        }

        if (e.NewItems is not null)
        {
            foreach (User user in e.NewItems)
            {
                user.UserChanged += User_UserChanged; ;
            }
        }
        UsersView?.Refresh();
    }

    private void User_UserChanged(object? sender, EventArgs e)
    {
        UsersView?.Refresh();
    }

    #endregion Events

    #region OpenCreateUserWindowCommand

    private ICommand _OpenCreateUserWindowCommand;

    /// <summary>
    /// OpenCreateUserWindow command.
    /// </summary>
    public ICommand OpenCreateUserWindowCommand => _OpenCreateUserWindowCommand ??= new LambdaCommand(OnOpenCreateUserWindowCommandExecuted, CanOpenCreateUserWindowCommandExecute);

    /// <summary>
    /// Can execute default command .
    /// </summary>
    /// <param name="arg">The arg.</param>
    /// <returns>A bool.</returns>
    private bool CanOpenCreateUserWindowCommandExecute(object arg) => true;

    /// <summary>
    /// OpenCreateUserWindow function.
    /// </summary>
    /// <param name="obj">The obj.</param>
    private async void OnOpenCreateUserWindowCommandExecuted(object obj)
    {
        var view = new CreateUserWindow
        {
            //Owner = Application.Current.MainWindow,
            //WindowStartupLocation = WindowStartupLocation.CenterOwner,
            DataContext = new CreateUserViewModel()
        };

        //show the dialog
        var result = await DialogHost.Show(view, MainControl.DialogName);
    }

    #endregion OpenCreateUserWindowCommand

    #region ModifySelectedUserCommand

    private ICommand _ModifySelectedUserCommand;

    /// <summary>
    /// ModifySelectedUser command.
    /// </summary>
    public ICommand ModifySelectedUserCommand => _ModifySelectedUserCommand ??= new LambdaCommand(OnModifySelectedUserCommandExecuted, CanModifySelectedUserCommandExecute);

    /// <summary>
    /// Can execute default command .
    /// </summary>
    /// <param name="arg">The arg.</param>
    /// <returns>A bool.</returns>
    private bool CanModifySelectedUserCommandExecute(object arg) => true;

    /// <summary>
    /// ModifySelectedUser function.
    /// </summary>
    /// <param name="obj">The obj.</param>
    private async void OnModifySelectedUserCommandExecuted(object obj)
    {
        if (obj is not User) throw new ArgumentException();
        User selUser = (User)obj;
        var view = new CreateUserWindow
        {
            //Owner = Application.Current.MainWindow,
            //WindowStartupLocation = WindowStartupLocation.CenterOwner,
            DataContext = new CreateUserViewModel()
            {
                OldName = selUser.Name,
                Description = selUser.Description,
                SelectedRole = Enum.GetName(typeof(IManagerUser.UserRoleNum), selUser.RoleNum)
            }
        };

        ////show the dialog
        var result = await DialogHost.Show(view, MainControl.DialogName);
    }

    #endregion ModifySelectedUserCommand

    #region DeleteSelectedUserCommand

    private ICommand _DeleteSelectedUserCommand;

    /// <summary>
    /// DeleteSelectedUser command.
    /// </summary>
    public ICommand DeleteSelectedUserCommand => _DeleteSelectedUserCommand ??= new LambdaCommand(OnDeleteSelectedUserCommandExecuted, CanDeleteSelectedUserCommandExecute);

    /// <summary>
    /// Can execute default command .
    /// </summary>
    /// <param name="arg">The arg.</param>
    /// <returns>A bool.</returns>
    private bool CanDeleteSelectedUserCommandExecute(object arg) => true;

    /// <summary>
    /// DeleteSelectedUser function.
    /// </summary>
    /// <param name="obj">The obj.</param>
    private async void OnDeleteSelectedUserCommandExecuted(object obj)
    {
        if (obj is not User) throw new ArgumentException();
        User selUser = (User)obj;
        if (await _UserDialogService.ConfirmWarning($"{Loc.Tr("UserInfo.DeleteUser.Text", "Not localized")} {selUser.Name} ?", Loc.Tr("UserInfo.DeleteUser.Title", "Not localized"))) ;
        _ManagerUser.DeleteUser(selUser);
    }

    #endregion DeleteSelectedUserCommand
}