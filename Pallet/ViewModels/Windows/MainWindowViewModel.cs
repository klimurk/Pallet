using Pallet.Database.Entities.Change.Types;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.Users;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Entities.Models;
using Pallet.Infrastructure.Commands;
using Pallet.Models;
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

        private readonly IUserDialogService _UserDialogService;

        private readonly IManagerLanguage _LanguageManager;
        private readonly IManagerUser _ManagerUser;
        private readonly IOPC _OPCProxy;

        //private readonly ResourceManager _ResourceManager;

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

        public ObservableCollection<AlarmOPC> Alarms
        {
            get => _Alarms;
            set => Set(ref _Alarms, value);
        }

        private ObservableCollection<AlarmOPC> _Alarms;

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

        private ObservableCollection<ProfileInfoData> ProfilesInfoData
        {
            get => _ProfilesInfoData;
            set
            {
                if (Set(ref _ProfilesInfoData, value))
                {
                    _ProfileViewSource.Source = value;
                    _ProfileViewSource.View.Refresh();
                }
                OnPropertyChanged(nameof(ProfilesView));
            }
        }

        private ObservableCollection<ProfileInfoData> _ProfilesInfoData;

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
        public ProfileInfoData ActiveProfile
        {
            get => _ActiveProfile;
            set => Set(ref _ActiveProfile, value);
        }

        private ProfileInfoData _ActiveProfile;

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

        public bool _IsCurrentViewModelIsPalletViewModel;

        public bool IsCurrentViewModelIsManualViewModel
        {
            get => _IsCurrentViewModelIsManualViewModel;
            set => Set(ref _IsCurrentViewModelIsManualViewModel, value);
        }

        public bool _IsCurrentViewModelIsManualViewModel;

        #endregion Current Model (View)

        #endregion Fields

        #region Constructor - Destructor

        public MainWindowViewModel(
            IDbRepository<Signal> RepositorySignals,
            IDbRepository<Alarm> RepositoryAlarms,
            IManagerProfiles ManagerProfiles,
            IManagerUser UserManager,
            IManagerLanguage languageManager,
            IUserDialogService UserDialogService,
            IOPC OPCProxy
            )
        {
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
                Source = ProfilesInfoData,
                SortDescriptions =
                {
                    new SortDescription(nameof(ProfileInfoData.DateLastUse), ListSortDirection.Descending)
                }
            };
            _ProfileViewSource.Filter += ProfileListSource_Filter;

            ProfilesInfoData = new();

            foreach (var profile in _ManagerProfiles.Items.OrderByDescending(profile => profile.DateLastUse).ToArray())
            {
                ProfilesInfoData.Add(new(profile));
            }

            Signals = _OPCProxy.Signals;
            Alarms = _OPCProxy.Alarms;

            Alarms.CollectionChanged += Alarms_CollectionChanged;
            Signals.CollectionChanged += Signals_CollectionChanged;

            new Thread(() => InitializeOPC()).Start();

            ActiveProfile = (ProfileInfoData)_ManagerProfiles.GetActiveProfileInfoData();
            ActiveProfile.PropertyChanged += ActiveProfile_PropertyChanged;
        }

        #endregion Constructor - Destructor

        #region Private Functions

        private async Task InitializeOPC()
        {
            _OPCProxy.Connect();
            _OPCProxy.AddSubcribeFolder(Services.OPC.OPCProxy.SubForlderAlarm);
            _OPCProxy.AddSubcribeFolder(Services.OPC.OPCProxy.SubForlderSystem);

            foreach (var alarm in _RepositoryAlarms.Items.ToArray())
            {
                AlarmOPC temp = new()
                {
                    Info = alarm,
                    NodeOPC = _OPCProxy.GetNode(alarm.Address),
                    Value = new bool()
                };
                _OPCProxy.SubscribeValue(temp, Services.OPC.OPCProxy.SubForlderAlarm);
            }
            foreach (var signal in _RepositorySignals.Items.ToArray())
            {
                SignalOPC temp = new()
                {
                    Info = signal,
                    NodeOPC = _OPCProxy.GetNode(signal.Address),
                    Value = new bool()
                };
                _OPCProxy.SubscribeValue(temp, Services.OPC.OPCProxy.SubForlderSystem);
            }

            IsAutoMode = Signals.First(s => s.Info.Name == "M_Auto");
            IsStopMode = Signals.First(s => s.Info.Name == "M_Halt");
        }

        #endregion Private Functions

        #region Events

        #region Active profile property changed event

        /// <summary>
        /// Active profile property changed - must refresh profiles view to show active profile.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ActiveProfile_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ProfilesInfoData.FirstOrDefault(profile => profile.Name == ActiveProfile.Name).DateLastUse = ActiveProfile.DateLastUse;
            ProfilesView.Refresh();
            //_ProfileViewSource.View.Refresh();
            OnPropertyChanged(nameof(ActiveProfile));
        }

        #endregion Active profile property changed event

        #region Profiles filter event

        /// <summary>
        /// _S the profile list source_ filter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ProfileListSource_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item is not ProfileInfoData profile || string.IsNullOrEmpty(FilterName)) return;
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
                foreach (AlarmOPC item in e.OldItems)
                {
                    item.PropertyChanged -= AlarmsValue_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (AlarmOPC item in e.NewItems)
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

        #region SendDataProfileCommand

        private ICommand _SendDataProfileCommand;
        public ICommand SendDataProfileCommand => _SendDataProfileCommand ??= new LambdaCommand(OnSendDataProfileCommandExecuted, CanSendDataProfileCommandExecute);

        private bool CanSendDataProfileCommandExecute(object arg) => _ManagerProfiles.ActiveProfile != null && (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        private void OnSendDataProfileCommandExecuted(object obj) => _OPCProxy.WriteProfile(_ManagerProfiles.ActiveProfile);

        #endregion SendDataProfileCommand

        #region AutoMode

        #region SetAutoModeCommand

        private ICommand _SetAutoModeCommand;
        public ICommand SetAutoModeCommand => _SetAutoModeCommand ??= new LambdaCommand(OnSetAutoModeCommandExecuted, CanSetAutoModeCommandExecute);

        private bool CanSetAutoModeCommandExecute(object arg) => !(bool)IsAutoMode.Value && !(bool)IsStopMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetAutoModeCommandExecuted(object obj)
        { IsAutoMode.Value = true; _OPCProxy.WriteActualValue((bool)IsAutoMode.Value, IsAutoMode.NodeOPC); }

        #endregion SetAutoModeCommand

        #region ResetAutoModeCommand

        private ICommand _ResetAutoModeCommand;
        public ICommand ResetAutoModeCommand => _ResetAutoModeCommand ??= new LambdaCommand(OnResetAutoModeCommandExecuted, CanResetAutoModeCommandExecute);

        private bool CanResetAutoModeCommandExecute(object arg) => (bool)IsAutoMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetAutoModeCommandExecuted(object obj)
        { IsAutoMode.Value = false; _OPCProxy.WriteActualValue((bool)IsAutoMode.Value, IsAutoMode.NodeOPC); }

        #endregion ResetAutoModeCommand

        #endregion AutoMode

        #region StopMode

        #region SetStopModeCommand

        private ICommand _SetStopModeCommand;
        public ICommand SetStopModeCommand => _SetStopModeCommand ??= new LambdaCommand(OnSetStopModeCommandExecuted, CanSetStopModeCommandExecute);

        private bool CanSetStopModeCommandExecute(object arg) => !(bool)IsStopMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnSetStopModeCommandExecuted(object obj)
        {
            IsStopMode.Value = true; _OPCProxy.WriteActualValue((bool)IsStopMode.Value, IsStopMode.NodeOPC);
            IsAutoMode.Value = false; _OPCProxy.WriteActualValue((bool)IsAutoMode.Value, IsAutoMode.NodeOPC);
        }

        #endregion SetStopModeCommand

        #region ResetStopModeCommand

        private ICommand _ResetStopModeCommand;
        public ICommand ResetStopModeCommand => _ResetStopModeCommand ??= new LambdaCommand(OnResetStopModeCommandExecuted, CanResetStopModeCommandExecute);

        private bool CanResetStopModeCommandExecute(object arg) => (bool)IsStopMode.Value && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnResetStopModeCommandExecuted(object obj)
        { IsStopMode.Value = false; _OPCProxy.WriteActualValue((bool)IsStopMode.Value, IsStopMode.NodeOPC); }

        #endregion ResetStopModeCommand

        #endregion StopMode

        #region OPCConnectCommand

        private ICommand _OPCConnectCommand;
        public ICommand OPCConnectCommand => _OPCConnectCommand ??= new LambdaCommand(OnOPCConnectCommandExecuted, CanOPCConnectCommandExecute);

        private bool CanOPCConnectCommandExecute(object arg) => !ConnectionStatus && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnOPCConnectCommandExecuted(object obj) => _OPCProxy.Reconnect();

        #endregion OPCConnectCommand

        #endregion OPCCommands

        #region ViewModelCommands

        #region ShowManualViewCommand

        private ICommand _ShowManualViewCommand;
        public ICommand ShowManualViewCommand => _ShowManualViewCommand ??= new LambdaCommand(OnShowManualViewCommandExecuted, CanShowManualViewCommandExecute);

        private bool CanShowManualViewCommandExecute(object arg) => !IsCurrentViewModelIsManualViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Manager;

        private void OnShowManualViewCommandExecuted(object obj)
        {
            //CurrentModel?.Dispose();
            CurrentModel = _ManualViewModel ??= new ManualViewModel();
        }

        #endregion ShowManualViewCommand

        #region ShowPalletViewCommand

        private ICommand _ShowPalletViewCommand;
        public ICommand ShowPalletViewCommand => _ShowPalletViewCommand ??= new LambdaCommand(OnShowPalletViewCommandExecuted, CanShowPalletViewCommandExecute);

        private bool CanShowPalletViewCommandExecute(object arg) => !IsCurrentViewModelIsPalletViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnShowPalletViewCommandExecuted(object obj)
        {
            //CurrentModel?.Dispose();
            CurrentModel = _PalletViewModel ??= new PalletViewModel();
        }

        #endregion ShowPalletViewCommand

        #region ShowAlarmViewCommand

        private ICommand _ShowAlarmViewCommand;
        public ICommand ShowAlarmViewCommand => _ShowAlarmViewCommand ??= new LambdaCommand(OnShowAlarmViewCommandExecuted, CanShowAlarmViewCommandExecute);

        private bool CanShowAlarmViewCommandExecute(object arg) => !IsCurrentViewModelIsAlarmViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        private void OnShowAlarmViewCommandExecuted(object obj)
        {
            //CurrentModel?.Dispose();
            CurrentModel = _AlarmViewModel ??= new AlarmViewModel();
        }

        #endregion ShowAlarmViewCommand

        #endregion ViewModelCommands

        #region LogoutCommand

        private ICommand _LogoutCommand;
        public ICommand LogoutCommand => _LogoutCommand ??= new LambdaCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);

        private bool CanLogoutCommandExecute(object arg) => _ManagerUser.IsLogined;

        private void OnLogoutCommandExecuted(object obj)
        {
            _ManagerUser.LogOut();
            User = null;
            _UserDialogService.ShowInformation("You have been logout", "Login");
        }

        #endregion LogoutCommand

        #region DefaultCommand

        private ICommand _DefaultCommand;

        public ICommand DefaultCommand => _DefaultCommand ??= new LambdaCommand(OnDefaultCommandExecuted, CanDefaultCommandExecute);

        private bool CanDefaultCommandExecute(object arg) => true;

        private void OnDefaultCommandExecuted(object obj)
        { }

        #endregion DefaultCommand

        #endregion Commands
    }
}