using Pallet.Database.Entities.OPC;
using Pallet.Services.Logging.Interfaces;
using Pallet.ViewModels.Base;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace Pallet.ViewModels.SubView;
/// <summary>
/// The alarm view model.
/// </summary>
public class AlarmViewModel : ViewModel
{
    #region Services

    private readonly IAlarmLogService _AlarmLogService;

    #endregion Services

    #region Fields

    private readonly CollectionViewSource LogsViewSourse;
    /// <summary>
    /// Gets the logs view. (On screen)
    /// </summary>
    public ICollectionView? LogsView => LogsViewSourse.View;

    /// <summary>
    /// Gets or sets the data update timer.
    /// </summary>
    private static DispatcherTimer? DataUpdateTimer { get; set; }

    #endregion Fields

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="AlarmViewModel"/> class.
    /// </summary>
    /// <param name="AlarmLogService">The alarm log service.</param>
    public AlarmViewModel(IAlarmLogService AlarmLogService)
    {
        _AlarmLogService = AlarmLogService;
        LogsViewSourse = new()
        {
            Source = _AlarmLogService.AlarmLogs,
            SortDescriptions = { new SortDescription(nameof(AlarmLog.TimestampStart), ListSortDirection.Descending) }
        };

        _AlarmLogService.AlarmLogs.CollectionChanged += AlarmLogs_CollectionChanged;

        SetupDataUpdateTimer();
    }

    /// <summary>
    /// Setups the data update timer.
    /// </summary>
    private void SetupDataUpdateTimer()
    {
        DataUpdateTimer = new DispatcherTimer();
        DataUpdateTimer.Tick += OnDataUpdateEvent;
        DataUpdateTimer.Interval = TimeSpan.FromMilliseconds(1000);
        DataUpdateTimer.Start();
    }

    #endregion Ctor

    #region Events

    /// <summary>
    /// Alarm logs collection changed. Refresh view
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void AlarmLogs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => LogsView?.Refresh();

    /// <summary>
    /// On the data update event. Refresh the view
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void OnDataUpdateEvent(object sender, EventArgs e) => LogsView?.Refresh();

    #endregion Events
}