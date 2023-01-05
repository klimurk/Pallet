using Pallet.InternalDatabase.Entities.OPC;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.SubView
{
    public class ManualViewModel : ViewModel
    {
        #region Fields

        #region Services

        private readonly IManagerUser _UserManager;
        private readonly ObservableCollection<Signal> _Signals;

        #endregion Services


        #endregion Fields

        #region Constructor

        public ManualViewModel()
        {
            _Signals =(App.Services.GetService(typeof(IOPC)) as IOPC).Signals;
            _UserManager = App.Services.GetService(typeof(IManagerUser)) as IManagerUser;
           
        }

        #endregion Constructor

        #region Commands

        #region SetPrg101Command

        private ICommand _SetPrg101Command;
        public ICommand SetPrg101Command => _SetPrg101Command ??= new LambdaCommand(OnSetPrg101CommandExecuted, CanSetPrg101CommandExecute);

        private bool CanSetPrg101CommandExecute(object arg) => !(bool)_Signals.First(s => s.Name == "R01_Prg101").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetPrg101CommandExecuted(object obj) => _Signals.First(s => s.Name == "R01_Prg101").Value = true;

        #endregion SetPrg101Command

        #region SetWithoutNailCommand

        private ICommand _SetWithoutNailCommand;
        public ICommand SetWithoutNailCommand => _SetWithoutNailCommand ??= new LambdaCommand(OnSetWithoutNailCommandExecuted, CanSetWithoutNailCommandExecute);

        private bool CanSetWithoutNailCommandExecute(object arg) => (bool)_Signals.First(s => s.Name == "R01_Ohne_Shooting").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetWithoutNailCommandExecuted(object obj) => _Signals.First(s => s.Name == "R01_Ohne_Shooting").Value = false;

        #endregion SetWithoutNailCommand

        #region SetWithoutProductCommand

        private ICommand _SetWithoutProductCommand;
        public ICommand SetWithoutProductCommand => _SetWithoutProductCommand ??= new LambdaCommand(OnSetWithoutProductCommandExecuted, CanSetWithoutProductCommandExecute);

        private bool CanSetWithoutProductCommandExecute(object arg) => !(bool)_Signals.First(s => s.Name == "R01_Ohne_Production").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetWithoutProductCommandExecuted(object obj) => _Signals.First(s => s.Name == "R01_Ohne_Production").Value = true;

        #endregion SetWithoutProductCommand

        #region EndProcessCommand

        private ICommand _EndProcessCommand;
        public ICommand EndProcessCommand => _EndProcessCommand ??= new LambdaCommand(OnEndProcessCommandExecuted, CanEndProcessCommandExecute);

        private bool CanEndProcessCommandExecute(object arg) => (bool)_Signals.First(s => s.Name == "R01_Abort").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnEndProcessCommandExecuted(object obj) => _Signals.First(s => s.Name == "R01_Abort").Value = false;

        #endregion EndProcessCommand

        #region StopProductCommand

        private ICommand _StopProductCommand;
        public ICommand StopProductCommand => _StopProductCommand ??= new LambdaCommand(OnStopProductCommandExecuted, CanStopProductCommandExecute);

        private bool CanStopProductCommandExecute(object arg) => !(bool)_Signals.First(s => s.Name == "R01_PauseProduction").Value && _UserManager.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnStopProductCommandExecuted(object obj) => _Signals.First(s => s.Name == "R01_PauseProduction").Value = true;

        #endregion StopProductCommand

        #endregion Commands
    }
}