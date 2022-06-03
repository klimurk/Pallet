using Pallet.Database.Entities.OPC;

namespace Pallet.Services.Logging.Interfaces;

public interface IAlarmLogService
{
    ObservableCollection<AlarmLog> AlarmLogs { get; }

    Task FinishAlarmLog(string AlarmName);

    Task<AlarmLog> MakeAlarmLog(string AlarmName);
}