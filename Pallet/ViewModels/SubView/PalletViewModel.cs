using Pallet.Infrastructure.Commands;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Managers.Interfaces;
using Pallet.ViewModels.Base;
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
    private bool CanCanvasReloadCommandExecute(object arg) => true;

    /// <summary>
    /// Canvas reload.
    /// </summary>
    /// <param name="obj">The obj.</param>
    public void OnCanvasReloadCommandExecuted(object obj) => _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentTask, _ManagerProfiles.GetTaskNails(), _ManagerProfiles.GetTaskParts());

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
        _Drawer.CanvasClear();
        _ManagerProfiles.CurrentTaskChanged -= _ManagerProfiles_CurrentTaskChanged;
        _ManagerProfiles.CurrentTaskChanged += _ManagerProfiles_CurrentTaskChanged;
        //_Drawer.CanvasPaintTask(_ManagerProfiles.CurrentTask,_ManagerProfiles.GetTaskNails());
    }

    /// <summary>
    /// Active profile changed add delegate.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void _ManagerProfiles_CurrentTaskChanged(object? sender, EventArgs e) => _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentTask, _ManagerProfiles.GetTaskNails(), _ManagerProfiles.GetTaskParts());

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
                _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentTask, _ManagerProfiles.GetTaskNails(), _ManagerProfiles.GetTaskParts());
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
                _Drawer.CanvasPaintTask(_ManagerProfiles.CurrentTask, _ManagerProfiles.GetTaskNails(), _ManagerProfiles.GetTaskParts());
            }
        }
    }

    private double _CanvasHeight;
}