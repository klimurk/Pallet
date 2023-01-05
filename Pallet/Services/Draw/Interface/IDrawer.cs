using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.Services.Models;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Pallet.Services.Draw.Interface;

/// <summary>
/// The drawer service interface.
/// </summary>
public interface IDrawer
{
    /// <summary>
    /// Canvas height.
    /// </summary>
    double CanvasHeight { get; set; }

    /// <summary>
    /// Canvas width.
    /// </summary>
    double CanvasWidth { get; set; }

    /// <summary>
    /// Combobox items for highlight.
    /// </summary>

    /// <summary>
    /// All drawed items.
    /// </summary>
    ObservableCollection<Shape> Items { get; set; }
    Model3D myModel { get; set; }

    /// <summary>
    /// Paint profile on canvas.
    /// </summary>
    /// <param name="profile">The profile.</param>
    public Task CanvasPaintTask(Profile profile);

    /// <summary>
    /// Canvas  clear.
    /// </summary>
    public Task CanvasClear();

    public event EventHandler? RefreshModelEventHandler;
}