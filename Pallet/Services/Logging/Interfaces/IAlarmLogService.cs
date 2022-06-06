using Pallet.Database.Entities.OPC;

namespace Pallet.Services.Logging.Interfaces;
/// <summary>
/// Alarm log service interface.
/// </summary>
public interface IAlarmLogService
{
    /// <summary>
    /// Logs collection .
    /// </summary>
    ObservableCollection<AlarmLog> AlarmLogs { get; }

    /// <summary>
    /// Find and finish the alarm log.
    /// </summary>
    /// <param name="AlarmName">The alarm name.</param>
    /// <returns>A Task.</returns>
    Task FinishAlarmLog(string AlarmName);

    /// <summary>
    /// Add alarm log to database.
    /// </summary>
    /// <param name="AlarmName">The alarm name.</param>
    /// <returns>A Task.</returns>
    Task<AlarmLog> MakeAlarmLog(string AlarmName);
}