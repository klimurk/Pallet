using Pallet.Database.Entities.Change.Products;
using Pallet.Database.Entities.Change.Profiles;
using Pallet.Database.Entities.Change.Types;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Managers.Interfaces;
using Pallet.ViewModels.Base;
using System.Collections.Specialized;
using System.Windows.Shapes;
using static Pallet.Services.Draw.Drawer;

namespace Pallet.ViewModels.SubView;

public class PalletViewModel : ViewModel
{
    #region Services

    private IManagerProfiles _ManagerProfiles;

    public IDbRepository<Element> IRepositoryElements;

    public IDbRepository<ElementPosition>? IRepositoryElementPositions { get; }

    #endregion Services

    public ObservableCollection<Shape> Items
    {
        get => _Items;
        set => Set(ref _Items, value);
    }

    private ObservableCollection<Shape> _Items;

    public ObservableCollection<string> ComboboxItems
    {
        get => _ComboboxItems;
        set => Set(ref _ComboboxItems, value);
    }

    private ObservableCollection<string> _ComboboxItems;

    private string _ComboboxItem;

    public string ComboboxItem
    {
        get => _ComboboxItem;
        set
        {
            _Drawer.HighlightSelected(value);
            Set(ref _ComboboxItem, value);
        }
    }

    public bool StatusTAB
    {
        get => _Drawer.IsStatusTAB;
        set
        {
            _Drawer.IsStatusTAB = value;
            OnPropertyChanged(nameof(StatusTAB));
        }
    }

    public bool StatusWRK
    {
        get => _Drawer.IsStatusWRK;
        set
        {
            _Drawer.IsStatusWRK = value;
            OnPropertyChanged(nameof(StatusWRK));
        }
    }

    public bool StatusPROD
    {
        get => _Drawer.IsStatusPROD;
        set
        {
            _Drawer.IsStatusPROD = value;
            OnPropertyChanged(nameof(StatusPROD));
        }
    }

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

    public ICommand CanvasReloadCommand => _CanvasReloadCommand ??= new LambdaCommand(OnCanvasReloadCommandExecuted, CanCanvasReloadCommandExecute);

    private bool CanCanvasReloadCommandExecute(object arg) => _ManagerProfiles.ActiveProfile is not null;

    private void OnCanvasReloadCommandExecuted(object obj) => _Drawer.CanvasPaintProfile(_ManagerProfiles.ActiveProfile);

    #endregion CanvasReloadCommand

    #region CanvasClearCommand

    private ICommand _CanvasClearCommand;

    public ICommand CanvasClearCommand => _CanvasClearCommand ??= new LambdaCommand(OnCanvasClearCommandExecuted, CanCanvasClearCommandExecute);

    private bool CanCanvasClearCommandExecute(object arg) => true;

    private void OnCanvasClearCommandExecuted(object obj) => _Drawer.CanvasClear();

    private readonly IDrawer _Drawer;

    #endregion CanvasClearCommand

    private Profile _ActiveProfile;

    #endregion Commands

    public PalletViewModel()
    {
        _ManagerProfiles = App.Services.GetService(typeof(IManagerProfiles)) as IManagerProfiles;
        _Drawer = App.Services.GetService(typeof(IDrawer)) as IDrawer;

        Items = _Drawer.Items;
        ComboboxItems = _Drawer.ComboboxItems;

        //Items.CollectionChanged += Items_CollectionChanged;
        //_Drawer.ProductElementTouch += _Drawer_ProductElementTouch;
        ComboboxItems.CollectionChanged += ComboboxItems_CollectionChanged;

        StatusTAB = true;
        ViewDirection = ObjectViewDirection.Top;
        _ManagerProfiles.ActiveProfileChanged += _ManagerProfiles_ActiveProfileChanged;
    }

    protected override void Dispose(bool Disposing)
    {
        _ManagerProfiles.ActiveProfileChanged -= _ManagerProfiles_ActiveProfileChanged;
        //Items.CollectionChanged -= Items_CollectionChanged;
        ComboboxItems.CollectionChanged -= ComboboxItems_CollectionChanged;
        base.Dispose(Disposing);
    }

    private void _ManagerProfiles_ActiveProfileChanged(object? sender, EventArgs e) => _Drawer.CanvasPaintProfile(_ManagerProfiles.ActiveProfile);

    //private void _Drawer_ProductElementTouch(object? sender, EventArgs e)
    //{
    //}

    private void ComboboxItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ComboboxItems.Refresh();
    }

    //private void Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    //{
    //}

    #region "Public Properties & Methods"

    #region LayerVis

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

    public double CanvasWidth
    {
        get => _Drawer.CanvasWidth;
        set
        {
            _Drawer.CanvasWidth = value;
            OnPropertyChanged(nameof(CanvasWidth));
        }
    }

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