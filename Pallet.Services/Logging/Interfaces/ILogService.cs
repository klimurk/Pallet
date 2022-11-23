using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using System.Collections.ObjectModel;
using Pallet.Services.Base;

namespace Pallet.Services.Logging.Interfaces;

public interface ILogService
{
    /// <summary>
    /// Logs collection .
    /// </summary>
    ObservableCollection<Log> Logs { get; }

    /// <summary>
    /// Add log to database.
    /// </summary>
    /// <param name="ev">system event from database</param>
    /// <returns>A Task.</returns>
    Task<Log> Post(SystemEvent ev);

    Task<Log> Post(Signal sig);

    Task<Log> Post(Alarm al);
    /// <summary>
    /// Logs collection .
    /// </summary>
    ObservableCollection<AlarmLog> AlarmLogs { get; }

    /// <summary>
    /// Find and finish the alarm log.
    /// </summary>
    /// <param name="AlarmName">The alarm name.</param>
    /// <returns>A Task.</returns>
    Task Put(Alarm alarm, DateTime time);

}