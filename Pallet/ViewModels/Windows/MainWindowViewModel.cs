using CodingSeb.Localization;
using MaterialDesignThemes.Wpf;
using Pallet.Extensions;
using Pallet.Infrastructure.Commands;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.InternalDatabase.Entities.Users;
using Pallet.Services.Language;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.Models;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Pallet.View.Dialogs;
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

        private readonly IManagerProfiles _ManagerProfiles;

        private readonly IUserDialogService _UserDialogService;

        private readonly IManagerLanguage _ManagerLanguage;
        private readonly IManagerUser _ManagerUser;
        private readonly IOPC _OPC;

        #endregion Services

        #region Fields

        #region OPC

        #region Collections

        public ObservableCollection<Alarm> Alarms
        {
            get => _Alarms;
            set => Set(ref value, _Alarms);
        }

        private ObservableCollection<Alarm> _Alarms;

        public ObservableCollection<Signal> Signals
        {
            get => _Signals;
            set => Set(ref value, _Signals);
        }

        private ObservableCollection<Signal> _Signals;

        #endregion Collections

        #region Signals

        public bool? IsAnforderungJobEnd { get => (bool)Signals.FirstOrDefault(s => s.Name == "Anforderung_JobEnd").Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Anforderung_JobEnd").Node); }
        public bool? IsAnforderungJobHalt { get => (bool)Signals.FirstOrDefault(s => s.Name == "Anforderung_JobHalt").Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Anforderung_JobHalt").Node); }

        public bool? IsAutoMode
        {
            get => (bool)Signals.FirstOrDefault(s => s.Name == "MOD_Auto")?.Value;
            set
            {
                _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Vizu_AutoStart")?.Node);
                if (value == true)
                    _OPC.WriteValue(!value, Signals.FirstOrDefault(s => s.Name == "Vizu_AutoStart")?.Node);
            }
        }

        public bool? IsDataActual { get => (bool)Signals.FirstOrDefault(s => s.Name == "AktuellDaten_niO")?.Value; }
        public bool? IsDataReady { get => (bool)Signals.FirstOrDefault(s => s.Name == "DatenBereit")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "DatenBereit")?.Node); }

        public bool? IsDataRequest => (bool)Signals.FirstOrDefault(s => s.Name == "DatenAnforderung")?.Value;

        public bool? IsFQuitt { get => (bool)Signals.FirstOrDefault(s => s.Name == "F_Quitt")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "F_Quitt")?.Node); }
        public bool? IsHaveFailure { get => (bool)Signals.FirstOrDefault(s => s.Name == "Stoerung")?.Value; }
        public bool? IsJobDone { get => (bool)Signals.FirstOrDefault(s => s.Name == "JobFertig")?.Value; }
        public bool? IsJobQuittierung { get => (bool)Signals.FirstOrDefault(s => s.Name == "JobQuittierung")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "JobQuittierung")?.Node); }
        public bool? IsOP1Acknowledge { get => (bool)Signals.FirstOrDefault(s => s.Name == "OP1_Acknowledge")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "OP1_Acknowledge")?.Node); }
        public bool? IsStopMode { get => (bool)Signals.FirstOrDefault(s => s.Name == "MOD_Hand")?.Value; set => _OPC.WriteValue(value, Signals.FirstOrDefault(s => s.Name == "Vizu_Autostop")?.Node); }

        #endregion Signals

        #region OPC Connection status

        public bool ConnectionStatus
        {
            get
            {
                Set(ref _ConnectionStatus, _OPC.IsConnected);
                return _ConnectionStatus;
            }
        }

        private bool _ConnectionStatus;

        #endregion OPC Connection status

        #region Page internals

        public bool IsNavigationDrawer
        {
            get => _IsNavigationDrawer;
            set => Set(ref _IsNavigationDrawer, value);
        }

        private bool _IsNavigationDrawer;
        public SnackbarMessageQueue MyMessageQueue => _UserDialogService.MessageQueue;

        #endregion Page internals

        #endregion OPC

        #region SQL

        #region User

        public User LoginedUser
        {
            get => _User;
            set => Set(ref _User, value);
        }

        private User _User;
        public bool IsUserAuthorized => _ManagerUser.LoginedUser != null;

        #endregion User

        #region Selected Language

        public Lang SelectedLang
        {
            get => _SelectedLang;
            set
            {
                Loc.Instance.CurrentLanguage = value.Culture.IetfLanguageTag;
                //try
                //{
                //    CultureManager.UICulture = value.Culture;
                //}
                //catch { }
                Set(ref _SelectedLang, value);
                //_ManagerLanguage.SelectedLang = value;
            }
        }

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
                IsNavigationDrawer = false;
            }
        }

        private ViewModel _CurrentModel;

        #endregion Current Model (View)

        public int? CountTaskMade => _ManagerProfiles.RobotTaskItem?.Count;
        public int? CountTaskDone => _ManagerProfiles.RobotTaskItem?.DoneCount;

        public string? CustomerName => _ManagerProfiles.Firm?.Kundekrz;
        public string? ContractNum => _ManagerProfiles.Contract?.CNr;
        public string? PackaeNum => _ManagerProfiles.CrateCharacteristics?.VerpNr; // todo: check if this or not

        public string? TaskElementName => _ManagerProfiles.RobotTaskItem?.CLayerId;
        public string? TaskNextElementName => _ManagerProfiles.NextRobotTaskItem?.CLayerId;
        public string? NextCustomerName => _ManagerProfiles.NextFirm?.Kundekrz;
        public string? NextContractNum => _ManagerProfiles.NextContract?.CNr;
        public string? NextPackaeNum => _ManagerProfiles.NextCrateCharacteristics?.VerpNr; // todo: check if this or not

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
        /// <param name="OPCProxy">The OPC proxy.</param>
        public MainWindowViewModel(

            IManagerProfiles ManagerProfiles,
            IManagerUser ManagerUser,
            IManagerLanguage ManagerLanguage,
            IUserDialogService UserDialogService,
            IOPC OPCProxy
            )
        {

            _ManagerLanguage = ManagerLanguage;
            _ManagerUser = ManagerUser;
            _ManagerProfiles = ManagerProfiles;
            _OPC = OPCProxy;
            _UserDialogService = UserDialogService;

            //_ResourceManager = new ResourceManager("Pallet.Resources.Windows.MainWindow.MainWindowResource", Assembly.GetExecutingAssembly());

            SelectedLang = _ManagerLanguage.Langs.First(s => s.Culture.IetfLanguageTag == "en");
            Langs = _ManagerLanguage.Langs;

            _Signals = _OPC.Signals;
            _Alarms = _OPC.Alarms;

            _Alarms.CollectionChanged -= Alarms_CollectionChanged;
            _Signals.CollectionChanged -= Signals_CollectionChanged;

            _Alarms.CollectionChanged += Alarms_CollectionChanged;
            _Signals.CollectionChanged += Signals_CollectionChanged;

            _OPC.DataChanged += _OPC_DataChanged;

            _ManagerUser.LoginedUserChanged += _ManagerUser_LoginedUserChanged;
            _ManagerUser.Login("administrator", "btadmin");

            // _LogService.MakeLog(_RepositorySystemEvents.Items.First(s => s.Name == "StartApp"));
        }

        private void _OPC_DataChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(default);
        }

        private void _ManagerUser_LoginedUserChanged(object? sender, EventArgs e) => LoginedUser = _ManagerUser.LoginedUser;

        #endregion Constructor - Destructor

        #region Events

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
        private void AlarmsValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Alarms.Refresh();
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
        private void SignalsValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Signals.Refresh();
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
        private bool CanSendProfileCommandExecute(object arg) =>
            IsDataRequest == true
            && _ManagerProfiles.CurrentTask != null
            && _OPC.IsConnected;

        //&& (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        /// <summary>
        /// Sending  profile.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSendProfileCommandExecuted(object obj)
        {
            _OPC.WriteTaskNails(_ManagerProfiles.GetTaskNails());
            IsDataReady = true;
        }

        #endregion SendProfileCommand

        #region SendProfileBackwardCommand

        private ICommand _SendProfileBackwardCommand;

        /// <summary>
        /// Send profile command.
        /// </summary>
        public ICommand SendProfileBackwardCommand => _SendProfileBackwardCommand ??= new LambdaCommand(OnSendProfileBackwardCommandExecuted, CanSendProfileBackwardCommandExecute);

        /// <summary>
        /// Can Send  profile .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanSendProfileBackwardCommandExecute(object arg) =>
            IsDataRequest == true
            && _ManagerProfiles.CurrentTask != null
            && _OPC.IsConnected;

        //&& (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        /// <summary>
        /// Sending  profile.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSendProfileBackwardCommandExecuted(object obj)
        {
            _OPC.WriteTaskNails(_ManagerProfiles.GetTaskNails().OrderByDescending(s => s.NOrder).ToList());

            IsDataReady = true;
        }

        #endregion SendProfileBackwardCommand

        #region SendProfileRandomCommand

        private ICommand _SendProfileRandomCommand;

        /// <summary>
        /// Send profile command.
        /// </summary>
        public ICommand SendProfileRandomCommand => _SendProfileRandomCommand ??= new LambdaCommand(OnSendProfileRandomCommandExecuted, CanSendProfileRandomCommandExecute);

        /// <summary>
        /// Can Send  profile .
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns>A bool.</returns>
        private bool CanSendProfileRandomCommandExecute(object arg) =>
            IsDataRequest == true
            && _ManagerProfiles.CurrentTask != null
            && _OPC.IsConnected;

        //&& (_ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker);

        /// <summary>
        /// Sending  profile.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnSendProfileRandomCommandExecuted(object obj)
        {
            _OPC.WriteTaskNails(_ManagerProfiles.GetTaskNails().Shuffle(new Random()).ToList());
            IsDataReady = true;
        }

        #endregion SendProfileRandomCommand

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
            && IsStopMode == false
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
            (bool)IsStopMode
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker
            && _OPC.IsConnected;

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
        private bool CanOPCConnectCommandExecute(object arg) =>
            !_OPC.IsConnected
            && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

        /// <summary>
        /// Resetting OPC connection.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OnOPCConnectCommandExecuted(object obj) => Task.Run(() => _OPC.InitializeOPC());

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
        private bool CanShowManualViewCommandExecute(object arg) => CurrentModel is not ManualViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Manager;

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
        private bool CanShowPalletViewCommandExecute(object arg) => CurrentModel is not PalletViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

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
        private bool CanShowAlarmViewCommandExecute(object arg) => CurrentModel is not AlarmViewModel && _ManagerUser.LoginedUser?.RoleNum >= (int)IManagerUser.UserRoleNum.Worker;

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
            _UserDialogService.ShowInformation("You have been logout");
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
                //Owner = Application.Current.MainWindow,
                //WindowStartupLocation = WindowStartupLocation.CenterOwner,
                DataContext = new LoginViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, MainWindow.DialogName);
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
    }
}