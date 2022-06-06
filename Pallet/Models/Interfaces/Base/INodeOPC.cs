using Opc.Ua;

namespace Pallet.Models.Interfaces.Base;

/// <summary>
/// Interface for variable for OPC with node and value.
/// </summary>
public interface INodeOpc : INotifyPropertyChanged
{
    /// <summary>
    /// OPC node.
    /// </summary>
    public Node NodeOpc { get; set; }
    /// <summary>
    /// OPC value.
    /// </summary>
    public object Value { get; set; }
}