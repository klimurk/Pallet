using Opc.Ua;

namespace Pallet.Models.Interfaces.Base;

public interface INodeOPC : INotifyPropertyChanged
{
    public Node NodeOPC { get; set; }
    public object Value { get; set; }
}