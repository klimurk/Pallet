using Opc.Ua;
using Pallet.Models.Interfaces;

namespace Pallet.Models
{
    /// <summary>
    /// The OPC alarm.
    /// </summary>
    public class AlarmOpc : IAlarmOpc
    {
        /// <summary>
        /// Base Alarm from database.
        /// </summary>
        public IAlarm Info { get; set; }

        /// <summary>
        /// OPC node for alarm.
        /// </summary>
        public Node NodeOpc { get; set; }

        /// <summary>
        /// Value of alarm.
        /// </summary>
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

        /// <summary>
        /// Timestamp of getting from PLC.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmOpc"/> class.
        /// </summary>
        public AlarmOpc()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmOpc"/> class.
        /// </summary>
        /// <param name="Alarm">The alarm.</param>
        public AlarmOpc(ref IAlarmOpc Alarm)
        {
            Info = Alarm.Info;
            NodeOpc = Alarm.NodeOpc;
            Value = Alarm.Value;
        }

        /// <summary>
        /// On the alarm value property changed.
        /// </summary>
        /// <param name="propertyChangedEventArgs">The property changed event args.</param>
        private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs) => PropertyChanged?.Invoke(this, propertyChangedEventArgs);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}