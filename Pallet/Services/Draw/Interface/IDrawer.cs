using Pallet.Database.Entities.ProfileData.Profiles;
using System.Windows.Shapes;
using static Pallet.Services.Draw.Drawer;

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
    ObservableCollection<string> ComboboxItems { get; set; }

    /// <summary>
    /// All drawed items.
    /// </summary>
    ObservableCollection<Shape> Items { get; set; }

    /// <summary>
    /// Layers.
    /// </summary>
    ushort LayerVis { get; set; }

    /// <summary>
    /// Is needed to draw table.
    /// </summary>
    bool IsShowTable { get; set; }

    /// <summary>
    /// Is needed to draw item from table left side .
    /// </summary>
    bool IsShowTableL { get; set; }

    /// <summary>
    /// Is needed to draw item from table right side .
    /// </summary>
    bool IsShowTableR { get; set; }

    /// <summary>
    /// Status preparation.
    /// </summary>
    bool IsStatusPREP { get; set; }

    /// <summary>
    /// Status production.
    /// </summary>
    bool IsStatusPROD { get; set; }

    /// <summary>
    /// Status table.
    /// </summary>
    bool IsStatusTAB { get; set; }

    /// <summary>
    /// Status work.
    /// </summary>
    bool IsStatusWRK { get; set; }

    /// <summary>
    /// View direction on table.
    /// </summary>
    ObjectViewDirection ViewDirection { get; set; }

    /// <summary>
    /// Rotation table side true or false
    /// </summary>
    bool IsTableSideFront { get; set; }

    /// <summary>
    /// Paint profile on canvas.
    /// </summary>
    /// <param name="profile">The profile.</param>
    public void CanvasPaintProfile(Profile profile);

    /// <summary>
    /// Canvas  clear.
    /// </summary>
    public void CanvasClear();

    /// <summary>
    /// Highlights the selected on combobox item.
    /// </summary>
    /// <param name="sel">The sel.</param>
    public void HighlightSelected(string sel = "");

    //public event EventHandler ProductElementTouch;
}