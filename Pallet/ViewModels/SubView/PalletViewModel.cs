using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Managers.Interfaces;
using Pallet.ViewModels.Base;
using System.Collections.Specialized;
using System.Windows.Shapes;
using static Pallet.Services.Draw.Drawer;

namespace Pallet.ViewModels.SubView;
/// <summary>
/// The pallet view model.
/// </summary>

public class PalletViewModel : ViewModel
{
    private readonly IDbRepository<Element> _Elements;

    #region Services

    private readonly IManagerProfiles _ManagerProfiles;

    private readonly IManagerLanguage _ManagerLanguage;

    #endregion Services

    /// <summary>
    /// Items for drawing in ItemsControl Canvas.
    /// </summary>
    public ObservableCollection<Shape> Items
    {
        get => _Items;
        set => Set(ref _Items, value);
    }

    private ObservableCollection<Shape> _Items;

    /// <summary>
    /// Combobox items for highlight.
    /// </summary>
    public ObservableCollection<string> ComboboxItems
    {
        get => _ComboboxItems;
        set => Set(ref _ComboboxItems, value);
    }

    private ObservableCollection<string> _ComboboxItems;

    private string _ComboboxItem;

    /// <summary>
    /// Combobox selected item.
    /// </summary>
    public string ComboboxItem
    {
        get => _ComboboxItem;
        set
        {
            _Drawer.HighlightSelected(value);
            var str = value.Split('_')[0];
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "en":
                    ComboboxItemDescription = _Elements.Items.First(s => s.Name == str).DescriptionEn;
                    break;

                case "de":
                    ComboboxItemDescription = _Elements.Items.First(s => s.Name == str).DescriptionDe;
                    break;

                default:
                    ComboboxItemDescription = _Elements.Items.First(s => s.Name == str).DescriptionLocal;
                    break;
            }
            Set(ref _ComboboxItem, value);
        }
    }

    public string ComboboxItemDescription
    {
        get => _ComboboxItemDescription;
        set => Set(ref _ComboboxItemDescription, value);
    }

    private string _ComboboxItemDescription;

    /// <summary>
    /// Status table.
    /// </summary>
    public bool StatusTAB
    {
        get => _Drawer.IsStatusTAB;
        set
        {
            _Drawer.IsStatusTAB = value;
            OnPropertyChanged(nameof(StatusTAB));
        }
    }

    /// <summary>
    /// Status work.
    /// </summary>
    public bool StatusWRK
    {
        get => _Drawer.IsStatusWRK;
        set
        {
            _Drawer.IsStatusWRK = value;
            OnPropertyChanged(nameof(StatusWRK));
        }
    }

    /// <summary>
    /// Status product.
    /// </summary>
    public bool StatusPROD
    {
        get => _Drawer.IsStatusPROD;
        set
        {
            _Drawer.IsStatusPROD = value;
            OnPropertyChanged(nameof(StatusPROD));
        }
    }

    /// <summary>
    /// Status preparation.
    /// </summary>
    public bool StatusPREP
    {
        get => _Drawer.IsStatusPREP;
        set
        {
            _Drawer.IsStatusPREP = value;
            OnPropertyChanged(nameof(StatusPREP));
        }
    }

    #region Commands

    #region CanvasReloadCommand

    private ICommand _CanvasReloadCommand;

    /// <summary>
    /// Canvas reload command.
    /// </summary>
    public ICommand CanvasReloadCommand => _CanvasReloadCommand ??= new LambdaCommand(OnCanvasReloadCommandExecuted, CanCanvasReloadCommandExecute);

    /// <summary>
    /// Can canvas reload .
    /// </summary>
    /// <param name="arg">The arg.</param>
    /// <returns>A bool.</returns>
    private bool CanCanvasReloadCommandExecute(object arg) => _ManagerProfiles.ActiveProfile is not null;

    /// <summary>
    /// Canvas reload.
    /// </summary>
    /// <param name="obj">The obj.</param>
    private void OnCanvasReloadCommandExecuted(object obj) => _Drawer.CanvasPaintProfile(_ManagerProfiles.ActiveProfile);

    #endregion CanvasReloadCommand

    #region CanvasClearCommand

    private ICommand _CanvasClearCommand;

    /// <summary>
    /// Canvas clear command.
    /// </summary>
    public ICommand CanvasClearCommand => _CanvasClearCommand ??= new LambdaCommand(OnCanvasClearCommandExecuted, CanCanvasClearCommandExecute);

    /// <summary>
    /// Can canvas clear.
    /// </summary>
    /// <param name="arg">The arg.</param>
    /// <returns>A bool.</returns>
    private bool CanCanvasClearCommandExecute(object arg) => true;

    /// <summary>
    /// Canvas clear.
    /// </summary>
    /// <param name="obj">The obj.</param>
    private void OnCanvasClearCommandExecuted(object obj) => _Drawer.CanvasClear();

    private readonly IDrawer _Drawer;

    #endregion CanvasClearCommand

    private Profile _ActiveProfile;
    //private static string __ResourceManagerNamespace = "Pallet.Resources.SubViews.PalletView.PalletViewResource";

    #endregion Commands

    /// <summary>
    /// Initializes a new instance of the <see cref="PalletViewModel"/> class.
    /// </summary>
    public PalletViewModel(IManagerProfiles ManagerProfiles, IDrawer Drawer, IManagerLanguage ManagerLanguage, IDbRepository<Element> Elements)
    {
        _Elements = Elements;
        _ManagerProfiles = ManagerProfiles;
        _Drawer = Drawer;

        Items = _Drawer.Items;
        ComboboxItems = _Drawer.ComboboxItems;

        ComboboxItems.CollectionChanged += ComboboxItems_CollectionChanged;

        StatusTAB = true;
        ViewDirection = ObjectViewDirection.Top;
        _ManagerProfiles.ActiveProfileChanged += _ManagerProfiles_ActiveProfileChanged;

        //_ManagerLanguage = ManagerLanguage;
        //_ManagerLanguage.ManageNewResource(__ResourceManagerNamespace);
    }

    /// <summary>
    /// On disposing viewmodel.
    /// </summary>
    /// <param name="Disposing">If true, disposing.</param>
    protected override void Dispose(bool Disposing)
    {
        _ManagerProfiles.ActiveProfileChanged -= _ManagerProfiles_ActiveProfileChanged;
        ComboboxItems.CollectionChanged -= ComboboxItems_CollectionChanged;
        base.Dispose(Disposing);
    }

    /// <summary>
    /// Active profile changed add delegate.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void _ManagerProfiles_ActiveProfileChanged(object? sender, EventArgs e) => _Drawer.CanvasPaintProfile(_ManagerProfiles.ActiveProfile);

    /// <summary>
    /// Comboboxes Collection changed delegate.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void ComboboxItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ComboboxItems.Refresh();

    #region "Public Properties & Methods"

    #region LayerVis

    /// <summary>
    /// Layers.
    /// </summary>
    public ushort LayerVis
    {
        get => _Drawer.LayerVis;
        set
        {
            _Drawer.LayerVis = value;
            OnPropertyChanged(nameof(LayerVis));
        }
    }

    #endregion LayerVis

    #region WProdName

    /// <summary>
    /// Product name.
    /// </summary>
    public string ProdName
    {
        get => _ActiveProfile.ProfileProducts.First().Product.Name;
        set
        {
            _ActiveProfile.ProfileProducts.First().Product.Name = value;
            OnPropertyChanged(nameof(ProdName));
        }
    }

    #endregion WProdName

    #region TableName

    /// <summary>
    /// Table name.
    /// </summary>
    public string TableName
    {
        get => _ActiveProfile.Table.Name;
        set
        {
            _ActiveProfile.Table.Name = value;
            OnPropertyChanged(nameof(TableName));
        }
    }

    #endregion TableName

    #region WTableSide

    /// <summary>
    /// Table side.
    /// </summary>
    public bool WTableSide
    {
        get => _Drawer.IsTableSideFront;
        set
        {
            _Drawer.IsTableSideFront = value;
            OnPropertyChanged(nameof(WTableSide));
        }
    }

    #endregion WTableSide

    #region ShowWTable

    /// <summary>
    /// Show table.
    /// </summary>
    public bool ShowWTable
    {
        get => _Drawer.IsShowTable;
        set
        {
            _Drawer.IsShowTable = value;
            OnPropertyChanged(nameof(ShowWTable));
        }
    }

    #endregion ShowWTable

    /// <summary>
    /// Canvas width.
    /// </summary>
    public double CanvasWidth
    {
        get => _Drawer.CanvasWidth;
        set
        {
            _Drawer.CanvasWidth = value;
            OnPropertyChanged(nameof(CanvasWidth));
        }
    }

    /// <summary>
    /// Canvas height.
    /// </summary>
    public double CanvasHeight
    {
        get => _Drawer.CanvasHeight;
        set
        {
            _Drawer.CanvasHeight = value;
            OnPropertyChanged(nameof(CanvasHeight));
        }
    }

    #region ShowWTableL

    /// <summary>
    /// Show  table left side.
    /// </summary>
    public bool ShowWTableL
    {
        get => _Drawer.IsShowTableL;
        set
        {
            _Drawer.IsShowTableL = value;
            OnPropertyChanged(nameof(ShowWTableL));
        }
    }

    #endregion ShowWTableL

    #region ShowWTableR

    /// <summary>
    /// Show  table right side.
    /// </summary>
    public bool ShowWTableR
    {
        get => _Drawer.IsShowTableR;
        set
        {
            _Drawer.IsShowTableR = value;
            OnPropertyChanged(nameof(ShowWTableR));
        }
    }

    #endregion ShowWTableR

    #region ViewDirection

    /// <summary>
    /// View direction on product.
    /// </summary>
    public ObjectViewDirection ViewDirection
    {
        get => _Drawer.ViewDirection;
        set
        {
            _Drawer.ViewDirection = value;
            OnPropertyChanged(nameof(ViewDirection));
        }
    }

    #endregion ViewDirection

    #endregion "Public Properties & Methods"
}