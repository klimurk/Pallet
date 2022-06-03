using Opc.Ua;
using Pallet.Models.Interfaces;

namespace Pallet.Models
{
    public class AlarmOPC : IAlarmOPC
    {
        public IAlarm Info { get; set; }
        public Node NodeOPC { get; set; }

        public object Value
        {
            get => _Value;
            set
            {
                _Value = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        private object _Value;
        public DateTime TimeStamp { get; set; }

        public AlarmOPC()
        { }

        public AlarmOPC(ref IAlarmOPC Alarm)
        {
            Info = Alarm.Info;
            NodeOPC = Alarm.NodeOPC;
            Value = Alarm.Value;
        }

        private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs) => PropertyChanged?.Invoke(this, propertyChangedEventArgs);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}