using Pallet.InternalDatabase.Entities.Log;
using Pallet.Services.Logging.Interfaces;
using Pallet.ViewModels.Base;
using System.Collections.Specialized;

namespace Pallet.ViewModels.SubView;

/// <summary>
/// The alarm view model.
/// </summary>
public class PalletLogViewModel : ViewModel
{
    #region Services

    private readonly ILogService _LogService;

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

    private readonly CollectionViewSource LogsViewSourse;

    /// <summary>
    /// Gets the logs view. (On screen)
    /// </summary>
    public ICollectionView? LogsView => LogsViewSourse.View;

    /// <summary>
    /// Gets or sets the data update timer.
    /// </summary>
    //private static DispatcherTimer? DataUpdateTimer { get; set; }

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

    #endregion Fields

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="LogViewModel"/> class.
    /// </summary>
    /// <param name="AlarmLogService">The alarm log service.</param>
    public PalletLogViewModel()
    {
        _LogService = App.Services.GetService(typeof(ILogService)) as ILogService;
        LogsViewSourse = new()
        {
            Source = _LogService.Logs,
            SortDescriptions = { new SortDescription(nameof(Log.Timestamp), ListSortDirection.Descending) }
        };
        AsyncInitialization().ConfigureAwait(false);

        //SetupDataUpdateTimer();
    }

    protected override async Task AsyncInitialization()
    {
       
        FilterDateFrom = DateTime.Today;
        FilterDateTo = DateTime.Now;
        LogsViewSourse.Filter += LogsViewSourse_Filter; ;

        _LogService.Logs.CollectionChanged += Logs_CollectionChanged;
    }

    private void LogsViewSourse_Filter(object sender, FilterEventArgs e)
    {
        if (e.Item is not Log log) return;
        FilterDevice ??= "";
        log.Device ??= "";
        log.Address ??= "";
        if (!(
            (
                (log.Device.ToLower().Contains(FilterDevice.ToLower()))
                || (log.Address.ToLower().Contains(FilterDevice.ToLower()))
                || (log.DescriptionEn.ToLower().Contains(FilterDevice.ToLower()))
                || (log.DescriptionDe.ToLower().Contains(FilterDevice.ToLower()))
                || (log.DescriptionLocal.ToLower().Contains(FilterDevice.ToLower()))
            )
            && ((log.Timestamp > FilterDateFrom) && (log.Timestamp < FilterDateTo))
            ))
            e.Accepted = false;
    }

    /// <summary>
    /// Setups the data update timer.
    /// </summary>
    //private void SetupDataUpdateTimer()
    //{
    //    DataUpdateTimer = new DispatcherTimer();
    //    DataUpdateTimer.Tick += OnDataUpdateEvent;
    //    DataUpdateTimer.Interval = TimeSpan.FromMilliseconds(1000);
    //    DataUpdateTimer.Start();
    //}

    #endregion Ctor

    #region Events

    /// <summary>
    /// Alarm logs collection changed. Refresh view
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void Logs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => LogsView?.Refresh();

    /// <summary>
    /// On the data update event. Refresh the view
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    //private void OnDataUpdateEvent(object sender, EventArgs e) => LogsView?.Refresh();

    #endregion Events
}