using Pallet.Database.Entities.Change.Profiles;
using Pallet.Database.Entities.Change.Types;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.Users;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Entities.Models;
using Pallet.Infrastructure.Commands;
using Pallet.Models;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.ViewModels.Base;
using Pallet.ViewModels.SubView;
using System.Collections.Specialized;

namespace Pallet.ViewModels.Windows
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        #region Services

        private readonly IDbRepository<Alarm> _RepositoryAlarms;
        private readonly IDbRepository<Signal> _RepositorySignals;
        private readonly IManagerProfiles _ManagerProfiles;
        private readonly IAlarmLogService _AlarmLogService;

        private readonly IUserDialogService _UserDialogService;

        private readonly IManagerLanguage _LanguageManager;
        private readonly IManagerUser _ManagerUser;
        private readonly IOPC _OPCProxy;


        #endregion Services

        #region Fields

        #region OPC

        #region Nail Type Active

        private Nailer _NailTypeActive;

        public Nailer NailTypeActive
        {
            get => _NailTypeActive;
            set => Set(ref _NailTypeActive, value);
        }

        #endregion Nail Type Active

        #region Alarms

        public ObservableCollection<AlarmOpc> Alarms
        {
            get => _Alarms;
            set => Set(ref _Alarms, value);
        }

        private ObservableCollection<AlarmOpc> _Alarms;

        #endregion Alarms

        #region Signals

        public ObservableCollection<SignalOPC> Signals
        {
            get => _Signals;
            set => Set(ref _Signals, value);
        }

        private ObservableCollection<SignalOPC> _Signals;

        #endregion Signals

        #region IsAutoMode

        public SignalOPC IsAutoMode
        {
            get => _IsAutoMode;
            set => Set(ref _IsAutoMode, value);
        }

        private SignalOPC _IsAutoMode;

        #endregion IsAutoMode

        #region IsStopMode

        public SignalOPC IsStopMode
        {
            get => _IsStopMode;
            set => Set(ref _IsStopMode, value);
        }

        private SignalOPC _IsStopMode;

        #endregion IsStopMode

        #region IsRequestProfile

        public SignalOPC IsRequestProfile
        {
            get => _IsRequestProfile;
            set => Set(ref _IsRequestProfile, value);
        }

        private SignalOPC _IsRequestProfile;

        #endregion IsRequestProfile

        #region OPC Connection status

        public bool ConnectionStatus
        {
            get
            {
                Set(ref _ConnectionStatus, _OPCProxy.ConnectionStatus);
                return _ConnectionStatus;
            }
        }

        private bool _ConnectionStatus;

        #endregion OPC Connection status

        #endregion OPC

        #region SQL

        #region Profiles

        public ICollectionView? ProfilesView => _ProfileViewSource.View;

        private readonly CollectionViewSource _ProfileViewSource;

        //private ObservableCollection<ProfileInfoData> ProfilesInfoData
        //{
        //    get => _ProfilesInfoData;
        //    set
        //    {
        //        if (Set(ref _ProfilesInfoData, value))
        //        {
        //            _ProfileViewSource.Source = value;
        //            _ProfileViewSource.View.Refresh();
        //        }
        //        OnPropertyChanged(nameof(ProfilesView));
        //    }
        //}

        //private ObservableCollection<ProfileInfoData> _ProfilesInfoData;

        #endregion Profiles

        #region Filtering (view)

        public string FilterName
        {
            get => _FilterName;
            set
            {
                if (Set(ref _FilterName, value))
                    _ProfileViewSource.View.Refresh();
            }
        }

        private string _FilterName;

        #endregion Filtering (view)

        #region Active Profile (view)

        /// <summary>
        /// Gets or sets the active profile.
        /// </summary>
        public Profile ActiveProfile
        {
            get => _ActiveProfile;
            set => Set(ref _ActiveProfile, value);
        }

        private Profile _ActiveProfile;

        #endregion Active Profile (view)

        #region User

        public User User
        {
            get => _User;
            set => Set(ref _User, value);
        }

        private User _User;

        #endregion User

        #region Selected Language

        public Lang SelectedLang
        { get => _SelectedLang; set { Set(ref _SelectedLang, value); _LanguageManager.SelectedLang = value; } }

        private Lang _SelectedLang;

        #endregion Selected Language

        #region Languages

        public List<Lang> Langs
        {
            get => _Langs;
            set => Set(ref _Langs, value);
        }

        private List<Lang> _Langs;

        #endregion Languages

        #endregion SQL

        #region Current Model (View)

        public ViewModel CurrentModel
        {
            get => _CurrentModel;
            set
            {
                Set(ref _CurrentModel, value);
                IsCurrentViewModelIsAlarmViewModel = value is AlarmViewModel;
                IsCurrentViewModelIsPalletViewModel = value is PalletViewModel;
                IsCurrentViewModelIsManualViewModel = value is ManualViewModel;
            }
        }

        private PalletViewModel _PalletViewModel;
        private ManualViewModel _ManualViewModel;
        private AlarmViewModel _AlarmViewModel;

        private ViewModel _CurrentModel;

        public bool IsCurrentViewModelIsAlarmViewModel
        {
            get => _IsCurrentViewModelIsAlarmViewModel;
            set => Set(ref _IsCurrentViewModelIsAlarmViewModel, value);
        }

        private bool _IsCurrentViewModelIsAlarmViewModel;

        public bool IsCurrentViewModelIsPalletViewModel
        {
            get => _IsCurrentViewModelIsPalletViewModel;
            set => Set(ref _IsCurrentViewModelIsPalletViewModel, value);
        }

        private bool _IsCurrentViewModelIsPalletViewModel;

        public bool IsCurrentViewModelIsManualViewModel
        {
            get => _IsCurrentViewModelIsManualViewModel;
            set => Set(ref _IsCurrentViewModelIsManualViewModel, value);
        }

        private bool _IsCurrentViewModelIsManualViewModel;

        #endregion Current Model (View)

        #endregion Fields

        #region Constructor - Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="RepositorySignals">The repository signals.</param>
        /// <param name="RepositoryAlarms">The repository alarms.</param>
        /// <param name="ManagerProfiles">The manager profiles.</param>
        /// <param name="UserManager">The user manager.</param>
        /// <param name="languageManager">The language manager.</param>
        /// <param name="UserDialogService">The user dialog service.</param>
        /// <param name="AlarmLogService">The alarm log service.</param>
        /// <param name="OPCProxy">The OPC proxy.</param>
        public MainWindowViewModel(
            IDbRepository<Signal> RepositorySignals,
            IDbRepository<Alarm> RepositoryAlarms,
            IManagerProfiles ManagerProfiles,
            IManagerUser UserManager,
            IManagerLanguage languageManager,
            IUserDialogService UserDialogService,
            IAlarmLogService AlarmLogService,
            IOPC OPCProxy
            )
        {
            _AlarmLogService = AlarmLogService;
            _RepositorySignals = RepositorySignals;
            _RepositoryAlarms = RepositoryAlarms;
            _LanguageManager = languageManager;
            _ManagerUser = UserManager;
            _ManagerProfiles = ManagerProfiles;
            _OPCProxy = OPCProxy;
            _UserDialogService = UserDialogService;

            //_ResourceManager = new ResourceManager("Pallet.Resources.Windows.MainWindow.MainWindowResource", Assembly.GetExecutingAssembly());
            SelectedLang = _LanguageManager.SelectedLang;
            Langs = _LanguageManager.Langs;

            _ProfileViewSource = new()
            {
                Source = _ManagerProfiles.Items.ToList(),
                SortDescriptions =
                {
                    new SortDescription(nameof(Profile.DateLastUse), ListSortDirection.Descending)
                }
            };
            _ProfileViewSource.Filter += ProfileListSource_Filter;

            Signals = _OPCProxy.Signals;
            Alarms = _OPCProxy.Alarms;

            Alarms.CollectionChanged += Alarms_CollectionChanged;
            Signals.CollectionChanged += Signals_CollectionChanged;

            new Thread(() => InitializeOPC()).Start();

            ActiveProfile = _ManagerProfiles.GetActiveProfile();
            _ManagerProfiles.ActiveProfileChanged -= ManagerProfiles_ActiveProfileChanged;
            _ManagerProfiles.ActiveProfileChanged += ManagerProfiles_ActiveProfileChanged;
        }

        /// <summary>
        /// Handler event when Active profile changed.
        /// Get active profile and refresh list view
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ManagerProfiles_ActiveProfileChanged(object? sender, EventArgs e)
        {
            ActiveProfile = _ManagerProfiles.GetActiveProfile();
            ProfilesView.Refresh();
            OnPropertyChanged(nameof(ActiveProfile));
        }

        #endregion Constructor - Destructor

        #region Private Functions

        /// <summary>
        /// Initializes OPC Connection.
        /// </summary>
        /// <returns>A Task.</returns>
        private Task InitializeOPC()
        {
            _OPCProxy.Connect();
            _OPCProxy.AddSubcribeFolder(Services.OPC.OPCProxy.SubForlderAlarm);
            _OPCProxy.AddSubcribeFolder(Services.OPC.OPCProxy.SubForlderSystem);

            foreach (var alarm in _RepositoryAlarms.Items.ToArray())
            {
                AlarmOpc temp = new()
                {
                    Info = alarm,
                    NodeOpc = _OPCProxy.GetNode(alarm.Address),
                    Value = new bool()
                };
                _OPCProxy.SubscribeValue(temp, Services.OPC.OPCProxy.SubForlderAlarm);
            }
            foreach (var signal in _RepositorySignals.Items.ToArray())
            {
                SignalOPC temp = new()
                {
                    Info = signal,
                    NodeOpc = _OPCProxy.GetNode(signal.Address),
                    Value = new bool()
                };
                _OPCProxy.SubscribeValue(temp, Services.OPC.OPCProxy.SubForlderSystem);
            }

            IsAutoMode = Signals.First(s => s.Info.Name == "M_Auto");
            IsStopMode = Signals.First(s => s.Info.Name == "M_Halt");
            return Task.CompletedTask;
        }

        #endregion Private Functions

        #region Events

        #region Active profile property changed event

        /// <summary>
        /// Active profile property changed - must refresh profiles view to show active profile.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        //private void ActiveProfile_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{

        #endregion Active profile property changed event

        #region Profiles filter event

        /// <summary>
        /// _S the profile list source_ filter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ProfileListSource_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item is not Profile profile || string.IsNullOrEmpty(FilterName)) return;
            if (!profile.Name.Contains(FilterName))
                e.Accepted = false;
        }

        #endregion Profiles filter event

        #region Alarms Collection changed Event

        /// <summary>
        /// Alarms collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Alarms_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (AlarmOpc item in e.OldItems)
                {
                    item.PropertyChanged -= AlarmsValue_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (AlarmOpc item in e.NewItems)
                {
                    item.PropertyChanged += AlarmsValue_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// Alarms property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AlarmsValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(Alarms).Refresh();
            Alarms.Refresh();
            OnPropertyChanged(nameof(Alarms));
        }

        #endregion Alarms Collection changed Event

        #region Signals Collection changed Event

        /// <summary>
        /// Signals collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Signals_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (SignalOPC item in e.OldItems)
                {
                    item.PropertyChanged -= SignalsValue_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (SignalOPC item in e.NewItems)
                {
                    item.PropertyChanged += SignalsValue_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// Signals property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SignalsValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(Signals).Refresh();
            Signals.Refresh();
        }

        #endregion Signals Collection changed Event

        #endregion Events

        #region Commands

        #region OPCCommands

        #region SendProfileCommand

        private ICommand _SendProfileCommand;
        /// <summary>
        /// Send profile command.
        /// </summary>
        public ICommand SendProfileCommand => _SendProfileCommand ??= new LambdaCommand(OnSendProfileCommandExecuted, CanSendProfileCommandExecute);

        /// <summary>
        /// Can Send  profile .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanSendProfileCommandExecute(object arg) => _ManagerProfiles.ActiveProfile != null && (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        /// <summary>
        /// Sending  profile.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSendProfileCommandExecuted(object obj) => _OPCProxy.WriteProfile(_ManagerProfiles.ActiveProfile);

        #endregion SendProfileCommand

        #region AutoMode

        #region SetAutoModeCommand

        private ICommand _SetAutoModeCommand;
        /// <summary>
        /// Set auto mode command.
        /// </summary>
        public ICommand SetAutoModeCommand => _SetAutoModeCommand ??= new LambdaCommand(OnSetAutoModeCommandExecuted, CanSetAutoModeCommandExecute);

        /// <summary>
        /// Can set auto mode.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanSetAutoModeCommandExecute(object arg) => !(bool)IsAutoMode.Value && !(bool)IsStopMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Setting auto mode .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSetAutoModeCommandExecuted(object obj)
        { IsAutoMode.Value = true; _OPCProxy.WriteActualValue((bool)IsAutoMode.Value, IsAutoMode.NodeOpc); }

        #endregion SetAutoModeCommand

        #region ResetAutoModeCommand

        private ICommand _ResetAutoModeCommand;
        /// <summary>
        /// Reset auto mode command.
        /// </summary>
        public ICommand ResetAutoModeCommand => _ResetAutoModeCommand ??= new LambdaCommand(OnResetAutoModeCommandExecuted, CanResetAutoModeCommandExecute);

        /// <summary>
        /// Can reset auto mode.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanResetAutoModeCommandExecute(object arg) => (bool)IsAutoMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting auto mode.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnResetAutoModeCommandExecuted(object obj)
        { IsAutoMode.Value = false; _OPCProxy.WriteActualValue((bool)IsAutoMode.Value, IsAutoMode.NodeOpc); }

        #endregion ResetAutoModeCommand

        #endregion AutoMode

        #region StopMode

        #region SetStopModeCommand

        private ICommand _SetStopModeCommand;
        /// <summary>
        /// Set stop mode command.
        /// </summary>
        public ICommand SetStopModeCommand => _SetStopModeCommand ??= new LambdaCommand(OnSetStopModeCommandExecuted, CanSetStopModeCommandExecute);

        /// <summary>
        /// Can set stop mode .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanSetStopModeCommandExecute(object arg) => !(bool)IsStopMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Setting stop mode.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSetStopModeCommandExecuted(object obj)
        {
            IsStopMode.Value = true; _OPCProxy.WriteActualValue((bool)IsStopMode.Value, IsStopMode.NodeOpc);
            IsAutoMode.Value = false; _OPCProxy.WriteActualValue((bool)IsAutoMode.Value, IsAutoMode.NodeOpc);
        }

        #endregion SetStopModeCommand

        #region ResetStopModeCommand

        private ICommand _ResetStopModeCommand;
        /// <summary>
        /// Reset stop mode command.
        /// </summary>
        public ICommand ResetStopModeCommand => _ResetStopModeCommand ??= new LambdaCommand(OnResetStopModeCommandExecuted, CanResetStopModeCommandExecute);

        /// <summary>
        /// Cans reset stop mode .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanResetStopModeCommandExecute(object arg) => (bool)IsStopMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting stop mode .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnResetStopModeCommandExecuted(object obj)
        { IsStopMode.Value = false; _OPCProxy.WriteActualValue((bool)IsStopMode.Value, IsStopMode.NodeOpc); }

        #endregion ResetStopModeCommand

        #endregion StopMode

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
        private bool CanOPCConnectCommandExecute(object arg) => !ConnectionStatus && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting OPC connection.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnOPCConnectCommandExecuted(object obj) => _OPCProxy.Reconnect();

        #endregion OPCConnectCommand

        #endregion OPCCommands

        #region ViewModelCommands

        #region ShowManualViewCommand

        private ICommand _ShowManualViewCommand;
        /// <summary>
        /// Show manual view command.
        /// </summary>
        public ICommand ShowManualViewCommand => _ShowManualViewCommand ??= new LambdaCommand(OnShowManualViewCommandExecuted, CanShowManualViewCommandExecute);

        /// <summary>
        /// Can show manual view .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanShowManualViewCommandExecute(object arg) => !IsCurrentViewModelIsManualViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Manager;

        /// <summary>
        /// Show manual view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowManualViewCommandExecuted(object obj) => CurrentModel = _ManualViewModel ??= new ManualViewModel();

        #endregion ShowManualViewCommand

        #region ShowPalletViewCommand

        private ICommand _ShowPalletViewCommand;
        /// <summary>
        /// Show pallet view command.
        /// </summary>
        public ICommand ShowPalletViewCommand => _ShowPalletViewCommand ??= new LambdaCommand(OnShowPalletViewCommandExecuted, CanShowPalletViewCommandExecute);

        /// <summary>
        /// Can show pallet view.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanShowPalletViewCommandExecute(object arg) => !IsCurrentViewModelIsPalletViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Show pallet view .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowPalletViewCommandExecuted(object obj)
        {
            //CurrentModel?.Dispose();
            CurrentModel = _PalletViewModel ??= new PalletViewModel();
        }

        #endregion ShowPalletViewCommand

        #region ShowAlarmViewCommand

        private ICommand _ShowAlarmViewCommand;
        /// <summary>
        /// Show alarm view command.
        /// </summary>
        public ICommand ShowAlarmViewCommand => _ShowAlarmViewCommand ??= new LambdaCommand(OnShowAlarmViewCommandExecuted, CanShowAlarmViewCommandExecute);

        /// <summary>
        /// Can show alarm view .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanShowAlarmViewCommandExecute(object arg) => !IsCurrentViewModelIsAlarmViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Show alarm view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowAlarmViewCommandExecuted(object obj) => CurrentModel = _AlarmViewModel ??= new AlarmViewModel(_AlarmLogService);

        #endregion ShowAlarmViewCommand

        #endregion ViewModelCommands

        #region LogoutCommand

        private ICommand _LogoutCommand;
        public ICommand LogoutCommand => _LogoutCommand ??= new LambdaCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);

        /// <summary>
        /// Cans logout .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanLogoutCommandExecute(object arg) => _ManagerUser.IsLogined;

        /// <summary>
        /// Logout.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnLogoutCommandExecuted(object obj)
        {
            _ManagerUser.LogOut();
            User = null;
            _UserDialogService.ShowInformation("You have been logout", "Login");
        }

        #endregion LogoutCommand

        #region DefaultCommand

        private ICommand _DefaultCommand;

        /// <summary>
        /// Default command.
        /// </summary>
        public ICommand DefaultCommand => _DefaultCommand ??= new LambdaCommand(OnDefaultCommandExecuted, CanDefaultCommandExecute);

        /// <summary>
        /// Can execute default command .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanDefaultCommandExecute(object arg) => true;

        /// <summary>
        /// Default function.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnDefaultCommandExecuted(object obj)
        { }

        #endregion DefaultCommand

        #endregion Commands
    }
}