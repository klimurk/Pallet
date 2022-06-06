using Pallet.Database.Entities.Change.Types;
using Pallet.Entities.Models;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.SubView
{
    public class ManualViewModel : ViewModel
    {
        #region Fields

        #region Services

        private readonly IManagerNailTypes _ManagerNailTypes;
        private readonly IManagerUser _UserManager;
        private readonly ObservableCollection<SignalOPC> _Signals;

        #endregion Services

        #region Nail Types

        public ICollectionView NailTypesView => _NailTypeViewSource.View;

        private readonly CollectionViewSource _NailTypeViewSource;

        private IEnumerable<Nailer> NailTypes => _ManagerNailTypes.NailTypes;

        #endregion Nail Types

        #region Active nail type

        public Nailer NailTypeActive
        {
            get => _NailTypeActive;
            set => Set(ref _NailTypeActive, value);
        }

        private Nailer _NailTypeActive;

        #endregion Active nail type

        #endregion Fields

        #region Constructor

        public ManualViewModel()
        {
            _Signals = (App.Services.GetService(typeof(IOPC)) as IOPC)?.Signals;
            _ManagerNailTypes = App.Services.GetService(typeof(IManagerNailTypes)) as IManagerNailTypes;
            _UserManager = App.Services.GetService(typeof(IManagerUser)) as IManagerUser;
            NailTypeActive = _ManagerNailTypes.ActiveNailType;

            _NailTypeViewSource = new()
            {
                Source = NailTypes,
                SortDescriptions = {
                    new SortDescription(
                        nameof(Nailer.Dock),
                        ListSortDirection.Ascending),
                    new SortDescription(
                        nameof(Nailer.Width),
                        ListSortDirection.Ascending),
                    new SortDescription(
                        nameof(Nailer.Size),
                        ListSortDirection.Ascending)
                }
            };
        }

        #endregion Constructor

        #region Commands

        #region RobotMoveLeft

        #region SetRobotMoveLeftCommand

        private ICommand _SetRobotMoveLeftCommand;
        public ICommand SetRobotMoveLeftCommand => _SetRobotMoveLeftCommand ??= new LambdaCommand(OnSetRobotMoveLeftCommandExecuted, CanSetRobotMoveLeftCommandExecute);

        private bool CanSetRobotMoveLeftCommandExecute(object arg) => !(bool)_Signals.First(s => s.Info.Name == "M_Links").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetRobotMoveLeftCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_Links").Value = true;

        #endregion SetRobotMoveLeftCommand

        #region ResetRobotMoveLeftCommand

        private ICommand _ResetRobotMoveLeftCommand;
        public ICommand ResetRobotMoveLeftCommand => _ResetRobotMoveLeftCommand ??= new LambdaCommand(OnResetRobotMoveLeftCommandExecuted, CanResetRobotMoveLeftCommandExecute);

        private bool CanResetRobotMoveLeftCommandExecute(object arg) => (bool)_Signals.First(s => s.Info.Name == "M_Links").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetRobotMoveLeftCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_Links").Value = false;

        #endregion ResetRobotMoveLeftCommand

        #endregion RobotMoveLeft

        #region RobotMoveRight

        #region SetRobotMoveRightCommand

        private ICommand _SetRobotMoveRightCommand;
        public ICommand SetRobotMoveRightCommand => _SetRobotMoveRightCommand ??= new LambdaCommand(OnSetRobotMoveRightCommandExecuted, CanSetRobotMoveRightCommandExecute);

        private bool CanSetRobotMoveRightCommandExecute(object arg) => !(bool)_Signals.First(s => s.Info.Name == "M_rechts").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetRobotMoveRightCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_rechts").Value = true;

        #endregion SetRobotMoveRightCommand

        #region ResetRobotMoveRightCommand

        private ICommand _ResetRobotMoveRightCommand;
        public ICommand ResetRobotMoveRightCommand => _ResetRobotMoveRightCommand ??= new LambdaCommand(OnResetRobotMoveRightCommandExecuted, CanResetRobotMoveRightCommandExecute);

        private bool CanResetRobotMoveRightCommandExecute(object arg) => (bool)_Signals.First(s => s.Info.Name == "M_rechts").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetRobotMoveRightCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_rechts").Value = false;

        #endregion ResetRobotMoveRightCommand

        #endregion RobotMoveRight

        #region RobotMoveUp

        #region SetRobotMoveUpCommand

        private ICommand _SetRobotMoveUpCommand;
        public ICommand SetRobotMoveUpCommand => _SetRobotMoveUpCommand ??= new LambdaCommand(OnSetRobotMoveUpCommandExecuted, CanSetRobotMoveUpCommandExecute);

        private bool CanSetRobotMoveUpCommandExecute(object arg) => !(bool)_Signals.First(s => s.Info.Name == "M_up").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetRobotMoveUpCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_up").Value = true;

        #endregion SetRobotMoveUpCommand

        #region ResetRobotMoveUpCommand

        private ICommand _ResetRobotMoveUpCommand;
        public ICommand ResetRobotMoveUpCommand => _ResetRobotMoveUpCommand ??= new LambdaCommand(OnResetRobotMoveUpCommandExecuted, CanResetRobotMoveUpCommandExecute);

        private bool CanResetRobotMoveUpCommandExecute(object arg) => (bool)_Signals.First(s => s.Info.Name == "M_up").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetRobotMoveUpCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_up").Value = false;

        #endregion ResetRobotMoveUpCommand

        #endregion RobotMoveUp

        #region RobotMoveDown

        #region SetRobotMoveDownCommand

        private ICommand _SetRobotMoveDownCommand;
        public ICommand SetRobotMoveDownCommand => _SetRobotMoveDownCommand ??= new LambdaCommand(OnSetRobotMoveDownCommandExecuted, CanSetRobotMoveDownCommandExecute);

        private bool CanSetRobotMoveDownCommandExecute(object arg) => !(bool)_Signals.First(s => s.Info.Name == "M_down").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetRobotMoveDownCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_down").Value = true;

        #endregion SetRobotMoveDownCommand

        #region ResetRobotMoveDownCommand

        private ICommand _ResetRobotMoveDownCommand;
        public ICommand ResetRobotMoveDownCommand => _ResetRobotMoveDownCommand ??= new LambdaCommand(OnResetRobotMoveDownCommandExecuted, CanResetRobotMoveDownCommandExecute);

        private bool CanResetRobotMoveDownCommandExecute(object arg) => (bool)_Signals.First(s => s.Info.Name == "M_down").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetRobotMoveDownCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_down").Value = false;

        #endregion ResetRobotMoveDownCommand

        #endregion RobotMoveDown

        #region LampTest

        #region SetLampTestCommand

        private ICommand _SetLampTestCommand;
        public ICommand SetLampTestCommand => _SetLampTestCommand ??= new LambdaCommand(OnSetLampTestCommandExecuted, CanSetLampTestCommandExecute);

        private bool CanSetLampTestCommandExecute(object arg) => !(bool)_Signals.First(s => s.Info.Name == "M_LampTeste").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetLampTestCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_LampTeste").Value = true;

        #endregion SetLampTestCommand

        #region ResetLampTestCommand

        private ICommand _ResetLampTestCommand;
        public ICommand ResetLampTestCommand => _ResetLampTestCommand ??= new LambdaCommand(OnResetLampTestCommandExecuted, CanResetLampTestCommandExecute);

        private bool CanResetLampTestCommandExecute(object arg) => (bool)_Signals.First(s => s.Info.Name == "M_LampTeste").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetLampTestCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_LampTeste").Value = false;

        #endregion ResetLampTestCommand

        #endregion LampTest

        #region SetRobotNailShootCommand

        private ICommand _SetRobotNailShootCommand;
        public ICommand SetRobotNailShootCommand => _SetRobotNailShootCommand ??= new LambdaCommand(OnSetRobotNailShootCommandExecuted, CanSetRobotNailShootCommandExecute);

        private bool CanSetRobotNailShootCommandExecute(object arg) => !(bool)_Signals.First(s => s.Info.Name == "M_shooting").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetRobotNailShootCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_nail").Value = true;

        #endregion SetRobotNailShootCommand

        #region SetMoveToConveyorCommand

        private ICommand _SetMoveToConveyorCommand;
        public ICommand SetMoveToConveyorCommand => _SetMoveToConveyorCommand ??= new LambdaCommand(OnSetMoveToConveyorCommandExecuted, CanSetMoveToConveyorCommandExecute);

        private bool CanSetMoveToConveyorCommandExecute(object arg) => false && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetMoveToConveyorCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_MoveToConveyor").Value = true;

        #endregion SetMoveToConveyorCommand

        #region SetRobotToHomePositionCommand

        private ICommand _SetRobotToHomePositionCommand;
        public ICommand SetRobotToHomePositionCommand => _SetRobotToHomePositionCommand ??= new LambdaCommand(OnSetRobotToHomePositionCommandExecuted, CanSetRobotToHomePositionCommandExecute);

        private bool CanSetRobotToHomePositionCommandExecute(object arg) => false && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetRobotToHomePositionCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_Home").Value = true;

        #endregion SetRobotToHomePositionCommand

        #region RotateTableCommand

        private ICommand _RotateTableCommand;
        public ICommand RotateTableCommand => _RotateTableCommand ??= new LambdaCommand(OnRotateTableCommandExecuted, CanRotateTableCommandExecute);

        private bool CanRotateTableCommandExecute(object arg) => false && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnRotateTableCommandExecuted(object obj) => _Signals.First(s => s.Info.Name == "M_TableRotate").Value = true;

        #endregion RotateTableCommand

        #endregion Commands
    }
}