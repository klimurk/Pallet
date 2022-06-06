using Opc.Ua;
using Pallet.Models.Interfaces;

namespace Pallet.Entities.Models
{
    public class SignalOPC : ISignalOPC
    {
        public ISignal Info { get; set; }
        public Node NodeOpc { get; set; }

        private object _Value;

        public object Value
        {
            get => _Value;
            set
            {
                _Value = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public SignalOPC()
        { }

        public SignalOPC(ISignalOPC signal)
        {
            Info = signal.Info;
            NodeOpc = signal.NodeOpc;
            Value = signal.Value;
        }

        private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs) => PropertyChanged?.Invoke(this, propertyChangedEventArgs);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}