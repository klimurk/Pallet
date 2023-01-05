using CodingSeb.Localization;
using Microsoft.Extensions.Configuration;
using Opc.Ua;
using Opc.Ua.Client;
using Pallet.Extensions;
using Pallet.InternalDatabase.Context;
using Pallet.InternalDatabase.Entities.Base;
using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Siemens.UAClientHelper;
using System.Security.Cryptography.X509Certificates;

namespace Pallet.Services.OPC;

internal class OPCService : IOPC
{
    #region Services

    private readonly ILogService _LogsService;

    private readonly DbSet<SystemEvent> _SystemEvents;
    private InternalDbContext internalDbContext;

    #endregion Services

    #region Fields

    #region Static OPC

    public const string SubForlderAlarm = "Alarm";
    public const string SubForlderSystem = "System";

    #endregion Static OPC

    private readonly AutoResetEvent _AutoResetEvent = new(true);
    public ObservableCollection<Alarm> Alarms { get; set; }

    private readonly IUserDialogService _UserDialogService;

    public ObservableCollection<Signal> Signals { get; set; }

    public Session Session;

    protected EndpointDescription SelectedEndpoint;

    private readonly Dictionary<string, Subscription> _Subscription = new();
    private MonitoredItem _MonitoredItem;

    private static readonly UAClientHelperAPI __myClientHelperAPI = new();

    //private static readonly string __DataOPCAddr = "opc.tcp://192.168.0.1"; // 192.168.0.10

    private const string __DataOPCAddr = "opc.tcp://192.168.0.10:4840";
    private readonly string _OPCAddress;

    #endregion Fields

    #region Ctor

    public OPCService(
        InternalDbContext internalDbContext,
        ILogService LogsService,
        IUserDialogService UserDialogService,
        IConfiguration Configuration
    )
    {
        this.internalDbContext = internalDbContext;
        _SystemEvents = internalDbContext.SystemEvents;

        _UserDialogService = UserDialogService;
        _LogsService = LogsService;

        var address = Configuration[$"OPC:ConnectionStrings:{Configuration["OPC:Type"]}"]; 
        _OPCAddress = string.IsNullOrEmpty(address) ? __DataOPCAddr : address;
        Signals = new()
        {
            internalDbContext.Signals
        };
        Alarms = new()
        {
            internalDbContext.Alarms
        };

        //InitializeOPC().ConfigureAwait(true);
    }

    public async Task InitializeOPC()
    {
        await Connect();
        if (Session is null) return;

        await AddSubcribeFolder(SubForlderAlarm);
        await AddSubcribeFolder(SubForlderSystem);

        foreach (var alarm in Alarms)
        {
            alarm.Node ??= await GetNode(alarm.Address);
            if (alarm.Node != null) await _SubscribeValue(alarm, SubForlderAlarm);
        }
        foreach (var signal in Signals)
        {
            signal.Node ??= await GetNode(signal.Address);
            if (signal.Node != null) await _SubscribeValue(signal, SubForlderSystem);
        }
    }

    #endregion Ctor

    #region Read / Write

    public async Task<bool> WriteValue<T>(T newValue, Node inNode)
    {
        if (Session == null) throw new NullReferenceException("Bad OPC Connection");
        if (Signals.Any(s => s.Node == inNode))
        {
            var sig = Signals.First(s => s.Node == inNode);
            sig.Value = newValue;

            _LogsService.Post(sig).ConfigureAwait(false);
        }
        if (Alarms.Any(s => s.Node == inNode))
        {
            var sig = Alarms.First(s => s.Node == inNode);
            sig.Value = newValue;
            _LogsService.Post(sig).ConfigureAwait(false);
        }

        return _WriteActualValue(newValue, inNode);
    }

