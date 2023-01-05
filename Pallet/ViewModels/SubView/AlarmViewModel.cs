using Pallet.InternalDatabase.Entities.OPC;
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

    private readonly ILogService _AlarmLogService;

    #endregion Services

    #region Fields

    private DateTime _FilterDateFrom;

    public DateTime FilterDateFrom
    {
        get => _FilterDateFrom;
        set => Set(ref _FilterDateFrom, value);
    }

    private DateTime _FilterDateTo;

    public DateTime FilterDateTo
    {
        get => _FilterDateTo;
        set
        {
            if (Set(ref _FilterDateTo, value))
                LogsViewSourse.View.Refresh();
        }
    }

    private bool _IsFilterDateTimeEnd;

    public bool IsFilterDateTimeEnd
    {
        get => _IsFilterDateTimeEnd;
        set
        {
            if (Set(ref _IsFilterDateTimeEnd, value))
                LogsViewSourse.View.Refresh();
        }
    }

    private readonly CollectionViewSource LogsViewSourse;

    #region Filtering (view)

    public string FilterDevice
    {
        get => _FilterDevice;
        set
        {
            if (Set(ref _FilterDevice, value))
                LogsViewSourse.View.Refresh();
        }
    }

    private string _FilterDevice;

    #endregion Filtering (view)

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
    public AlarmViewModel()
    {
        _AlarmLogService = App.Services.GetService(typeof(ILogService)) as ILogService;
        LogsViewSourse = new()
        {
            Source = _AlarmLogService.AlarmLogs,
            SortDescriptions = { new SortDescription(nameof(AlarmLog.TimestampStart), ListSortDirection.Descending) }
        };
        AsyncInitialization().ConfigureAwait(false);
    }
    protected override async Task AsyncInitialization()
    {
        FilterDateFrom = DateTime.Today;
        FilterDateTo = DateTime.Now;
        LogsViewSourse.Filter += LogsViewSourse_Filter; ;

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

    private void LogsViewSourse_Filter(object sender, FilterEventArgs e)
    {
        if (e.Item is not AlarmLog log) return;
        FilterDevice ??= "";
        if (!(
            (
                log.Device.ToLower().Contains(FilterDevice.ToLower())
                || log.Address.ToLower().Contains(FilterDevice.ToLower())
                || log.DescriptionEn.ToLower().Contains(FilterDevice.ToLower())
                || log.DescriptionDe.ToLower().Contains(FilterDevice.ToLower())
                || log.DescriptionLocal.ToLower().Contains(FilterDevice.ToLower())
            )
            && ((log.TimestampStart > FilterDateFrom) && (log.TimestampStart < FilterDateTo))
            && (IsFilterDateTimeEnd ? log.TimestampEnd < FilterDateTo : true)
            ))
            e.Accepted = false;
    }

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