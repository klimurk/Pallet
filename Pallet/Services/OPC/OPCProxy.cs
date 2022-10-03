using Microsoft.Extensions.Configuration;
using Opc.Ua;
using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog.Interfaces;

namespace Pallet.Services.OPC;

internal class OPCProxy : IOPC
{
    #region Services

    private readonly IAlarmLogService _AlarmLogService;
    private readonly IOPC _ConnectorOPC;
    private readonly ILogService _LogsService;
    private readonly IManagerProfiles _ManagerProfiles;
    private readonly IDbRepository<SystemEvent> _SystemEvents;

    #endregion Services

    #region Fields

    #region Static OPC

    public const string SubForlderAlarm = "Alarm";
    public const string SubForlderSystem = "System";

    private readonly Signal _IsAnforderungJobEnd;
    private readonly Signal _IsAnforderungJobHalt;

    private readonly Signal _IsAutoModeRead;
    private readonly Signal _IsAutoModeWrite;

    private readonly Signal _IsDataActual;
    private readonly Signal _IsDataReady;
    private readonly Signal _IsDataRequest;
    private readonly Signal _IsFQuitt;
    private readonly Signal _IsHaveFailure;
    private readonly Signal _IsJobDone;
    private readonly Signal _IsJobQuittierung;
    private readonly Signal _IsOP1Acknowledge;
    private readonly Signal _IsStopModeRead;
    private readonly Signal _IsStopModeWrite;
    public bool? IsAnforderungJobEnd { get => (bool)_IsAnforderungJobEnd.Value; set => WriteActualValue(value, _IsAnforderungJobEnd.Node); }
    public bool? IsAnforderungJobHalt { get => (bool)_IsAnforderungJobHalt.Value; set => WriteActualValue(value, _IsAnforderungJobHalt.Node); }

    public bool? IsAutoMode
    {
        get => (bool)_IsAutoModeRead.Value;
        set
        {
            WriteActualValue(value, _IsAutoModeWrite.Node);
            if (value == true)
                WriteActualValue(!value, _IsAutoModeWrite.Node);
        }
    }

    public bool? IsDataActual { get => (bool)_IsDataActual.Value; }
    public bool? IsDataReady { get => (bool)_IsDataReady.Value; set => WriteActualValue(value, _IsDataReady.Node); }

    public bool? IsDataRequest => (bool)_IsDataRequest.Value;

    public bool? IsFQuitt { get => (bool)_IsFQuitt.Value; set => WriteActualValue(value, _IsFQuitt.Node); }
    public bool? IsHaveFailure { get => (bool)_IsHaveFailure.Value; }
    public bool? IsJobDone { get => (bool)_IsJobDone.Value; }
    public bool? IsJobQuittierung { get => (bool)_IsJobQuittierung.Value; set => WriteActualValue(value, _IsJobQuittierung.Node); }
    public bool? IsOP1Acknowledge { get => (bool)_IsOP1Acknowledge.Value; set => WriteActualValue(value, _IsOP1Acknowledge.Node); }
    public bool? IsStopMode { get => (bool)_IsStopModeRead.Value; set => WriteActualValue(value, _IsStopModeWrite.Node); }

    #endregion Static OPC

    private readonly AutoResetEvent _AutoResetEvent = new(true);
    public ObservableCollection<Alarm> Alarms { get; set; }

    public ObservableCollection<Signal> Signals { get; set; }

    #endregion Fields

    #region Ctor

    public OPCProxy(
        IAlarmLogService AlarmLogService,
        IManagerProfiles ManagerProfiles,
        IDbRepository<Signal> SignalsRepository,
        IDbRepository<Alarm> AlarmsRepository,
        IDbRepository<SystemEvent> SystemEvents,
        ILogService LogsService,
        IUserDialogService UserDialogService,
        IConfiguration Configuration)
    {
        Signals = new();
        Alarms = new();
        _ManagerProfiles = ManagerProfiles;
        _ConnectorOPC = new OPCConnector(SystemEvents, LogsService, UserDialogService, this, Configuration["OPC:ConnectionStrings:" + Configuration["OPC:Type"]]);
        _LogsService = LogsService;
        _SystemEvents = SystemEvents;
        _AlarmLogService = AlarmLogService;

        Signals.Add(SignalsRepository.Items);
        Alarms.Add(AlarmsRepository.Items);

        new Thread(() => InitializeOPC()) { IsBackground = true }.Start();

        _IsAutoModeWrite = Signals.First(s => s.Name == "Vizu_AutoStart");
        _IsStopModeWrite = Signals.First(s => s.Name == "Vizu_Autostop");
        _IsAutoModeRead = Signals.First(s => s.Name == "MOD_Auto");
        _IsStopModeRead = Signals.First(s => s.Name == "MOD_Hand");
        _IsDataRequest = Signals.First(s => s.Name == "DatenAnforderung");
        _IsJobDone = Signals.First(s => s.Name == "JobFertig");
        _IsDataActual = Signals.First(s => s.Name == "AktuellDaten_niO");
        _IsHaveFailure = Signals.First(s => s.Name == "Stoerung");
        _IsDataReady = Signals.First(s => s.Name == "DatenBereit");
        _IsJobQuittierung = Signals.First(s => s.Name == "JobQuittierung");
        _IsAnforderungJobHalt = Signals.First(s => s.Name == "Anforderung_JobHalt");
        _IsAnforderungJobEnd = Signals.First(s => s.Name == "Anforderung_JobEnd");
        _IsOP1Acknowledge = Signals.First(s => s.Name == "OP1_Acknowledge");
        _IsFQuitt = Signals.First(s => s.Name == "F_Quitt");
    }

