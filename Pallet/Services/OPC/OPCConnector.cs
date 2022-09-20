using Opc.Ua;
using Opc.Ua.Client;
using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.ProfileData.Products;
using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Extensions;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.OPC.Interfaces;
using Pallet.Services.UserDialog.Interfaces;
using Siemens.UAClientHelper;
using System.Security.Cryptography.X509Certificates;

namespace Pallet.Services.OPC;

internal class OPCConnector : IOPC
{
    #region Services

    private readonly IUserDialogService? _UserDialogService;
    private readonly IManagerLanguage _ManagerLanguage;
    private readonly ILogService _LogsService;
    private readonly IDbRepository<SystemEvent> _SystemEvents;

    #endregion Services

    #region Fields

    public Session Session;
    private int _RestartConnection;

    protected EndpointDescription SelectedEndpoint;

    private readonly Dictionary<string, Subscription> _Subscription = new();
    private MonitoredItem _MonitoredItem;
    private readonly AutoResetEvent _AutoResetEvent = new(true);
    private readonly OPCProxy _Proxy;

    private static readonly UAClientHelperAPI __myClientHelperAPI = new();

    //private static readonly string __DataOPCAddr = "opc.tcp://192.168.0.1"; // 192.168.0.10

    private const string __DataOPCAddr = "opc.tcp://Klymov-PC.benthor-mb.cz:53530/OPCUA/SimulationServer";
    private readonly string _OPCAddress;

    #endregion Fields

    public OPCConnector(IDbRepository<SystemEvent> SystemEvents, ILogService LogsService, IUserDialogService UserDialogService, OPCProxy Proxy, string address = "")
    {
        "OPCConnector init --------------".CheckStage();
        _LogsService = LogsService;
        _SystemEvents = SystemEvents;
        _OPCAddress = string.IsNullOrEmpty(address) ? __DataOPCAddr : address;
        _UserDialogService = UserDialogService;
        //_ManagerLanguage = App.Host.Services.GetService(typeof(IManagerLanguage)) as IManagerLanguage;
        _Proxy = Proxy;
        "OPCConnector complete --------------".CheckStage();
    }

    #region Connection

    public async Task Connect()
    {
        if (Session != null)
        {
            Reconnect();
            return;
        }
        try
        {
            SelectedEndpoint = __myClientHelperAPI.GetEndpoints(__myClientHelperAPI.FindServers(_OPCAddress)[0].DiscoveryUrls[0]).First(s => s.SecurityMode == MessageSecurityMode.None);
        }
        catch
        {
            _LogsService.MakeLog(_SystemEvents.Items.First(s => s.Name == "OPCError_ServersNotFinded"));

            _UserDialogService.ShowError(
                _SystemEvents.Items.First(s => s.Name == "OPCError_ServersNotFinded"),
                _SystemEvents.Items.First(s => s.Name == "OPCError_Header"));
        }
        try
        {
            //Register mandatory events (cert and keep alive)
            //if (__myClientHelperAPI.KeepAliveNotification == null)
            __myClientHelperAPI.KeepAliveNotification -= new KeepAliveEventHandler(Notification_KeepAlive);
            __myClientHelperAPI.KeepAliveNotification += new KeepAliveEventHandler(Notification_KeepAlive);
            //if (__myClientHelperAPI.CertificateValidationNotification == null)
            __myClientHelperAPI.CertificateValidationNotification -= new CertificateValidationEventHandler(Notification_ServerCertificate);
            __myClientHelperAPI.CertificateValidationNotification += new CertificateValidationEventHandler(Notification_ServerCertificate);
            //if (__myClientHelperAPI.ItemChangedNotification == null)
            __myClientHelperAPI.ItemChangedNotification -= new MonitoredItemNotificationEventHandler(Notification_MonitoredItem);
            __myClientHelperAPI.ItemChangedNotification += new MonitoredItemNotificationEventHandler(Notification_MonitoredItem);

            //Check for a selected endpoint
            if (SelectedEndpoint == null) return;

            __myClientHelperAPI.Connect(SelectedEndpoint, false).Wait();
            Session = __myClientHelperAPI.Session;
            _LogsService.MakeLog(_SystemEvents.Items.First(s => s.Name == "OPCConnected"));
        }
        catch (Exception)
        {
            _LogsService.MakeLog(_SystemEvents.Items.First(s => s.Name == "OPCError_ConnectionFailed"));
            _UserDialogService.ShowError(
                _SystemEvents.Items.First(s => s.Name == "OPCError_ConnectionFailed"),
                _SystemEvents.Items.First(s => s.Name == "OPCError_Header"));
        }
    }

