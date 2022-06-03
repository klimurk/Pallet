using Pallet.Models.Interfaces.Base;

namespace Pallet.Models.Interfaces;

public interface IAlarmOPC : INodeOPC
{
    public IAlarm Info { get; set; }
}