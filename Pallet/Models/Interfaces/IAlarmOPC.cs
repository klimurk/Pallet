using Pallet.Models.Interfaces.Base;

namespace Pallet.Models.Interfaces;

public interface IAlarmOpc : INodeOpc
{
    event PropertyChangedEventHandler PropertyChanged;

    public IAlarm Info { get; set; }
}