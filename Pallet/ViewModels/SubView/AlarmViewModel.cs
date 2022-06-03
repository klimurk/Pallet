using Pallet.Database.Entities.OPC;
using Pallet.Services.Logging.Interfaces;
using Pallet.ViewModels.Base;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace Pallet.ViewModels.SubView;

public class AlarmViewModel : ViewModel
{
    #region Services

    private readonly IAlarmLogService _AlarmLogService;

    #endregion Services

    #region Fields

    private readonly CollectionViewSource LogsViewSourse;
    public ICollectionView? LogsView => LogsViewSourse.View;

    private static DispatcherTimer _dataUpdateTimer;

    #endregion Fields

    #region Ctor

    public AlarmViewModel()
    {
        _AlarmLogService = App.Services.GetService(typeof(IAlarmLogService)) as IAlarmLogService;
        LogsViewSourse = new()
        {
            Source = _AlarmLogService.AlarmLogs,
            SortDescriptions = { new SortDescription(nameof(AlarmLog.TimestampStart), ListSortDirection.Descending) }
        };

        _AlarmLogService.AlarmLogs.CollectionChanged += AlarmLogs_CollectionChanged;

        SetupDataUpdateTimer();
    }

    private void SetupDataUpdateTimer()
    {
        _dataUpdateTimer = new DispatcherTimer();
        _dataUpdateTimer.Tick += OnDataUpdateEvent;
        _dataUpdateTimer.Interval = TimeSpan.FromMilliseconds(1000);
        _dataUpdateTimer.Start();
    }

    #endregion Ctor

    #region Events

    private void AlarmLogs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => LogsView?.Refresh();

    private void OnDataUpdateEvent(object sender, EventArgs e) => LogsView?.Refresh();

    #endregion Events
}