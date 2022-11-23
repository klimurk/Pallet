using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
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

    /// <summary>
    /// Paint profile on canvas.
    /// </summary>
    /// <param name="profile">The profile.</param>
    public void CanvasPaintTask(PackageItem task, IEnumerable<NailingData> nailList, IEnumerable<WoodenPart> parts);
    /// <summary>
    /// Canvas  clear.
    /// </summary>
    public void CanvasClear();
}