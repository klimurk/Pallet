using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Logging;

/// <summary>
/// The Alarm logging service create and finish log.
/// </summary>
internal class LogService : ILogService, IDisposable
{
    private readonly IDbRepository<Log> _Logs;
    private readonly IDbRepository<SystemEvent> _SystemEvents;
    private readonly IManagerLanguage _ManagerLanguage;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlarmLogService"/> class.
    /// </summary>
    /// <param name="Logs">The logs.</param>
    /// <param name="SystemEvents">The alarms.</param>
    public LogService(IDbRepository<Log> _Logs, IDbRepository<SystemEvent> SystemEvents, IManagerLanguage ManagerLanguage)
    {
        "LogService init --------------".CheckStage();
        this._Logs = _Logs;
        _SystemEvents = SystemEvents;
        _ManagerLanguage = ManagerLanguage;

        Logs = new()
        {
            _Logs.Items.ToList()
        };
        "LogService complete --------------".CheckStage();
    }

    public ObservableCollection<Log> Logs { get; }

    /// <summary>
    /// Makes the alarm log.
    /// </summary>
    /// <param name="AlarmName">The alarm name.</param>
    /// <returns>A Task.</returns>
    public async Task<Log> MakeLog(SystemEvent ev)
    {
        Log log = new(ev)
        {
        };
        Logs.Add(log);
        await _Logs.AddAsync(log);
        return log;
    }

    public async Task<Log> MakeLog(Signal sig)
    {
        Logs.Add(new(sig, sig.Value));
        await _Logs.AddAsync(new(sig, sig.Value));
        return new(sig, sig.Value);
    }

    public async Task<Log> MakeLog(Alarm al)
    {
        Logs.Add(new(al, al.Value));
        await _Logs.AddAsync(new(al, al.Value));
        return new(al, al.Value);
    }

    /// <summary>
    /// Disposing.
    /// </summary>
    public void Dispose()
    {
        //MakeLog(_Signals.get"ExitApp").Wait();
    }
}