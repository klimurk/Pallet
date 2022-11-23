using Opc.Ua;
using Pallet.BaseDatabase.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.InternalDatabase.Entities.Base;

public abstract class NodeOPC : NamedEntity, IValueNotified
{
    public new int ID { get; set; }

    [NotMapped]
    public Node Node { get; set; }

    [NotMapped]
    public object Value
    {
        get => _Value;
        set
        {
            _Value = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    [NotMapped]
    private object _Value;

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs) => PropertyChanged?.Invoke(this, propertyChangedEventArgs);
}