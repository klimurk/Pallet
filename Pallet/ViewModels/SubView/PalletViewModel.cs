using HelixToolkit.Wpf;
using Pallet.Infrastructure.Commands;
using Pallet.Services.Draw;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Managers.Interfaces;
using Pallet.ViewModels.Base;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Pallet.ViewModels.SubView;
/// <summary>
/// The pallet view model.
/// </summary>

public class PalletViewModel : ViewModel
{
    #region Services

    private readonly IManagerProfiles _ManagerProfiles;

    #endregion Services

    /// <summary>
    /// Items for drawing in ItemsControl Canvas.
    /// </summary>
    public ObservableCollection<Shape> Items => _Drawer.Items;

    //public Model3D Model { get; set; }
    private Model3D _Model;
    public Model3D Model
        => _Drawer.myModel;
    //{
    //    get => _Model;
    //    set =>  Set(ref _Model, value);
    //}

    

    /// <summary>
    /// Combobox items for highlight.
    /// </summary>

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
    private bool CanCanvasReloadCommandExecute(object arg) => _ManagerProfiles.CurrentTask is not null;

    /// <summary>
    /// Canvas reload.
    /// </summary>
    /// <param name="obj">The obj.</param>
    public void OnCanvasReloadCommandExecuted(object obj) => _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentProfile);

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

    #endregion Commands

    /// <summary>
    /// Initializes a new instance of the <see cref="PalletViewModel"/> class.
    /// </summary>
    public PalletViewModel()
    {
        _ManagerProfiles = App.Services.GetService(typeof(IManagerProfiles)) as IManagerProfiles;

        _Drawer = App.Services.GetService(typeof(IDrawer)) as IDrawer;

        AsyncInitialization().ConfigureAwait(false);
        _Drawer.RefreshModelEventHandler += _Drawer_RefreshModelEventHandler;
       // Model = _Drawer.myModel;
    }

    private void _Drawer_RefreshModelEventHandler(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(Model));
    }

    protected override async Task AsyncInitialization()
    {
        _Drawer.CanvasClear();
        
        _ManagerProfiles.CurrentTaskChanged -= _ManagerProfiles_CurrentTaskChanged;
        _ManagerProfiles.CurrentTaskChanged += _ManagerProfiles_CurrentTaskChanged;
    }

    /// <summary>
    /// Active profile changed add delegate.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void _ManagerProfiles_CurrentTaskChanged(object? sender, EventArgs e) => _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentProfile);

    /// <summary>
    /// Canvas width.
    /// </summary>
    public double CanvasWidth
    {
        get => _CanvasWidth;
        set
        {
            if (Set(ref _CanvasWidth, value))
            {
                _Drawer.CanvasWidth = value;
                _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentProfile);
            }
        }
    }

    private double _CanvasWidth;

    /// <summary>
    /// Canvas height.
    /// </summary>
    public double CanvasHeight
    {
        get => _CanvasHeight;
        set
        {
            if (Set(ref _CanvasHeight, value))
            {
                _Drawer.CanvasHeight = value;
                _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentProfile);
            }
        }
    }

    private double _CanvasHeight;
}