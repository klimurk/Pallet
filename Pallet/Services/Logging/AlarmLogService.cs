using Pallet.Database.Entities.OPC;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Logging.Interfaces;

namespace Pallet.Services.Logging;

internal class AlarmLogService : IAlarmLogService, IDisposable
{
    private readonly IDbRepository<AlarmLog> _Logs;
    private readonly IDbRepository<Alarm> _Alarms;

    public AlarmLogService(
        IDbRepository<AlarmLog> Logs, IDbRepository<Alarm> Alarms
        )
    {
        _Logs = Logs;
        _Alarms = Alarms;
        AlarmLogs = new();
        AlarmLogs.Add(_Logs.Items.ToList());
        ResetAlarmSignals();
    }

    public ObservableCollection<AlarmLog> AlarmLogs { get; }

    public async Task<AlarmLog?> MakeAlarmLog(string AlarmName)
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

    public async Task FinishAlarmLog(string AlarmName)
    {
        foreach (var alarm in await _Logs.Items.Where(a => a.Name == AlarmName && !a.Gone).ToArrayAsync())
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

    private void ResetAlarmSignals()
    {
        foreach (var alarm in _Logs.Items.Where(a => !a.Gone).ToArray())
        {
            alarm.TimestampEnd = DateTime.Now;
            alarm.Gone = true;
            _Logs.Update(alarm);
        }
    }

    public void Dispose()
    {
        ResetAlarmSignals();
    }
}