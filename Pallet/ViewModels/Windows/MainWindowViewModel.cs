using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Database.Entities.Users;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Infrastructure.Commands;
using Pallet.Models;
using Pallet.Services.Draw.Interface;
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
        private readonly IDbRepository<Element> _RepositoryElements;
        private readonly IDbRepository<Signal> _RepositorySignals;
        private readonly IManagerProfiles _ManagerProfiles;
        private readonly IAlarmLogService _AlarmLogService;

        private readonly IUserDialogService _UserDialogService;

        private readonly IManagerLanguage _ManagerLanguage;
        private readonly IManagerUser _ManagerUser;
        private readonly IOPC _OPCProxy;

        private readonly IManagerNailTypes _ManagerNailTypes;
        private readonly ILogService _LogService;
        private readonly IManagerUser _UserManager;

        private IDrawer _Drawer;
        private readonly IDbRepository<SystemEvent> _RepositorySystemEvents;

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

        public ObservableCollection<Alarm> Alarms
        {
            get => _Alarms;
            set => Set(ref _Alarms, value);
        }

        private ObservableCollection<Alarm> _Alarms;

        #endregion Alarms

        #region Signals

        public ObservableCollection<Signal> Signals
        {
            get => _Signals;
            set => Set(ref _Signals, value);
        }

        private ObservableCollection<Signal> _Signals;

        #endregion Signals

        #region IsAutoMode

        public bool? IsAutoMode
        {
            get
            {
                _IsAutoMode = _OPCProxy.IsAutoMode;
                return _IsAutoMode;
            }

            set
            {
                _OPCProxy.IsAutoMode = value;
                Set(ref _IsAutoMode, value);
            }
        }

        private bool? _IsAutoMode;

        #endregion IsAutoMode

        #region IsStopMode

        public bool? IsStopMode
        {
            get
            {
                _IsStopMode = _OPCProxy.IsStopMode;
                return _IsStopMode;
            }

            set
            {
                _OPCProxy.IsStopMode = value;
                Set(ref _IsStopMode, value);
            }
        }

        private bool? _IsStopMode;

        #endregion IsStopMode

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

        public bool IsUserAuthorized => _ManagerUser.IsLogined;

        #endregion User

        #region Selected Language

        public Lang SelectedLang
        { get => _SelectedLang; set { Set(ref _SelectedLang, value); _ManagerLanguage.SelectedLang = value; } }

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
                IsCurrentViewModelIsLogViewModel = value is LogViewModel;
            }
        }

        private PalletViewModel _PalletViewModel;
        private ManualViewModel _ManualViewModel;
        private AlarmViewModel _AlarmViewModel;
        private LogViewModel _LogViewModel;

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

        public bool IsCurrentViewModelIsLogViewModel
        {
            get => _IsCurrentViewModelIsLogViewModel;
            set => Set(ref _IsCurrentViewModelIsLogViewModel, value);
        }

        private bool _IsCurrentViewModelIsLogViewModel;

        #endregion Current Model (View)

        #region Nail Types

        public ICollectionView NailTypesView => _NailTypeViewSource.View;

        private readonly CollectionViewSource _NailTypeViewSource;

        private IEnumerable<Nailer> NailTypes => _ManagerNailTypes.NailTypes;

        #endregion Nail Types

        #endregion Fields

        #region Constructor - Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="RepositorySignals">The repository signals.</param>
        /// <param name="RepositoryAlarms">The repository alarms.</param>
        /// <param name="ManagerProfiles">The manager profiles.</param>
        /// <param name="ManagerUser">The user manager.</param>
        /// <param name="ManagerLanguage">The language manager.</param>
        /// <param name="UserDialogService">The user dialog service.</param>
        /// <param name="AlarmLogService">The alarm log service.</param>
        /// <param name="OPCProxy">The OPC proxy.</param>
        public MainWindowViewModel(
                IDbRepository<Signal> RepositorySignals,
                IDbRepository<Alarm> RepositoryAlarms,
                IDbRepository<SystemEvent> RepositorySystemEvents,
                IDbRepository<Element> RepositoryElements,
                IManagerProfiles ManagerProfiles,
                IManagerUser ManagerUser,
                IManagerLanguage ManagerLanguage,
                IUserDialogService UserDialogService,
                IAlarmLogService AlarmLogService,
                IOPC OPCProxy,
                IManagerNailTypes ManagerNailTypes,
                ILogService LogsService,
                IDrawer Drawer
                )
        {
            "MainWindow view model init".CheckStage();

            _RepositoryElements = RepositoryElements;
            _RepositorySignals = RepositorySignals;
            _RepositoryAlarms = RepositoryAlarms;
            _AlarmLogService = AlarmLogService;
            _ManagerLanguage = ManagerLanguage;
            _ManagerUser = ManagerUser;
            _ManagerProfiles = ManagerProfiles;
            _OPCProxy = OPCProxy;
            _UserDialogService = UserDialogService;
            _ManagerNailTypes = ManagerNailTypes;
            _LogService = LogsService;
            _Drawer = Drawer;
            _RepositorySystemEvents = RepositorySystemEvents;
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
            //_ResourceManager = new ResourceManager("Pallet.Resources.Windows.MainWindow.MainWindowResource", Assembly.GetExecutingAssembly());
            SelectedLang = _ManagerLanguage.SelectedLang;
            Langs = _ManagerLanguage.Langs;

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

            Alarms.CollectionChanged -= Alarms_CollectionChanged;
            Signals.CollectionChanged -= Signals_CollectionChanged;

            Alarms.CollectionChanged += Alarms_CollectionChanged;
            Signals.CollectionChanged += Signals_CollectionChanged;

            ActiveProfile = _ManagerProfiles.GetActiveProfile();
            _ManagerProfiles.ActiveProfileChanged -= ManagerProfiles_ActiveProfileChanged;
            _ManagerProfiles.ActiveProfileChanged += ManagerProfiles_ActiveProfileChanged;

            // _LogService.MakeLog(_RepositorySystemEvents.Items.First(s => s.Name == "StartApp"));
        }

        #endregion Constructor - Destructor

        #region Events

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

        #region Profiles filter event

        /// <summary>
        /// _S the profile list source_ filter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ProfileListSource_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item is not Profile profile || string.IsNullOrEmpty(FilterName)) return;
            if (!profile.Name.ToLower().Contains(FilterName.ToLower()))
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
            if (e.OldItems is not null)
            {
                foreach (Alarm item in e.OldItems)
                {
                    item.PropertyChanged -= AlarmsValue_PropertyChanged;
                }
            }

            if (e.NewItems is not null)
            {
                foreach (Alarm item in e.NewItems)
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
        private void AlarmsValue_PropertyChanged(object sender, PropertyChangedEventArgs e) => Alarms.Refresh();

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
                foreach (Signal item in e.OldItems)
                {
                    item.PropertyChanged -= SignalsValue_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (Signal item in e.NewItems)
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
        private void SignalsValue_PropertyChanged(object sender, PropertyChangedEventArgs e) => Signals.Refresh();

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
        private bool CanSendProfileCommandExecute(object arg) => _OPCProxy.IsDataRequest == true && _ManagerProfiles.ActiveProfile != null && (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

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
        private bool CanSetAutoModeCommandExecute(object arg) => _OPCProxy.IsAutoMode == false && IsStopMode == false && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Setting auto mode .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSetAutoModeCommandExecuted(object obj) => IsAutoMode = true;

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
        private bool CanResetAutoModeCommandExecute(object arg) => IsAutoMode == true && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting auto mode.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnResetAutoModeCommandExecuted(object obj) => IsAutoMode = false;

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
        private bool CanSetStopModeCommandExecute(object arg) => IsStopMode == false && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Setting stop mode.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSetStopModeCommandExecuted(object obj)
        {
            IsStopMode = true;
            IsAutoMode = false;
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
        private bool CanResetStopModeCommandExecute(object arg) => (bool)IsStopMode && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting stop mode .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnResetStopModeCommandExecuted(object obj) => IsStopMode = false;

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
        private void OnOPCConnectCommandExecuted(object obj) => new Thread(() => _OPCProxy.InitializeOPC()).Start();

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
        private void OnShowManualViewCommandExecuted(object obj) => CurrentModel = _ManualViewModel ??= new ManualViewModel(_ManagerNailTypes, _ManagerUser, _OPCProxy);

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
            CurrentModel = _PalletViewModel ??= new PalletViewModel(_ManagerProfiles, _Drawer, _ManagerLanguage, _RepositoryElements);
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

        #region ShowLogViewCommand

        private ICommand _ShowLogViewCommand;

        /// <summary>
        /// Show alarm view command.
        /// </summary>
        public ICommand ShowLogViewCommand => _ShowLogViewCommand ??= new LambdaCommand(OnShowLogViewCommandExecuted, CanShowLogViewCommandExecute);

        /// <summary>
        /// Can show alarm view .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanShowLogViewCommandExecute(object arg) => !IsCurrentViewModelIsLogViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Show alarm view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowLogViewCommandExecuted(object obj) => CurrentModel = _LogViewModel ??= new LogViewModel(_LogService);

        #endregion ShowLogViewCommand

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

        #region StartWinccCommand

        private ICommand _StartWinccCommand;

        /// <summary>
        /// Default command.
        /// </summary>
        public ICommand StartWinccCommand => _StartWinccCommand ??= new LambdaCommand(OnStartWinccCommandExecuted, CanStartWinccCommandExecute);

        /// <summary>
        /// Can execute default command .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanStartWinccCommandExecute(object arg) => true;

        /// <summary>
        /// Default function.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnStartWinccCommandExecuted(object obj)
        {
            string path = @"C:\Users\benthor\Documents\Automation\KISTIR2\IM\HMI\C\4\Generates\pdata.fwc";
            try
            {
                Process.Start(path);
            }
            catch (Exception e) { _UserDialogService.ShowError(e.Message, "Wincc error"); }
        }

        #endregion StartWinccCommand

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