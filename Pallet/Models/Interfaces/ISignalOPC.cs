using Pallet.Models.Interfaces.Base;

namespace Pallet.Models.Interfaces;
/// <summary>
/// The signal OPC.
/// </summary>
public interface ISignalOPC : INodeOpc
{
    /// <summary>
    /// Database signal info
    /// </summary>
    public ISignal Info { get; set; }
}