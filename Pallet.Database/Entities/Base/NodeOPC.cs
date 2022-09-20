using Opc.Ua;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Base;

public abstract class NodeOPC : NamedEntity
{
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

    private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs) => PropertyChanged?.Invoke(this, propertyChangedEventArgs);
}