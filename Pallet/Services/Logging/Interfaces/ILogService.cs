using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;

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
    Task<Log> MakeLog(SystemEvent ev);

    Task<Log> MakeLog(Signal sig);

    Task<Log> MakeLog(Alarm al);
}