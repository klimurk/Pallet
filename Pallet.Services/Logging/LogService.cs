using Pallet.Extensions;
using Pallet.ExternalDatabase.Models;
using Pallet.InternalDatabase.Context;
using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.Services.Logging.Interfaces;

namespace BlazorServerPallet.Services.Logging;

/// <summary>
/// The Alarm logging service create and finish log.
/// </summary>
internal class LogService : ILogService, IDisposable
{
    private readonly InternalDbContext _dbContext;
    private readonly DbSet<PalletLog> _dbSetPalletLogs;
    private readonly DbSet<Signal> _dbSetSignals;
    private readonly DbSet<Log> _dbSetLogs;
    private readonly DbSet<SystemEvent> _dbSetSysEvents;
    private readonly DbSet<AlarmLog> _dbSetAlarmLogs;
    private readonly DbSet<Alarm> _dbSetAlarms;

    public ObservableCollection<Log> Logs { get; }
    public ObservableCollection<AlarmLog> AlarmLogs { get; }
    public ObservableCollection<PalletLog> PalletLogs { get; }

    public LogService(
        InternalDbContext internalDbContext
        )
    {
        _dbContext = internalDbContext;
        _dbSetSignals = _dbContext.Signals;
        _dbSetLogs = _dbContext.Logs;
        _dbSetSysEvents = _dbContext.SystemEvents;
        _dbSetAlarmLogs = _dbContext.AlarmLogs;
        _dbSetAlarms = _dbContext.Alarms;
        //_dbSetPalletLogs = _dbContext.PalletLogs;

        AlarmLogs = new()
        {
            _dbSetAlarmLogs
        };

        Logs = new()
        {
            _dbSetLogs
        };
        //PalletLogs = new()
        //{
        //    _dbSetPalletLogs
        //};
        //PalletLogs.CollectionChanged += PalletLogs_CollectionChanged;
        Logs.CollectionChanged += Logs_CollectionChanged;
        AlarmLogs.CollectionChanged += AlarmLogs_CollectionChanged;
        ResetAlarmSignals();
    }

    private void PalletLogs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (PalletLog item in e.NewItems)
                _dbSetPalletLogs.Remove(item);
            _dbContext.SaveChanges();
        }

        if (e.NewItems is not null)
        {
            foreach (PalletLog item in e.NewItems)
                _dbSetPalletLogs.AddAsync(item);
            _dbContext.SaveChanges();
        }
    }

    #region MyRegion

    private void AlarmLogs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (AlarmLog item in e.OldItems)
            {
                item.PropertyChanged -= AlarmLogPropertyChanged;
                _dbSetAlarmLogs.Remove(item);
            }
            _dbContext.SaveChanges();
        }

        if (e.NewItems is not null)
        {
            foreach (AlarmLog item in e.NewItems)
            {
                _dbSetAlarmLogs.Add(item);
                item.PropertyChanged += AlarmLogPropertyChanged;
            }
            _dbContext.SaveChanges();
        }
    }

    private void AlarmLogPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var alm = sender as AlarmLog;
        _dbSetAlarmLogs.Update(alm);
        _dbContext.SaveChanges();
    }

    private void Logs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (Log item in e.NewItems)
                _dbSetLogs.Remove(item);
            _dbContext.SaveChanges();
        }

        if (e.NewItems is not null)
        {
            foreach (Log item in e.NewItems)
                _dbSetLogs.AddAsync(item);
            _dbContext.SaveChanges();
        }
    }

    #endregion MyRegion

    public async Task<Log> Post(SystemEvent ev)
    {
        Log log = new(ev);
        Logs.Add(log);
        return log;
    }

    public async Task<Log> Post(Signal sig)
    {
        Log log = new(sig, sig.Value);
        Logs.Add(log);
        return log;
    }

    public async Task<PalletLog> Post(PackageItem packageItem, RobotTaskItem RobotTaskItem, Verpackung CrateCharacteristics, Auftrag Contract, Firma Firm)
    {
        PalletLog log = new()
        {
            //Name =,
            //Worker =,
            //Timestamp =,
            //Layer =,
            //CustomerName =,
            //ContractNum = Contract.,
            //PackageNum = packageItem.CPackageId,
            //Lenght = packageItem.NLength,
            //Height = packageItem.NHeight,
            //Width = packageItem.NWidth,
        };
        //PalletLogs.Add(log);
        return log;
    }

    public async Task<Log> Post(Alarm al)
    {
        Log log = new(al, al.Value);
        Logs.Add(log);

        AlarmLog alarmLog = new(al)
        {
            TimestampStart = DateTime.Now,
        };
        AlarmLogs.Add(alarmLog);

        return log;
    }

    /// <summary>
    /// Finishes the alarm log.
    /// </summary>
    /// <param name="alarm">The alarm name.</param>
    /// <returns>A Task.</returns>
    public async Task Put(Alarm alarm, DateTime time)
    {
        foreach (var alarmlg in AlarmLogs.Where(a => a.Name == alarm.Name && !a.Gone).ToArray())
        {
            alarmlg.TimestampEnd = time;
        }
    }

    /// <summary>
    /// Resetting all active alarms (on start and finish program).
    /// </summary>
    private async void ResetAlarmSignals()
    {
        foreach (var alarm in _dbSetAlarmLogs.Where(a => !a.Gone).ToArray())
        {
            Put(_dbSetAlarms.First(s => s.Name == alarm.Name), DateTime.Now);
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