    public bool ConnectionStatus { get => Session?.KeepAliveStopped == false; }

    public bool? IsAutoMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsStopMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool? IsDataRequest => throw new NotImplementedException();

    public bool? IsJobDone => throw new NotImplementedException();

    public bool? IsDataActual => throw new NotImplementedException();

    public bool? IsHaveFailure => throw new NotImplementedException();

    public bool? IsDataReady { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsJobQuittierung { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsAnforderungJobHalt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsAnforderungJobEnd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsOP1Acknowledge { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsFQuitt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ObservableCollection<Alarm> Alarms { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ObservableCollection<Signal> Signals { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Reconnect()
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
            //if (ex.Message == "BadSessionIdInvalid")
            Connect();
        }
    }

    public void Disconnect() => __myClientHelperAPI.Disconnect();

    #endregion Connection

    #region Nodes

    public Node GetNode(string addr) => __myClientHelperAPI.ReadNode(addr);

    #endregion Nodes

    #region Subscribes

    public async Task SubscribeValue<T>(T data, string SubscriptionName) where T : NodeOPC
    {
        await TaskExtension.WaitWhile(!ConnectionStatus);
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
            SubscribeValue(data, SubscriptionName);
        }
    }

    public void Unsubscribe(string name)
    {
        if (_Subscription.ContainsKey(name)) _Subscription.Remove(name);
    }

    public async Task AddSubcribeFolder(string SubscriptionName)
    {
        if (_Subscription.ContainsKey(SubscriptionName)) return;
        await TaskExtension.WaitWhile(!ConnectionStatus);
        _AutoResetEvent.WaitOne();
        if (_Subscription.TryAdd(SubscriptionName, new Subscription()))
        {
            _Subscription[SubscriptionName] = __myClientHelperAPI.Subscribe(1000);
            _Subscription[SubscriptionName].DisplayName = SubscriptionName;
        }
        _AutoResetEvent.Set();
    }

    #endregion Subscribes

    #region Read / Write

    public string ReadActualValue(Node Node)
    {
        return Node is null
            ? throw new ArgumentNullException("Try read OPC null data")
            : ReadValue(Node.NodeId.Identifier.ToString(), Node?.NodeId.NamespaceIndex.ToString());
    }

    public bool WriteActualValue<T>(T newValue, Node inNode)
    {
        if (Session == null) throw new NullReferenceException("Bad OPC Connection");
        //string ident = inNode.NodeId.Identifier.ToString();

        //if (inNode.NodeId.Identifier.ToString().Contains("[")) inNode. = inNode.NodeId.Identifier.ToString().Remove(0, 2);
        //List<string> DBData = ident.Split('\"').ToList();
        //if (string.IsNullOrEmpty(DBData[DBData.Count - 1])) DBData.RemoveAt(DBData.Count - 1);
        try
        {
            __myClientHelperAPI.WriteValues(
                new List<string> { newValue.ToString() },
                new List<string> { inNode.NodeId.ToString() });
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void WriteProfile(Profile ActiveProfile)
    {
        List<Nail> NailList = ActiveProfile.ProfileProducts.First().Product.Nails.ToList();
        // TODO как загружать разные типы гвоздей?
        List<int> nails = new();
        for (int i = 0; i < NailList.Count; i++)
        {
            nails.Add((int)NailList[i].PosX * 10);
            nails.Add((int)NailList[i].PosY * 10);
            nails.Add((int)NailList[i].MoveNextType);
            //nails.Add(NailList[i].PosZ * 10);
            //nails.Add(NailList[i].NailType);
            //nails.Add(NailList[i].NailID);
            //nails.Add(NailList[i].NailGRP);
            //nails.Add(NailList[i].Angle1);
            //nails.Add(NailList[i].Angle2);
        }
        WriteProfile(
            nails,
            Profile.OPCData.Nails.DBName,
            Profile.OPCData.Nails.DBVar,
            new List<string>()
            {
                Profile.OPCData.Nails.Fields.CoorX,
                Profile.OPCData.Nails.Fields.CoorY,
                Profile.OPCData.Nails.Fields.Active
                //NailsPositions.OPCDataInfo.Fields.CoorZ,
                //NailsPositions.OPCDataInfo.Fields.NailType,
                //NailsPositions.OPCDataInfo.Fields.NailID,
                //NailsPositions.OPCDataInfo.Fields.NailGRP,
                //NailsPositions.OPCDataInfo.Fields.Angle1,
                //NailsPositions.OPCDataInfo.Fields.Angle2
            },
            Profile.OPCData.Nails.DBNamespace
            );
    }

    private bool WriteProfile(List<int> newValues, string DBname, string DBvar, List<string> DBPostVar, int namespaceIndex)
    {
        List<string> values = new();
        List<string> address = new();
        if (newValues.Count % DBPostVar.Count != 0) throw new ArgumentOutOfRangeException($"Count of newValues in {nameof(newValues)} not fit count of {DBPostVar} coordinates");

        for (int y = 0; y < newValues.Count / DBPostVar.Count; y++)
        {
            for (int i = 0; i < DBPostVar.Count; i++)
            {
                values.Add(newValues[(y * DBPostVar.Count) + i].ToString());
                address.Add("ns=" + namespaceIndex + ";s=\"" + DBname + "\".\"" + DBvar + "\"[" + y + "].\"" + DBPostVar[i] + "\"");
            }
        }

        _AutoResetEvent.WaitOne();
        try
        {
            __myClientHelperAPI.WriteValues(values, address);
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

    #region internal

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
        }).Start();
    }

    private void Notification_KeepAlive(Session sender, KeepAliveEventArgs e)
    {
        new Thread(() =>
        {
            try
            {
                _RestartConnection = 0;
                if (!ReferenceEquals(sender, Session)) return;
                if (!ServiceResult.IsGood(e.Status)) Session.Reconnect();
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
                Reconnect();
            }
        }).Start();
    }

    private void Notification_MonitoredItem(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e) => new Thread(() => UpdateSubValue(monitoredItem, e)).Start();

    #endregion Notifications

    private async Task UpdateSubValue(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
    {
        if (e.NotificationValue is not MonitoredItemNotification notification) return;

        await _Proxy.UpdateValue(
            monitoredItem.DisplayName,
            notification.Value.Value,
            monitoredItem.Subscription.DisplayName);
    }

    public Session GetSession() => Session;

    private string ReadValue(string identifier, string namespaceIndex)
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

            _UserDialogService.ShowError(
                _SystemEvents.Items.First(s => s.Name == "OPCError_ReadFailed"),
                _SystemEvents.Items.First(s => s.Name == "OPCError_Header"));
            return default;
        }
    }

    #endregion internal

    public async Task<List<Node>> ReadNodesAllAsync(string[] namespaceIndexes, string[] dbName = null, string[] dbVar = null)
    {
        await TaskExtension.WaitWhile(!ConnectionStatus);
        _AutoResetEvent.WaitOne();
        dbName ??= new string[] { "" };
        dbVar ??= new string[] { "" };
        List<Node> retList = ReadNodesCycle(__myClientHelperAPI.BrowseRoot(), namespaceIndexes, dbName, dbVar);
        _AutoResetEvent.Set();
        return retList;
    }

    private List<Node> ReadNodesCycle(ReferenceDescriptionCollection myReference, string[] namespaceIndexes, string[] dbName = null, string[] dbVar = null)
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
                retList.AddRange(ReadNodesCycle(__myClientHelperAPI.BrowseNode(refDesc), dbName, dbVar));
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

    public Task InitializeOPC()
    {
        throw new NotImplementedException();
    }
}