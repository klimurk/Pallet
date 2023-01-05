using Opc.Ua;
using Pallet.ExternalDatabase.Models;
using Pallet.InternalDatabase.Entities.Base;
using Pallet.InternalDatabase.Entities.OPC;
using System.Collections.ObjectModel;

namespace Pallet.Services.OPC.Interfaces;

public interface IOPC
{
    public Task InitializeOPC();

    public event EventHandler DataChanged;

    /// <summary>
    /// Checks the connection status.
    /// </summary>
    /// <returns>A bool.</returns>
    bool IsConnected { get; }

    /// <summary>
    /// Writes the value.
    /// </summary>
    /// <param name="newValue">The new value.</param>
    /// <param name="inNode">The in node.</param>
    /// <returns>A bool.</returns>
    Task<bool> WriteValue<T>(T newValue, Node inNode);

    /// <summary>
    /// Gets the node.
    /// </summary>
    /// <param name="addr">The addr.</param>
    /// <returns>A Node.</returns>
    Task<Node> GetNode(string addr);

    /// <summary>
    /// Reads the value.
    /// </summary>
    /// <param name="inNode">The in node.</param>
    /// <returns>A string.</returns>
    Task<string> ReadValue(Node inNode);

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
    Task Unsubscribe(string name);

    /// <summary>
    /// Connects the.
    /// </summary>
    Task Connect();

    /// <summary>
    /// Disconnects the.
    /// </summary>
    Task Disconnect();

    public Task<bool> WriteList<T>(IList<T> newValues, string DBname, string DBvar, IList<string> DBPostVar, int namespaceIndex);
    public Task AddSubcribeFolder(string SubscriptionName);

    public Task Reconnect();

    public ObservableCollection<Alarm> Alarms { get; set; }

    public ObservableCollection<Signal> Signals { get; set; }
}