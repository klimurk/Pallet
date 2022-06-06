using Opc.Ua;
using Pallet.Database.Entities.Change.Profiles;
using Pallet.Entities.Models;
using Pallet.Models;
using Pallet.Models.Interfaces.Base;

namespace Pallet.Services.OPC.Interfaces;

public interface IOPC
{
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
    Task SubscribeValue<T>(T data, string SubscriptionName) where T : INodeOpc;

    /// <summary>
    /// Unsubscribe the value.
    /// </summary>
    /// <param name="name">The name.</param>
    void Unsubscribe(string name);

    /// <summary>
    /// Connects the.
    /// </summary>
    void Connect();

    /// <summary>
    /// Disconnects the.
    /// </summary>
    void Disconnect();

    void WriteProfile(Profile ActiveProfile);

    public Task AddSubcribeFolder(string SubscriptionName);

    public void Reconnect();

    public ObservableCollection<AlarmOpc> Alarms { get; set; }

    public ObservableCollection<SignalOPC> Signals { get; set; }
}