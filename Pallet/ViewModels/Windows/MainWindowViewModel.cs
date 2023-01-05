using Pallet.ExternalDatabase.Context;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.Windows
{
    internal class MainWindowViewModel : ViewModel
    {
        public bool ConnectionStatus
        {
            get
            {
                Set(ref _ConnectionStatus, _OPC.IsConnected);
                return _ConnectionStatus;
            }
        }

        private bool _ConnectionStatus;

        public MainControlViewModel CurrentModel
        {
            get => _CurrentModel;
            set => Set(ref _CurrentModel, value);
        }

        private MainControlViewModel _CurrentModel;

        public Task<bool> IsExternalDatabaseConnected => _externalDbContext.Database.CanConnectAsync();

        private readonly IManagerProfiles _ManagerProfiles;
        private readonly IManagerUser _ManagerUser;
        private readonly ExternalDbContext _externalDbContext;
        private readonly IOPC _OPC;

        public MainWindowViewModel(ExternalDbContext externalDbContext, IOPC OPCProxy, IManagerUser IManagerUser, IManagerProfiles ManagerProfiles)
        {
            _ManagerProfiles = ManagerProfiles;
            _ManagerUser = IManagerUser;
            _externalDbContext = externalDbContext;
            _OPC = OPCProxy;
            AsyncInitialization().ConfigureAwait(false);
        }

        protected override async Task AsyncInitialization() => CurrentModel = new();

        #region ExternalDatabaseConnectCommand

        private ICommand _ExternalDatabaseConnectCommand;

        /// <summary>
        /// OPC connect command.
        /// </summary>
        public ICommand ExternalDatabaseConnectCommand => _ExternalDatabaseConnectCommand ??= new LambdaCommand(OnExternalDatabaseConnectCommandExecuted, CanExternalDatabaseConnectCommandExecute);

        /// <summary>
        /// Can reset OPC connection.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanExternalDatabaseConnectCommandExecute(object arg) =>
            !IsExternalDatabaseConnected.Result
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting OPC connection.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnExternalDatabaseConnectCommandExecuted(object obj)
        {
            _externalDbContext.Database.OpenConnection();
            _ManagerProfiles.ReadNewTask().ConfigureAwait(false);
            OnPropertyChanged(default);
        }

        #endregion ExternalDatabaseConnectCommand

        #region OPCConnectCommand

        private ICommand _OPCConnectCommand;

        /// <summary>
        /// OPC connect command.
        /// </summary>
        public ICommand OPCConnectCommand => _OPCConnectCommand ??= new LambdaCommand(OnOPCConnectCommandExecuted, CanOPCConnectCommandExecute);

        /// <summary>
        /// Can reset OPC connection.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanOPCConnectCommandExecute(object arg) =>
        !_OPC.IsConnected
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting OPC connection.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnOPCConnectCommandExecuted(object obj)
        {
            Task.Run(() => _OPC.InitializeOPC().ConfigureAwait(false));
        }

        #endregion OPCConnectCommand
    }
}