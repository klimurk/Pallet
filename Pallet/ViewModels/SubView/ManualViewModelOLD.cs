using Pallet.InternalDatabase.Entities.OPC;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.ViewModels.Base;

namespace Pallet.ViewModels.SubView
{
    public class ManualViewModelOLD : ViewModel
    {
        #region Fields

        #region Services
        
        private readonly IManagerUser _UserManager;
        private readonly ObservableCollection<Signal> _Signals;

        #endregion Services

        #endregion Fields

        #region Constructor

        public ManualViewModelOLD(IManagerUser ManagerUser, IOPC OPC)
        {
            _Signals = OPC.Signals;
            //_ManagerNailTypes = ManagerNailTypes;
            _UserManager = ManagerUser;
        }

        #endregion Constructor
    }
}