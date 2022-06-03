using Opc.Ua;
using Pallet.Database.Entities.Change.Profiles;
using Pallet.Entities.Models;
using Pallet.Models;
using Pallet.Models.Interfaces;
using Pallet.Models.Interfaces.Base;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;

namespace Pallet.Services.OPC;

internal class OPCProxy : IOPC
{
    #region Services

    private readonly IOPC _ConnectorOPC;
    private readonly IAlarmLogService _AlarmLogService;
    private readonly IManagerProfiles _ManagerProfiles;

    #endregion Services

    #region Fields

    #region Static OPC

    public static readonly string SubForlderAlarm = "Alarm";
    public static readonly string SubForlderSystem = "System";
    public const string SubForlderAlarmC = "Alarm";
    public bool PLCRequestProfileData { get; private set; }

    #endregion Static OPC

    public ObservableCollection<AlarmOPC> Alarms { get; set; }

    public ObservableCollection<SignalOPC> Signals { get; set; }

    private AutoResetEvent _AutoResetEvent = new(true);

    #endregion Fields

    #region Ctor

    public OPCProxy(IAlarmLogService AlarmLogService, IManagerProfiles ManagerProfiles)
    {
        Signals = new();
        Alarms = new();
        _ConnectorOPC = new OPCConnector(this);
        _AlarmLogService = AlarmLogService;
        _ManagerProfiles = ManagerProfiles;
    }

    #endregion Ctor

    #region Nodes

    public Node GetNode(string addr) => _ConnectorOPC.GetNode(addr);

    #endregion Nodes

    #region Read / Write

    public string ReadActualValue(Node inNode) => _ConnectorOPC.ReadActualValue(inNode);

    public bool WriteActualValue<T>(T newValue, Node inNode) => _ConnectorOPC.WriteActualValue(newValue, inNode);

    public void WriteProfile(Profile ActiveProfile)
    {
        if (ActiveProfile == null) return;
        _ConnectorOPC.WriteProfile(ActiveProfile);
        _ConnectorOPC.WriteActualValue(
            true,
            Signals.First(signal => signal.NodeOPC.DisplayName.ToString() == "DatenBereit").NodeOPC);
    }

    #endregion Read / Write

    #region Subscribes

    public async Task AddSubcribeFolder(string SubscriptionName) => await _ConnectorOPC.AddSubcribeFolder(SubscriptionName);

    public async Task SubscribeValue<T>(T data, string SubscriptionName) where T : INodeOPC
    {
        _AutoResetEvent.WaitOne();
        if (SubscriptionName == SubForlderAlarm && data is IAlarmOPC && !Alarms.Any(a => a.Info.Name == (data as AlarmOPC)?.Info.Name))
            Alarms.Add((IAlarmOPC)data as AlarmOPC);
        if (SubscriptionName == SubForlderSystem && data is ISignalOPC && !Signals.Any(a => a.Info.Name == (data as SignalOPC)?.Info.Name))
            Signals.Add((ISignalOPC)data as SignalOPC);
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

        if (RepositoryName == SubForlderAlarm && Alarms.Any(alarm => alarm?.NodeOPC.DisplayName.ToString() == Name))
        {
            var alarm = Alarms.First(alarm => alarm?.NodeOPC.DisplayName.ToString() == Name);
            alarm.Value = newValue;
            alarm.TimeStamp = DateTime.Now;
            await AlarmLog(alarm);
            return;
        }
        if (RepositoryName == SubForlderSystem && Signals.Any(signal => signal.NodeOPC.DisplayName.ToString() == Name))
        {
            var signal = Signals.First(signal => signal.NodeOPC.DisplayName.ToString() == Name);
            signal.Value = newValue;
            SignalHandler(signal);
            return;
        }
    }

    #endregion UpdateValue

    #endregion Subscribes

    #region Connection

    public void Connect()
    {
        if (ConnectionStatus) return;
        _ConnectorOPC.Connect();
    }

    public void Disconnect()
    {
        if (!ConnectionStatus) return;
        _ConnectorOPC.Disconnect();
    }

    public bool ConnectionStatus { get => _ConnectorOPC.ConnectionStatus; }

    #endregion Connection

    #region OPC handler

    /// <summary>
    /// Alarms logging.
    /// </summary>
    /// <param name="Alarm">The alarm</param>
    private async Task AlarmLog(AlarmOPC Alarm)
    {
        switch (Alarm.Value)
        {
            case int intVal:
                if (Alarm.Info.Inverted ^ intVal >= 0)
                    await _AlarmLogService.MakeAlarmLog(Alarm.Info.Name);
                else
                    await _AlarmLogService.FinishAlarmLog(Alarm.Info.Name);
                break;

            case bool boolVal:
                if (Alarm.Info.Inverted ^ boolVal)
                    await _AlarmLogService.MakeAlarmLog(Alarm.Info.Name);
                else
                    await _AlarmLogService.FinishAlarmLog(Alarm.Info.Name);
                break;
        }
    }

    /// <summary>
    /// Handler of signals.
    /// </summary>
    /// <param name="Name">Signal name</param>
    /// <param name="Value">Signal value</param>
    private void SignalHandler(INodeOPC Signal)
    {
        switch (Signal.NodeOPC.DisplayName.ToString())
        {
            case "DatenAnforderung":
                if (_ManagerProfiles.ActiveProfile != null) PLCRequestProfileData = (bool)Signal.Value;
                break;

            case "JobFertig":
                WriteActualValue(Signal.Value, Signal.NodeOPC);
                break;
        }
    }

    /// <summary>
    /// Reset all not finished alarm logs. Must be complete at the begin and at the end of program.
    /// </summary>
    /// <returns>A Task.</returns>
    public void Reconnect() => _ConnectorOPC.Reconnect();

    #endregion OPC handler
}