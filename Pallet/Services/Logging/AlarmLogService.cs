using Pallet.Database.Entities.OPC;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Logging.Interfaces;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Logging;

/// <summary>
/// The Alarm logging service create and finish log.
/// </summary>
internal class AlarmLogService : IAlarmLogService, IDisposable
{
    private readonly IDbRepository<AlarmLog> _Logs;
    private readonly IDbRepository<Alarm> _Alarms;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlarmLogService"/> class.
    /// </summary>
    /// <param name="Logs">The logs.</param>
    /// <param name="Alarms">The alarms.</param>
    public AlarmLogService(IDbRepository<AlarmLog> Logs, IDbRepository<Alarm> Alarms, IManagerLanguage ManagerLanguage)
    {
        _Logs = Logs;
        _Alarms = Alarms;

        "AlarmLogService init --------------".CheckStage();
        AlarmLogs = new()
        {
            _Logs.Items.ToList()
        };
        ResetAlarmSignals();
        "AlarmLogService complete --------------".CheckStage();
    }

    public ObservableCollection<AlarmLog> AlarmLogs { get; }

    /// <summary>
    /// Makes the alarm log.
    /// </summary>
    /// <param name="AlarmName">The alarm name.</param>
    /// <returns>A Task.</returns>
    public async Task<AlarmLog> MakeAlarmLog(string AlarmName)
    {
        var Alarm = await _Alarms.Items.FirstAsync(a => a.Name == AlarmName);

        AlarmLog alarmLog = new(Alarm)
        {
            TimestampStart = DateTime.Now,
        };
        AlarmLogs.Add(alarmLog);
        await _Logs.AddAsync(alarmLog);
        return alarmLog;
    }

    /// <summary>
    /// Finishes the alarm log.
    /// </summary>
    /// <param name="AlarmName">The alarm name.</param>
    /// <returns>A Task.</returns>
    public async Task FinishAlarmLog(string AlarmName)
    {
        foreach (var alarm in _Logs.Items.Where(a => a.Name == AlarmName && !a.Gone).ToArray())
        {
            alarm.TimestampEnd = DateTime.Now;
            alarm.Gone = true;
            await _Logs.UpdateAsync(alarm);
        }
        foreach (var alarm in AlarmLogs.Where(a => a.Name == AlarmName && !a.Gone).ToArray())
        {
            alarm.TimestampEnd = DateTime.Now;
            alarm.Gone = true;
        }
    }

    /// <summary>
    /// Resetting all active alarms (on start and finish program).
    /// </summary>
    private void ResetAlarmSignals()
    {
        foreach (var alarm in _Logs.Items.Where(a => !a.Gone).ToArray())
        {
            alarm.TimestampEnd = DateTime.Now;
            alarm.Gone = true;
            _Logs.Update(alarm);
        }
    }

    /// <summary>
    /// Disposing.
    /// </summary>
    public void Dispose()
    {
        ResetAlarmSignals();
    }
}