using Pallet.Database.Entities.Change.Profiles;
using System.Windows.Shapes;
using static Pallet.Services.Draw.Drawer;

namespace Pallet.Services.Draw.Interface;

public interface IDrawer
{
    double CanvasHeight { get; set; }
    double CanvasWidth { get; set; }

    //string ComboboxItem { get; set; }
    ObservableCollection<string> ComboboxItems { get; set; }

    //string CurrentPosition { get; set; }
    ObservableCollection<Shape> Items { get; set; }

    ushort LayerVis { get; set; }

    //int Left { get; set; }
    //string ProdName { get; set; }

    //Color RulerColor { get; set; }
    //string Selection { get; set; }
    //bool ShowRulers { get; set; }
    //bool ShowStep1 { get; set; }

    //bool ShowStep2 { get; set; }
    bool IsShowTable { get; set; }

    bool IsShowTableL { get; set; }
    bool IsShowTableR { get; set; }
    bool IsStatusPREP { get; set; }
    bool IsStatusPROD { get; set; }
    bool IsStatusTAB { get; set; }
    bool IsStatusWRK { get; set; }
    //string TableName { get; set; }

    //int Top { get; set; }
    ObjectViewDirection ViewDirection { get; set; }

    bool IsTableSideFront { get; set; }

    public void CanvasPaintProfile(Profile profile);

    public void CanvasClear();

    public void HighlightSelected(string sel = "");

    public event EventHandler ProductElementTouch;
}