    private bool _WriteActualValue<T>(T newValue, Node inNode)
    {
        if (Session == null) throw new NullReferenceException("Bad OPC Connection");

        try
        {
            __myClientHelperAPI.WriteValues(
                new List<string> { newValue.ToString() },
                new List<string> { inNode.NodeId.ToString() });
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async Task<string> ReadValue(string identifier, string namespaceIndex)
    {
        if (Session == null) throw new NullReferenceException("Bad OPC Connection");
        try
        {
            _AutoResetEvent.WaitOne();
            string retval = __myClientHelperAPI.ReadValues(new List<string> { "ns=" + namespaceIndex + identifier })[0];
            _AutoResetEvent.Set();
            return retval;
        }
        catch
        {
            _AutoResetEvent.Set();
            return default;
        }
    }

    public async Task<string> ReadValue(Node Node)
    {
        return Node is null
            ? throw new ArgumentNullException("Try read OPC null data")
            : await ReadValue(Node.NodeId.Identifier.ToString(), Node?.NodeId.NamespaceIndex.ToString());
    }

    public async Task<List<Node>> ReadNodesAllAsync(string[] namespaceIndexes, string[] dbName = null, string[] dbVar = null)
    {
        await TaskExtension.WaitWhile(!IsConnected);
        _AutoResetEvent.WaitOne();
        dbName ??= new string[] { "" };
        dbVar ??= new string[] { "" };
        List<Node> retList = await ReadNodesCycle(__myClientHelperAPI.BrowseRoot(), namespaceIndexes, dbName, dbVar);
        _AutoResetEvent.Set();
        return retList;
    }

    public async Task<Node> GetNode(string addr) => __myClientHelperAPI.ReadNode(addr);

    private async Task<List<Node>> ReadNodesCycle(ReferenceDescriptionCollection myReference, string[] namespaceIndexes, string[] dbName = null, string[] dbVar = null)
    {
        List<Node> retList = new();
        dbName ??= new string[] { "" };
        dbVar ??= new string[] { "" };
        foreach (ReferenceDescription refDesc in myReference)
        {
            if (!namespaceIndexes.Contains(refDesc.NodeId.NamespaceIndex.ToString())) continue;

            Node node = __myClientHelperAPI.ReadNode(refDesc.NodeId.ToString());

            if (node.NodeClass != NodeClass.Variable)
            {
                retList.AddRange(await ReadNodesCycle(__myClientHelperAPI.BrowseNode(refDesc), dbName, dbVar));
            }
            else
            {
                // ========= filter ============
                foreach (string db in dbName)
                {
                    foreach (string var in dbVar)
                    {
                        if (node.NodeId.ToString().Contains(db) && node.NodeId.ToString().Contains(var) && !retList.Contains(node))
                        {
                            retList.Add(node);
                            break;
                        }
                    }
                }
            }
        }
        return retList;
    }

    public async Task<bool> WriteList<T>(IList<T> newValues, string DBname, string DBvar, IList<string> DBPostVar, int namespaceIndex)
    {
        if (Session == null) throw new NullReferenceException("Bad OPC Connection");
        if (newValues == null) throw new ArgumentNullException("List is empty");
        if (newValues.Count % DBPostVar.Count != 0) throw new ArgumentOutOfRangeException($"Count of newValues in {nameof(newValues)} not fit count of {DBPostVar} coordinates");

        Dictionary<string, string> valuesD = new();

        for (int y = 0; y < newValues.Count / DBPostVar.Count; y++)
        {
            for (int i = 0; i < DBPostVar.Count; i++)
            {
                valuesD.Add(
                    $"ns={namespaceIndex};s=\"{DBname}\".\"{DBvar}\"[{y}].\"{DBPostVar[i]}\"",
                    newValues[(y * DBPostVar.Count) + i].ToString()
                );
            }
        }

        _AutoResetEvent.WaitOne();
        try
        {
            __myClientHelperAPI.WriteValues(valuesD.Values.ToList(), valuesD.Keys.ToList());
        }
        catch (Exception)
        {
            _AutoResetEvent.Set();
            return false;
        }
        _AutoResetEvent.Set();
        return true;
    }

    #endregion Read / Write

    #region Subscribes

    public async Task SubscribeValue<T>(T data, string SubscriptionName) where T : NodeOPC
    {
        _AutoResetEvent.WaitOne();
        if (SubscriptionName == SubForlderAlarm && !Alarms.Any(a => a.Name == data?.Name))
            Alarms.Add(data as Alarm);
        if (SubscriptionName == SubForlderSystem && !Signals.Any(a => a.Name == data?.Name))
            Signals.Add(data as Signal);
        _AutoResetEvent.Set();
        await _SubscribeValue(data, SubscriptionName);
    }

    public async Task _SubscribeValue<T>(T data, string SubscriptionName) where T : NodeOPC
    {
        await TaskExtension.WaitWhile(!IsConnected);
        string monitoredItemName = data.Node.DisplayName.ToString();

        if (_MonitoredItem != null)        // check if monitored item already exist
        {
            foreach (MonitoredItem mi in _MonitoredItem.Subscription.MonitoredItems)
            {
                if (mi.DisplayName == monitoredItemName) return;
            }
        }

        try
        {
            _AutoResetEvent.WaitOne();
            _MonitoredItem = __myClientHelperAPI.AddMonitoredItem(_Subscription[SubscriptionName], data.Node.NodeId.ToString(), monitoredItemName, 1);
            _AutoResetEvent.Set();
        }
        catch (Exception ex)
        {
            _AutoResetEvent.Set();
            if (ex.Message == "BadSessionIdInvalid") Reconnect();
            _SubscribeValue(data, SubscriptionName);
        }
    }

    public async Task Unsubscribe(string name)
    {
        if (_Subscription.ContainsKey(name)) _Subscription.Remove(name);
    }

    public async Task AddSubcribeFolder(string SubscriptionName)
    {
        if (_Subscription.ContainsKey(SubscriptionName)) return;
        await TaskExtension.WaitWhile(!IsConnected);
        _AutoResetEvent.WaitOne();
        if (_Subscription.TryAdd(SubscriptionName, new Subscription()))
        {
            _Subscription[SubscriptionName] = __myClientHelperAPI.Subscribe(1000);
            _Subscription[SubscriptionName].DisplayName = SubscriptionName;
        }
        _AutoResetEvent.Set();
    }

    #region UpdateValue

    /// <summary>
    /// Handler subscribed variables new value notification.
    /// </summary>
    /// <param name="Name">Name of subscribed value</param>
    /// <param name="newValue">New value in object</param>
    /// <param name="RepositoryName">Repository name</param>
    public async Task UpdateValue(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
    {
        if (e.NotificationValue is not MonitoredItemNotification notification) return;
        string Name = monitoredItem.DisplayName;
        object newValue = notification.Value.Value;
        string RepositoryName = monitoredItem.Subscription.DisplayName;

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
                            await _LogsService.Post(alarm);
                        else
                            await _LogsService.Put(alarm, DateTime.Now);
                        break;

                    case bool boolVal:
                        if (alarm.Inverted ^ boolVal)
                            await _LogsService.Post(alarm);
                        else
                            await _LogsService.Put(alarm, DateTime.Now);
                        break;
                }
                break;

            case SubForlderSystem:

                Signal? signal = Signals.First(signal => signal.Name == Name);
                signal.Value = newValue;

                _LogsService.Post(signal);

                if (Name == "DatenAnforderung") Signals.First(s => s.Name == "DatenAnforderung").Value = newValue;

                //if (((bool)_IsAutoModeRead.Value) && ((bool)_IsDataRequest.Value) && (_ManagerProfiles.ActiveProfile is not null))
                //    WriteProfile(_ManagerProfiles.ActiveProfile);

                break;
        }
        OnDataChanged();

        //_AutoResetEvent.Set();
    }

    #endregion UpdateValue

    #endregion Subscribes

    #region Connection

    public async Task Connect()
    {
        if (IsConnected) return;
        if (Session != null)
        {
            Reconnect();
            return;
        }
        try
        {
            var servers = __myClientHelperAPI.FindServers(_OPCAddress);
            if (servers is null)
            {
                _UserDialogService.ShowSnackbarError(Loc.Tr("OPC.Errors.ServerNotFinded", "Not localized"));
                _LogsService.Post(_SystemEvents.First(s => s.Name == "OPCError_ServersNotFinded")).ConfigureAwait(false);
                return;
            }
            var firstServerUrl = servers[0].DiscoveryUrls[0];
            var endpoints = __myClientHelperAPI.GetEndpoints(firstServerUrl);
            if (endpoints is null)
            {
                _UserDialogService.ShowSnackbarError(Loc.Tr("OPC.Errors.EndpointsNotFinded", "Not localized"));
                _LogsService.Post(_SystemEvents.First(s => s.Name == "OPCError_ServersNotFinded")).ConfigureAwait(false);
                return;
            }

            SelectedEndpoint = endpoints.First(s => s.SecurityMode == MessageSecurityMode.None);
        }
        catch
        {
        }
        try
        {
            //Register mandatory events (cert and keep alive)
            __myClientHelperAPI.KeepAliveNotification -= new KeepAliveEventHandler(Notification_KeepAlive);
            __myClientHelperAPI.KeepAliveNotification += new KeepAliveEventHandler(Notification_KeepAlive);

            __myClientHelperAPI.CertificateValidationNotification -= new CertificateValidationEventHandler(Notification_ServerCertificate);
            __myClientHelperAPI.CertificateValidationNotification += new CertificateValidationEventHandler(Notification_ServerCertificate);

            __myClientHelperAPI.ItemChangedNotification -= new MonitoredItemNotificationEventHandler(Notification_MonitoredItem);
            __myClientHelperAPI.ItemChangedNotification += new MonitoredItemNotificationEventHandler(Notification_MonitoredItem);

            //Check for a selected endpoint
            if (SelectedEndpoint == null) return;

            __myClientHelperAPI.Connect(SelectedEndpoint, false).Wait();
            Session = __myClientHelperAPI.Session;
            _LogsService.Post(_SystemEvents.First(s => s.Name == "OPCConnected")).ConfigureAwait(false);
        }
        catch (Exception)
        {
            _LogsService.Post(_SystemEvents.First(s => s.Name == "OPCError_ConnectionFailed")).ConfigureAwait(false);
        }
    }

    public async Task Disconnect()
    {
        if (!IsConnected) return;
        __myClientHelperAPI.Disconnect();
    }

    public bool IsConnected { get => Session?.KeepAliveStopped == false; }

    public async Task Reconnect()
    {
        try
        {
            if (Session != null)
                Session.Reconnect();
            else
                Connect();
        }
        catch (Exception ex)
        {
            Session = null;
            Connect();
        }
    }

    #endregion Connection

    #region Notifications

    private void Notification_ServerCertificate(CertificateValidator cert, CertificateValidationEventArgs e)
    {
        new Thread(() =>
        {
            X509CertificateCollection certCol;
            using (X509Store store = new(StoreName.Root, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                certCol = store.Certificates.Find(X509FindType.FindByThumbprint, e.Certificate.Thumbprint, true);
            }
            if (certCol.Capacity > 0) e.Accept = true;
        })
        { IsBackground = true }.Start();
    }

    private void Notification_KeepAlive(Session sender, KeepAliveEventArgs e)
    {
        new Thread(() =>
        {
            try
            {
                if (!ReferenceEquals(sender, Session)) return;
                if (!ServiceResult.IsGood(e.Status)) Session.Reconnect();
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
                Reconnect();
            }
        })
        { IsBackground = true }.Start();
    }

    private void Notification_MonitoredItem(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e) => new Thread(() => UpdateValue(monitoredItem, e)).Start();

    #endregion Notifications

    public event EventHandler DataChanged;

    private void OnDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);
}