    public async Task InitializeOPC()
    {
        Connect().Wait();

        AddSubcribeFolder(SubForlderAlarm);
        AddSubcribeFolder(SubForlderSystem);
        foreach (var alarm in Alarms)
        {
            alarm.Node ??= GetNode(alarm.Address);
            await _ConnectorOPC.SubscribeValue(alarm, SubForlderAlarm);
        }
        foreach (var signal in Signals)
        {
            signal.Node ??= GetNode(signal.Address);
            await _ConnectorOPC.SubscribeValue(signal, SubForlderSystem);
        }
    }

    #endregion Ctor

    #region Nodes

    public Node GetNode(string addr) => _ConnectorOPC.GetNode(addr);

    #endregion Nodes

    #region Read / Write

    public string ReadActualValue(Node inNode) => _ConnectorOPC.ReadActualValue(inNode);

    public bool WriteActualValue<T>(T newValue, Node inNode)
    {
        if (Signals.Any(s => s.Node == inNode))
        {
            var sig = Signals.First(s => s.Node == inNode);
            sig.Value = newValue;

            _LogsService.MakeLog(sig);
        }
        if (Alarms.Any(s => s.Node == inNode))
        {
            var sig = Alarms.First(s => s.Node == inNode);
            sig.Value = newValue;
            _LogsService.MakeLog(sig);
        }

        return _ConnectorOPC.WriteActualValue(newValue, inNode);
    }

    public void WriteProfile(Profile ActiveProfile)
    {
        if (ActiveProfile == null) return;

        _ConnectorOPC.WriteProfile(ActiveProfile);
        IsDataReady = true;
        _LogsService.MakeLog(_IsDataReady);
    }

    #endregion Read / Write

    #region Subscribes

    public async Task AddSubcribeFolder(string SubscriptionName) => await _ConnectorOPC.AddSubcribeFolder(SubscriptionName);

    public async Task SubscribeValue<T>(T data, string SubscriptionName) where T : NodeOPC
    {
        _AutoResetEvent.WaitOne();
        if (SubscriptionName == SubForlderAlarm && !Alarms.Any(a => a.Name == data?.Name))
            Alarms.Add(data as Alarm);
        if (SubscriptionName == SubForlderSystem && !Signals.Any(a => a.Name == data?.Name))
            Signals.Add(data as Signal);
        _AutoResetEvent.Set();
        await _ConnectorOPC.SubscribeValue(data, SubscriptionName);
    }

    public void Unsubscribe(string name) => _ConnectorOPC.Unsubscribe(name);

    #region UpdateValue

    /// <summary>
    /// Handler subscribed variables new value notification.
    /// </summary>
    /// <param name="Name">Name of subscribed value</param>
    /// <param name="newValue">New value in object</param>
    /// <param name="RepositoryName">Repository name</param>
    public async Task UpdateValue(string Name, object newValue, string RepositoryName)
    {
        if (newValue == null) return;
        if (newValue.GetType() == typeof(short)) newValue = (int)(short)newValue;
        //_AutoResetEvent.WaitOne();

        switch (RepositoryName)
        {
            case SubForlderAlarm:

                Alarm? alarm = Alarms.First(alarm => alarm?.Name == Name);
                alarm.Value = newValue;
                alarm.TimeStamp = DateTime.Now;
                switch (alarm.Value)
                {
                    case int intVal:
                        if (alarm.Inverted ^ intVal >= 0)
                            await _AlarmLogService.MakeAlarmLog(alarm.Name);
                        else
                            await _AlarmLogService.FinishAlarmLog(alarm.Name);
                        break;

                    case bool boolVal:
                        if (alarm.Inverted ^ boolVal)
                            await _AlarmLogService.MakeAlarmLog(alarm.Name);
                        else
                            await _AlarmLogService.FinishAlarmLog(alarm.Name);
                        break;
                }

                _LogsService.MakeLog(alarm);
                break;

            case SubForlderSystem:

                Signal? signal = Signals.First(signal => signal.Name == Name);
                signal.Value = newValue;

                _LogsService.MakeLog(signal);

                if (Name == "DatenAnforderung") _IsDataRequest.Value = newValue;

                if (((bool)_IsAutoModeRead.Value) && ((bool)_IsDataRequest.Value) && (_ManagerProfiles.ActiveProfile is not null))
                    WriteProfile(_ManagerProfiles.ActiveProfile);

                break;
        }

        //_AutoResetEvent.Set();
    }

    #endregion UpdateValue

    #endregion Subscribes

    #region Connection

    public bool ConnectionStatus { get => _ConnectorOPC.ConnectionStatus; }

    public async Task Connect()
    {
        if (ConnectionStatus) return;
        _ConnectorOPC.Connect();
    }

    public void Disconnect()
    {
        if (!ConnectionStatus) return;
        _ConnectorOPC.Disconnect();
    }

    #endregion Connection

    #region OPC handler

    /// <summary>
    /// Reset all not finished alarm logs. Must be complete at the begin and at the end of program.
    /// </summary>
    /// <returns>A Task.</returns>
    public void Reconnect() => _ConnectorOPC.Reconnect();

    /// <summary>
    /// Alarms logging.
    /// </summary>
    /// <param name="Alarm">The alarm</param>
    private async Task AlarmLog(Alarm Alarm)
    {
    }

    /// <summary>
    /// Handler of signals.
    /// </summary>
    /// <param name="Name">Signal name</param>
    /// <param name="Value">Signal value</param>

    #endregion OPC handler
}