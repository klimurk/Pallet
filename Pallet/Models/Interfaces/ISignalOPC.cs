using Pallet.Models.Interfaces.Base;

namespace Pallet.Models.Interfaces;

public interface ISignalOPC : INodeOPC
{
    public ISignal Info { get; set; }
}