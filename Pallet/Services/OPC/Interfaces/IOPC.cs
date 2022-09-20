using Opc.Ua;
using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.ProfileData.Profiles;

namespace Pallet.Services.OPC.Interfaces;

public interface IOPC
{
    public Task InitializeOPC();

    public bool? IsAutoMode { get; set; }
    public bool? IsStopMode { get; set; }

    public bool? IsDataRequest { get; }
    public bool? IsJobDone { get; }
    public bool? IsDataActual { get; }
    public bool? IsHaveFailure { get; }
    public bool? IsDataReady { get; set; }
    public bool? IsJobQuittierung { get; set; }
    public bool? IsAnforderungJobHalt { get; set; }
    public bool? IsAnforderungJobEnd { get; set; }
    public bool? IsOP1Acknowledge { get; set; }
    public bool? IsFQuitt { get; set; }

    /// <summary>
    /// Checks the connection status.
    /// </summary>
    /// <returns>A bool.</returns>
    bool ConnectionStatus { get; }

    /// <summary>
    /// Writes the value.
    /// </summary>
    /// <param name="newValue">The new value.</param>
    /// <param name="inNode">The in node.</param>
    /// <returns>A bool.</returns>
    bool WriteActualValue<T>(T newValue, Node inNode);

    /// <summary>
    /// Gets the node.
    /// </summary>
    /// <param name="addr">The addr.</param>
    /// <returns>A Node.</returns>
    Node GetNode(string addr);

    /// <summary>
    /// Reads the value.
    /// </summary>
    /// <param name="inNode">The in node.</param>
    /// <returns>A string.</returns>
    string ReadActualValue(Node inNode);

    /// <summary>
    /// Subscribes the value.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="SubscriptionName">The subscription name.</param>
    Task SubscribeValue<T>(T data, string SubscriptionName) where T : NodeOPC;

    /// <summary>
    /// Unsubscribe the value.
    /// </summary>
    /// <param name="name">The name.</param>
    void Unsubscribe(string name);

    /// <summary>
    /// Connects the.
    /// </summary>
    Task Connect();

    /// <summary>
    /// Disconnects the.
    /// </summary>
    void Disconnect();

    void WriteProfile(Profile ActiveProfile);

    public Task AddSubcribeFolder(string SubscriptionName);

    public void Reconnect();

    public ObservableCollection<Alarm> Alarms { get; set; }

    public ObservableCollection<Signal> Signals { get; set; }
}