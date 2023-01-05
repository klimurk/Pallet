using CodingSeb.Localization;
using MaterialDesignThemes.Wpf;
using Pallet.ExternalDatabase.Models;
using Pallet.Infrastructure.Commands;
using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.InternalDatabase.Entities.Users;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.Models;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.View.Dialogs;
using Pallet.View.SubViews;
using Pallet.ViewModels.Base;
using Pallet.ViewModels.SubView;

namespace Pallet.ViewModels.Windows
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public class MainControlViewModel : ViewModel
    {
        private bool _IsSending;

        public MessageToSnackLevel CurrentMessageLevel => _UserDialogService.CurrentMessageLevel;

        public bool IsSending
        {
            get => _IsSending;
            set => Set(ref _IsSending, value);
        }

        #region Services

        private readonly ILogService _LogService;
        private readonly IManagerProfiles _ManagerProfiles;

        private readonly IManagerUser _ManagerUser;
        private readonly IOPC _OPC;
        private readonly IUserDialogService _UserDialogService;

        #endregion Services

        #region Fields

        #region OPC

        #region Collections

        private ObservableCollection<Alarm> _Alarms;

        private ObservableCollection<PalletLog> _PalletLogs;

        private ObservableCollection<Signal> _Signals;

        public ObservableCollection<Alarm> Alarms
        {
            get => _Alarms;
            set => Set(ref value, _Alarms);
        }

        public ObservableCollection<Signal> Signals
        {
            get => _Signals;
            set => Set(ref value, _Signals);
        }

        #endregion Collections

        #region Signals

        public bool? IsAnforderungJobEnd { get => (bool)Signals.FirstOrDefault(s => s.Name == "Anforderung_JobEnd").Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Anforderung_JobEnd").Node).ConfigureAwait(false); }
        public bool? IsAnforderungJobHalt { get => (bool)Signals.FirstOrDefault(s => s.Name == "Anforderung_JobHalt").Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Anforderung_JobHalt").Node).ConfigureAwait(false); }

        public bool? IsAutoMode
        {
            get => (bool)Signals.FirstOrDefault(s => s.Name == "MOD_Auto")?.Value;
            set
            {
                _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Vizu_AutoStart")?.Node).ConfigureAwait(false);
                if (value == true)
                    _OPC.WriteValue(!value, Signals.FirstOrDefault(s => s.Name == "Vizu_AutoStart")?.Node).ConfigureAwait(false);
            }
        }

        public bool? IsDataActual { get => (bool?)Signals.FirstOrDefault(s => s.Name == "AktuellDaten_niO")?.Value; }

        public bool? IsDataReady
        {
            get => (bool?)Signals.FirstOrDefault(s => s.Name == "DatenBereit")?.Value;
            set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "DatenBereit")?.Node).ConfigureAwait(false);
        }

        public bool? IsDataRequest
        {
            get
            {
                if (Signals.FirstOrDefault(s => s.Name == "DatenAnforderung")?.Value is null) return false;

                Set(ref _IsDataRequest, (bool?)Signals.FirstOrDefault(s => s.Name == "DatenAnforderung")?.Value);
                return _IsDataRequest;
            }
        }

        public bool? _IsDataRequest;
        public bool? IsFQuitt { get => (bool?)Signals.FirstOrDefault(s => s.Name == "F_Quitt")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "F_Quitt")?.Node).ConfigureAwait(false); }
        public bool? IsHaveFailure { get => (bool?)Signals.FirstOrDefault(s => s.Name == "Stoerung")?.Value; }
        public bool? IsJobDone { get => (bool?)Signals.FirstOrDefault(s => s.Name == "JobFertig")?.Value; }
        public bool? IsJobQuittierung { get => (bool?)Signals.FirstOrDefault(s => s.Name == "JobQuittierung")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "JobQuittierung")?.Node).ConfigureAwait(false); }

        public bool? IsOP1Acknowledge
        {
            get => (bool?)Signals.FirstOrDefault(s => s.Name == "OP1_Acknowledge")?.Value;
            set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "OP1_Acknowledge")?.Node).ConfigureAwait(false);
        }

        public bool? IsStopMode
        {
            get => (bool?)Signals.FirstOrDefault(s => s.Name == "MOD_Hand")?.Value;
            set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Vizu_Autostop")?.Node).ConfigureAwait(false);
        }

        #endregion Signals

        #region OPC Connection status

        private bool _ConnectionStatus;

        public bool ConnectionStatus
        {
            get
            {
                Set(ref _ConnectionStatus, _OPC.IsConnected);
                return _ConnectionStatus;
            }
        }

        #endregion OPC Connection status

        #region Page internals

        private bool _IsNavigationDrawer;

        public bool IsNavigationDrawer
        {
            get => _IsNavigationDrawer;
            set => Set(ref _IsNavigationDrawer, value);
        }

        public SnackbarMessageQueue MyMessageQueue => _UserDialogService.MessageQueue;

        #endregion Page internals

        #endregion OPC

        #region SQL

        #region User

        private User _User;

        public bool IsUserAuthorized => _ManagerUser.LoginedUser != null;

        public User LoginedUser
        {
            get => _User;
            set => Set(ref _User, value);
        }

        #endregion User

        #region Selected Language

        private Lang _SelectedLang;

        public Lang SelectedLang
        {
            get
            {
                if (_SelectedLang == null)
                {
                    _SelectedLang = Langs.First(s => s.Culture.IetfLanguageTag == Thread.CurrentThread.CurrentCulture.IetfLanguageTag);
                    if (_SelectedLang == null) _SelectedLang = Langs.First(s => s.Culture.IetfLanguageTag == "en");
                    OnPropertyChanged(nameof(SelectedLang));
                }
                return _SelectedLang;
            }

            set
            {
                Loc.Instance.CurrentLanguage = value.Culture.IetfLanguageTag;
                Set(ref _SelectedLang, value);
            }
        }

        #endregion Selected Language

        #region Languages

        private List<Lang> _Langs;

        public List<Lang> Langs
        {
            get => _Langs ??= new()
                {
                    new(Loc.Tr("Languages.English"),  "Resources/Icons/great-britain-48.png", new CultureInfo("en")),
                    new(Loc.Tr("Languages.Czech"), "Resources/Icons/czech-republic-48.png", new CultureInfo("cs-CZ")),
                    new(Loc.Tr("Languages.German"), "Resources/Icons/germany-48.png", new CultureInfo("de-DE"))
                };
            set => Set(ref _Langs, value);
        }

        #endregion Languages

        #endregion SQL

        #region Current Model (View)

        private ViewModel _CurrentModel;

        public ViewModel CurrentModel
        {
            get => _CurrentModel;
            set
            {
                Set(ref _CurrentModel, value);
                IsNavigationDrawer = false;
            }
        }

        #endregion Current Model (View)

        //public string? ContractNum => _ManagerProfiles.Contract?.CNr;
        //public int? CountTaskDone => _ManagerProfiles.RobotTaskItem?.DoneCount;
        //public int? CountTaskMade => _ManagerProfiles.RobotTaskItem?.Count;
        //public string? CustomerName => _ManagerProfiles.Firm?.Kundekrz;
        //public string? NextContractNum => _ManagerProfiles.NextContract?.CNr;
        //public string? NextCustomerName => _ManagerProfiles.NextFirm?.Kundekrz;
        //public string? NextPackageNum => _ManagerProfiles.NextCrateCharacteristics?.VerpNr;
        //public string? PackageNum => _ManagerProfiles.CrateCharacteristics?.VerpNr;
        //public string? TaskElementName => _ManagerProfiles.RobotTaskItem?.CLayerId;
        //public string? TaskNextElementName => _ManagerProfiles.NextRobotTaskItem?.CLayerId;

        public Profile CurrentProfile => _ManagerProfiles.CurrentProfile;
        public Profile NextProfile => _ManagerProfiles.NextProfile;

        #endregion Fields

        #region Constructor - Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainControlViewModel"/> class.
        /// </summary>

        public MainControlViewModel()
        {
            _ManagerUser = App.Services.GetService(typeof(IManagerUser)) as IManagerUser;
            _ManagerProfiles = App.Services.GetService(typeof(IManagerProfiles)) as IManagerProfiles;
            _OPC = App.Services.GetService(typeof(IOPC)) as IOPC;
            _UserDialogService = App.Services.GetService(typeof(IUserDialogService)) as IUserDialogService;
            _LogService = App.Services.GetService(typeof(ILogService)) as ILogService;
            _Signals = _OPC.Signals;
            _Alarms = _OPC.Alarms;

            //_PalletLogs = _LogService.PalletLogs;
            //_Alarms.CollectionChanged -= Alarms_CollectionChanged;
            //_Signals.CollectionChanged -= Signals_CollectionChanged;

            //_Alarms.CollectionChanged += Alarms_CollectionChanged;
            //_Signals.CollectionChanged += Signals_CollectionChanged;

            _Signals.First(s => s.Name == "JobFertig").PropertyChanged += RobotJobDone;
            foreach (var signal in _Signals) signal.PropertyChanged += RefreshEvent;
            foreach (var alarm in _Alarms) alarm.PropertyChanged += RefreshEvent;

            _OPC.DataChanged += RefreshEvent;
            //_OPC.InitializeOPC();
            _UserDialogService.NewSnackBarEventHandler += RefreshEvent;

            _ManagerUser.LoginedUserChanged += _ManagerUser_LoginedUserChanged;
            _ManagerUser.Login("administrator", "btadmin").ConfigureAwait(false);
            //Langs = new()
            //{
            //    new(Loc.Tr("Languages.English"),  "Resources/Icons/great-britain-48.png", new CultureInfo("en")),
            //    new(Loc.Tr("Languages.Czech"), "Resources/Icons/czech-republic-48.png", new CultureInfo("cs-CZ")),
            //    new(Loc.Tr("Languages.German"), "Resources/Icons/germany-48.png", new CultureInfo("de-DE"))
            //};

            //SelectedLang = Langs.FirstOrDefault(s => s.Culture.IetfLanguageTag == Thread.CurrentThread.CurrentCulture.IetfLanguageTag);
            //if (SelectedLang == default) SelectedLang = Langs.First(s => s.Culture.IetfLanguageTag == "cs-CZ");
            AsyncInitialization().ConfigureAwait(false);
        }

        protected override async Task AsyncInitialization()
        {
        }

        #endregion Constructor - Destructor

        #region Events

        private void _ManagerUser_LoginedUserChanged(object? sender, EventArgs e) => LoginedUser = _ManagerUser.LoginedUser;

        private void RobotJobDone(object? sender, PropertyChangedEventArgs e)
        {
            IsSending = false;
            //_PalletLogs.Add(new(_ManagerProfiles.)) // add to log
            _ManagerProfiles.ReadNewTask().ConfigureAwait(false);
        }

        //#region Alarms Collection changed Event

        ///// <summary>
        ///// Alarms collection changed.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        //private void Alarms_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.OldItems is not null)
        //    {
        //        foreach (Alarm item in e.OldItems)
        //        {
        //            item.PropertyChanged -= AlarmsValue_PropertyChanged;
        //        }
        //    }

        //    if (e.NewItems is not null)
        //    {
        //        foreach (Alarm item in e.NewItems)
        //        {
        //            item.PropertyChanged += AlarmsValue_PropertyChanged;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Alarms property changed.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        //private void AlarmsValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //Alarms.Refresh();
        //}

        //#endregion Alarms Collection changed Event

        //#region Signals Collection changed Event

        ///// <summary>
        ///// Signals collection changed.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        //private void Signals_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.OldItems != null)
        //    {
        //        foreach (Signal item in e.OldItems)
        //        {
        //            item.PropertyChanged -= SignalsValue_PropertyChanged;
        //        }
        //    }

        //    if (e.NewItems != null)
        //    {
        //        foreach (Signal item in e.NewItems)
        //        {
        //            item.PropertyChanged += SignalsValue_PropertyChanged;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Signals property changed.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        //private void SignalsValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //Signals.Refresh();
        //}

        //#endregion Signals Collection changed Event

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
        private bool CanSendProfileCommandExecute(object arg) =>
            IsDataRequest == true
            //&& !IsSending
            && _ManagerProfiles.CurrentTask != null
            && _OPC.IsConnected;

        //&& (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        /// <summary>
        /// Sending  profile.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private async void OnSendProfileCommandExecuted(object obj)
        {
            IsSending = true;
            WriteTaskNails(CurrentProfile.Nails.ToList()).ConfigureAwait(false);
            IsDataReady = true;
        }

        #endregion SendProfileCommand

        //#region SendProfileBackwardCommand

        //private ICommand _SendProfileBackwardCommand;

        ///// <summary>
        ///// Send profile command.
        ///// </summary>
        //public ICommand SendProfileBackwardCommand => _SendProfileBackwardCommand ??= new LambdaCommand(OnSendProfileBackwardCommandExecuted, CanSendProfileBackwardCommandExecute);

        ///// <summary>
        ///// Can Send  profile .
        ///// </summary>
        ///// <param name="arg">The arg.</param>
        ///// <returns>A bool.</returns>
        //private bool CanSendProfileBackwardCommandExecute(object arg) =>
        //    IsDataRequest == true
        //    && !IsSending
        //    && _ManagerProfiles.CurrentTask != null
        //    && _OPC.IsConnected;

        ////&& (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        ///// <summary>
        ///// Sending  profile.
        ///// </summary>
        ///// <param name="obj">The obj.</param>
        //private void OnSendProfileBackwardCommandExecuted(object obj)
        //{
        //    IsSending = true;
        //    WriteTaskNails(_ManagerProfiles.GetTaskNails().OrderByDescending(s => s.NOrder).ToList()).ConfigureAwait(false);

        //    IsDataReady = true;
        //    IsSending = false;
        //}

        //#endregion SendProfileBackwardCommand

        //#region SendProfileRandomCommand

        //private ICommand _SendProfileRandomCommand;

        ///// <summary>
        ///// Send profile command.
        ///// </summary>
        //public ICommand SendProfileRandomCommand => _SendProfileRandomCommand ??= new LambdaCommand(OnSendProfileRandomCommandExecuted, CanSendProfileRandomCommandExecute);

        ///// <summary>
        ///// Can Send  profile .
        ///// </summary>
        ///// <param name="arg">The arg.</param>
        ///// <returns>A bool.</returns>
        //private bool CanSendProfileRandomCommandExecute(object arg) =>
        //    IsDataRequest == true
        //    && !IsSending
        //    && _ManagerProfiles.CurrentTask != null
        //    && _OPC.IsConnected;

        ////&& (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        ///// <summary>
        ///// Sending  profile.
        ///// </summary>
        ///// <param name="obj">The obj.</param>
        //private void OnSendProfileRandomCommandExecuted(object obj)
        //{
        //    IsSending = true;
        //    WriteTaskNails(_ManagerProfiles.GetTaskNails().ToList().Shuffle(new Random()).ToList()).ConfigureAwait(false);
        //    IsDataReady = true;
        //    IsSending = false;
        //}

        //#endregion SendProfileRandomCommand

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
        private bool CanSetAutoModeCommandExecute(object arg) =>
            IsAutoMode == false
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker
            && _OPC.IsConnected;

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
        private bool CanResetAutoModeCommandExecute(object arg) =>
            IsAutoMode == true
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker
            && _OPC.IsConnected;

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
        private bool CanSetStopModeCommandExecute(object arg) =>
            IsStopMode == false
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker
            && _OPC.IsConnected;

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
        private bool CanResetStopModeCommandExecute(object arg) =>
            IsStopMode == true
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker
            && _OPC.IsConnected;

        /// <summary>
        /// Resetting stop mode .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnResetStopModeCommandExecuted(object obj) => IsStopMode = false;

        #endregion ResetStopModeCommand

        #endregion StopMode

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
        private bool CanShowManualViewCommandExecute(object arg) => CurrentModel is not ManualViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin;

        /// <summary>
        /// Show manual view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowManualViewCommandExecuted(object obj) => CurrentModel = new ManualViewModel();

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
        private bool CanShowPalletViewCommandExecute(object arg) => CurrentModel is not PalletViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin;

        /// <summary>
        /// Show pallet view .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowPalletViewCommandExecuted(object obj)
        {
            //CurrentModel?.Dispose();
            CurrentModel = new PalletViewModel();
        }

        #endregion ShowPalletViewCommand

        #region ShowPalletLogCommand

        private ICommand _ShowPalletLogCommand;

        /// <summary>
        /// Show pallet view command.
        /// </summary>
        public ICommand ShowPalletLogCommand => _ShowPalletLogCommand ??= new LambdaCommand(OnShowPalletLogCommandExecuted, CanShowPalletLogCommandExecute);

        /// <summary>
        /// Can show pallet view.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanShowPalletLogCommandExecute(object arg) => CurrentModel is not PalletLogViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin;

        /// <summary>
        /// Show pallet view .
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowPalletLogCommandExecuted(object obj)
        {
            //CurrentModel?.Dispose();
            CurrentModel = new PalletLogViewModel();
        }

        #endregion ShowPalletLogCommand

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
        private bool CanShowAlarmViewCommandExecute(object arg) => CurrentModel is not AlarmViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin;

        /// <summary>
        /// Show alarm view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowAlarmViewCommandExecuted(object obj) => CurrentModel = new AlarmViewModel();

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
        private bool CanShowLogViewCommandExecute(object arg) => CurrentModel is not LogViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Show alarm view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowLogViewCommandExecuted(object obj) => CurrentModel = new LogViewModel();

        #endregion ShowLogViewCommand

        #region ShowUsersViewCommand

        private ICommand _ShowUsersViewCommand;

        /// <summary>
        /// Show alarm view command.
        /// </summary>
        public ICommand ShowUsersViewCommand => _ShowUsersViewCommand ??= new LambdaCommand(OnShowUsersViewCommandExecuted, CanShowUsersViewCommandExecute);

        /// <summary>
        /// Can show alarm view .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanShowUsersViewCommandExecute(object arg) => CurrentModel is not UsersViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin;

        /// <summary>
        /// Show alarm view.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnShowUsersViewCommandExecuted(object obj) => CurrentModel = new UsersViewModel();

        #endregion ShowUsersViewCommand

        #endregion ViewModelCommands

        #region LogoutCommand

        private ICommand _LogoutCommand;
        public ICommand LogoutCommand => _LogoutCommand ??= new LambdaCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);

        /// <summary>
        /// Cans logout .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanLogoutCommandExecute(object arg) => _ManagerUser.LoginedUser != null;

        /// <summary>
        /// Logout.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnLogoutCommandExecuted(object obj)
        {
            _ManagerUser.LogOut();
            _UserDialogService.ShowSnackbarInfo("You have been logout");
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
        private bool CanStartWinccCommandExecute(object arg) => _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Admin;

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
            catch (Exception e) { _UserDialogService.ShowDialogError(e.Message, "Wincc error"); }
        }

        #endregion StartWinccCommand

        #region OpenLoginWindowCommand

        private ICommand _OpenLoginWindowCommand;

        /// <summary>
        /// OpenLoginWindow command.
        /// </summary>
        public ICommand OpenLoginWindowCommand => _OpenLoginWindowCommand ??= new LambdaCommand(OnOpenLoginWindowCommandExecuted, CanOpenLoginWindowCommandExecute);

        /// <summary>
        /// Can execute default command .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanOpenLoginWindowCommandExecute(object arg) => true;

        /// <summary>
        /// OpenLoginWindow function.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private async void OnOpenLoginWindowCommandExecuted(object obj)
        {
            var view = new LoginWindow
            {
                DataContext = new LoginViewModel()
            };
            await DialogHost.Show(view, MainControl.DialogName);
            OnPropertyChanged(nameof(LoginedUser));
            IsNavigationDrawer = false;
        }

        #endregion OpenLoginWindowCommand

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

        public SnackbarMessage Message
        {
            get => _UserDialogService.Message;
            set => _UserDialogService.Message = value;
        }

        private async Task WriteTaskNails(List<NailingData> nails)
        {
            if (nails == null) throw new ArgumentNullException("Nails list is empty");

            List<int> nailsInt = new();

            List<bool> nailsbools = new();
            for (int i = 0; i < nails.Count; i++)
            {
                nailsInt.Add(nails[i].NX * 10);
                nailsInt.Add(nails[i].NY * 10);

                nailsInt.Add(nails[i].NZ * 10);

                nailsbools.Add(true);

                nailsbools.Add(i < 10 ? true : nails[i].NSaveShot);
            }
            await _OPC.WriteList(
                nailsInt,
                ManagerProfiles.OPCData.Nails.DBName,
                ManagerProfiles.OPCData.Nails.DBVar,
                new List<string>()
                {
                ManagerProfiles.OPCData.Nails.Fields.CoorX,
                ManagerProfiles.OPCData.Nails.Fields.CoorY,
                ManagerProfiles.OPCData.Nails.Fields.CoorZ
                },
                ManagerProfiles.OPCData.Nails.DBNamespace
                );

            await _OPC.WriteList(
                nailsbools,
                ManagerProfiles.OPCData.Nails.DBName,
                ManagerProfiles.OPCData.Nails.DBVar,
                new List<string>()
                {
                    ManagerProfiles.OPCData.Nails.Fields.Active,
                    ManagerProfiles.OPCData.Nails.Fields.FixNail
                },
                ManagerProfiles.OPCData.Nails.DBNamespace
                );
        }
    }
}