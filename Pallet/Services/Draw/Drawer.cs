using Opc.Ua;
using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Managers.Interfaces;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Pallet.Services.Draw;

/// <summary>
/// The drawer service.
/// Working as collection of System.Windows.Shapes items which will be added to ItemsControl with canvas as ItemsPanelTemplate
/// </summary>

public partial class Drawer : IDrawer
{
    private readonly ILogger<Drawer> _Logger;
    private readonly IManagerProfiles _ManagerProfiles;

    #region "Class Variables & Constants"

    // View Field Variables
    private Point _PointViewFieldLeftBottom;// View Field - Left Bottom Point2D coordinates

    private Point _PointViewFieldRightUp;// View Field - Right upper Point2D coordinates

    /// <summary>
    /// View field width.
    /// </summary>
    private double _ViewFieldWidth { get => _PointViewFieldRightUp.X - _PointViewFieldLeftBottom.X; }

    /// <summary>
    /// View field height.
    /// </summary>
    private double _ViewFieldHeight { get => _PointViewFieldRightUp.Y - _PointViewFieldLeftBottom.Y; }

    /// <summary>
    /// Scale canvas to View field width
    /// </summary>
    private double _ViewFieldScaleWidth { get => CanvasWidth / _ViewFieldWidth; }

    /// <summary>
    /// Scale canvas to View field height
    /// </summary>
    private double _ViewFieldScaleHeight { get => CanvasHeight / _ViewFieldHeight; }

    private readonly double _WidthBorder = 1;

    private const double __NailDiametr = 20.0;
    private Color _ColorBorder = Colors.Black;

    private static readonly Uri UriWoodRound = new("pack://application:,,,/Resources/Wood/WoodR.bmp");
    private static readonly Uri UriWoodHorizontal = new("pack://application:,,,/Resources/Wood/WoodH.bmp");
    private static readonly Uri UriWoodVertical = new("pack://application:,,,/Resources/Wood/WoodV.bmp");

    #endregion "Class Variables & Constants"

    #region "Public Properties & Methods"

    public ObservableCollection<Shape> Items { get; set; }

    public double CanvasWidth { get; set; }

    public double CanvasHeight { get; set; }

    #endregion "Public Properties & Methods"

    public Drawer(ILogger<Drawer> Logger, IManagerProfiles ManagerProfiles)
    {
        Items = new();
        _Logger = Logger;
        _ManagerProfiles = ManagerProfiles;
    }

    #region "Main Menu & Status Bar Events"

    private void ViewFieldNormalize()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Корректировка вида");

        Point _PointCenter = new(
            _ViewFieldWidth / 2d,
            _ViewFieldHeight / 2d
            );

        double _PointRatio = _ViewFieldWidth / _ViewFieldHeight;
        double _CanvasRatio = CanvasWidth / CanvasHeight;

        if (_CanvasRatio > _PointRatio)
        {
            double width = _ViewFieldWidth * _CanvasRatio / _PointRatio;
            _PointViewFieldLeftBottom = new(_PointCenter.X - (width / 2d), _PointViewFieldLeftBottom.Y);
            _PointViewFieldRightUp = new(_PointCenter.X + (width / 2d), _PointViewFieldRightUp.Y);
        }

        if (_CanvasRatio < _PointRatio)
        {
            double height = _ViewFieldHeight * _PointRatio / _CanvasRatio;
            _PointViewFieldLeftBottom = new(_PointViewFieldLeftBottom.X, _PointCenter.Y - (height / 2d));
            _PointViewFieldRightUp = new(_PointViewFieldRightUp.X, _PointCenter.Y + (height / 2d));
        }
        _Logger.LogInformation("Корректировка вида за {0} с", timer.Elapsed.TotalSeconds);
    }

    public void HighlightSelected(string sel = "")
    {
        // Delete all existing selections

        _Logger.LogInformation("Подсветка выбранного");
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Очистка выбранного");
        foreach (Shape item in Items.Where(s => s.Name.Contains("_HIGHLIGHT_") && s is Rectangle).ToList()) Items.Remove(item);

        string[] selList;
        if (sel.Contains(", ")) selList = sel.Split(", ");
        else selList = new string[] { sel };
        _Logger.LogInformation("Добавление выбранного");
        foreach (Shape obj in Items.Where(s => s is Rectangle && selList.Any(name => s.Name == name)).ToList())
        {
            Rectangle highlight = new()
            {
                Name = "_HIGHLIGHT_" + obj.Uid,
                Stroke = new SolidColorBrush(Colors.Yellow),
                StrokeThickness = 3 * _WidthBorder,
                Fill = null,
                Width = obj.Width,
                Height = obj.Height
            };

            Canvas.SetLeft(highlight, Canvas.GetLeft(obj));
            Canvas.SetTop(highlight, Canvas.GetTop(obj));
            Items.Add(highlight);
            _Logger.LogInformation("Подсвечен объект {0}", highlight.Name);
        }
        _Logger.LogInformation("Подсветка выбранного за {0} с", timer.Elapsed.TotalSeconds);
    }

    public void CanvasClear()
    {
        _Logger.LogInformation("Очистка");
        Items?.Clear();
    }

    #endregion "Main Menu & Status Bar Events"

    #region "Paint Objects functions"

    public void CanvasPaintTask(PackageItem task, IEnumerable<NailingData> nailList, IEnumerable<WoodenPart> parts)
    {
        _Logger.LogInformation("Отрисовка профиля");
        var timer = Stopwatch.StartNew();
        CanvasClear();
        if (task is null) return;

        _PointViewFieldLeftBottom = new(-0.02 * task.NLength, -0.02 * task.NWidth);

        _PointViewFieldRightUp = new(1.02 * task.NLength, 1.02 * task.NWidth);

        ViewFieldNormalize();

        CanvasPaintTable(task);
        _Logger.LogInformation("Стол отрисован за {0} с", timer.Elapsed.TotalSeconds);

        { // for pavel sided parts
            //var products = _ActiveProfile.ProfileProducts.Select(profProduct => profProduct.Product).ToList();
            //if (IsTableSideFront)
            //{
            //    if (IsShowTableL)
            //    {
            //        Point2D refpnt = new(table.WorkAreaAOffsetX + table.PlaceA1OffsetX, table.WorkAreaAOffsetY + table.PlaceA1OffsetY);

            //        var getNumbers = (from t in table.PlaceA1Cofiguration where char.IsDigit(t) select int.Parse(t.ToString())).ToArray();
            //        int side = 0;
            //        if (table.PlaceA1Cofiguration.Contains("PREP")) side = 2;
            //        if (table.PlaceA1Cofiguration.Contains("PROD")) side = 1;
            //        if (side > 0) PaintProduct(products[getNumbers[0] - 1], refpnt, side);
            //    }
            //    if (IsShowTableR)
            //    {
            //        Point2D refpnt = new(table.WorkAreaAOffsetX + table.PlaceA2OffsetX, table.WorkAreaAOffsetY + table.PlaceA2OffsetY);

            //        var getNumbers = (from t in table.PlaceA2Cofiguration where char.IsDigit(t) select int.Parse(t.ToString())).ToArray();
            //        int side = 0;
            //        if (table.PlaceA2Cofiguration.Contains("PREP")) side = 2;
            //        if (table.PlaceA2Cofiguration.Contains("PROD")) side = 1;
            //        if (side > 0) PaintProduct(products[getNumbers[0] - 1], refpnt, side);
            //    }
            //}
            //else
            //{
            //    if (IsShowTableL)
            //    {
            //        Point2D refpnt = new(table.WorkAreaBOffsetX + table.PlaceB1OffsetX, table.WorkAreaBOffsetY + table.PlaceB1OffsetY);

            //        var getNumbers = (from t in table.PlaceB1Cofiguration where char.IsDigit(t) select int.Parse(t.ToString())).ToArray();
            //        int side = 0;
            //        if (table.PlaceB1Cofiguration.Contains("PREP")) side = 2;
            //        if (table.PlaceB1Cofiguration.Contains("PROD")) side = 1;
            //        if (side > 0) PaintProduct(products[getNumbers[0] - 1], refpnt, side);
            //    }
            //    if (IsShowTableR)
            //    {
            //        Point2D refpnt = new(table.WorkAreaBOffsetX + table.PlaceB2OffsetX, table.WorkAreaBOffsetY + table.PlaceB2OffsetY);

            //        var getNumbers = (from t in table.PlaceB2Cofiguration where char.IsDigit(t) select int.Parse(t.ToString())).ToArray();
            //        int side = 0;
            //        if (table.PlaceB2Cofiguration.Contains("PREP")) side = 2;
            //        if (table.PlaceB2Cofiguration.Contains("PROD")) side = 1;
            //        if (side > 0) PaintProduct(products[getNumbers[0] - 1], refpnt, side);
            //    }
            //}
        }
        { //foreach (var product in _ActiveProfile.ProfileProducts.Select(profProduct => profProduct.Product))
          //{
          //    if (IsShowTableL)
          //    {
          //        Point2D refpnt = IsTableSideFront
          //            ? new(table.WorkAreaAOffsetX + table.PlaceA1OffsetX, table.WorkAreaAOffsetY + table.PlaceA1OffsetY)
          //            : new(table.WorkAreaBOffsetX + table.PlaceB1OffsetX, table.WorkAreaBOffsetY + table.PlaceB1OffsetY);

            //        PaintProduct(product, refpnt, 1);
            //        _Logger.LogInformation("Продукт 1 отрисован за {0} с", timer.Elapsed.TotalSeconds);
            //    }

            //    if (IsShowTableR)
            //    {
            //        Point2D refpnt = IsTableSideFront
            //            ? new(table.WorkAreaAOffsetX + table.PlaceA2OffsetX, table.WorkAreaAOffsetY + table.PlaceA2OffsetY)
            //            : new(table.WorkAreaBOffsetX + table.PlaceB2OffsetX, table.WorkAreaBOffsetY + table.PlaceB2OffsetY);

            //        PaintProduct(product, refpnt, 2);
            //        _Logger.LogInformation("Продукт 2 отрисован {0} с", timer.Elapsed.TotalSeconds);
            //    }
            //}
        }
        //var products = _ActiveProfile.ProfileProducts.OrderBy(p => p.Position).Select(profProduct => profProduct.Product).ToList();

        Point refpnt = new(0, 0);

        foreach (var part in parts) PaintProduct(part);

        //PaintProduct(prod, refpnt, 1);

        foreach (NailingData nail in nailList) PaintNails(nail);

        _Logger.LogInformation("Профиль отрисован за {0} с", timer.Elapsed.TotalSeconds);
    }

    private void CanvasPaintTable(PackageItem task)
    {
        // Paint Work Table and Work Area
        PaintRectCol(
            new(0d, 0d),
             task.NLength,
             task.NWidth,
            0.5,
            Colors.Black, Colors.DarkGray
           );
        PaintRectCol(
            new(0d, 0d),
             task.NLength,
             task.NWidth,
            0.3,
            Colors.Black, Colors.LightGray

            );
    }

    #endregion "Paint Objects functions"

    #region "Paint Essential Object"

    private void PaintLine(Point pointBase, double width, double height, double borderThick, Color borderColor, Point zero = default)
    {
        // Work Area
        Point pointWA = new(pointBase.X + zero.X, pointBase.Y + zero.Y);

        var X1 = _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X);
        var Y1 = _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y);

        // Paint!

        Items.Add(new Line()
        {
            StrokeThickness = borderThick,
            Stroke = new SolidColorBrush(borderColor),
            X1 = X1,
            Y1 = Y1,
            X2 = X1 + (_ViewFieldScaleWidth * width),
            Y2 = Y1 - (_ViewFieldScaleHeight * height)
        });
    }

    private void PaintRectCol(Point pointBase, double width, double height, double borderThick, Color colorBorder, Color colorBackground, Point zero = default)
    {
        // Definition
        Rectangle rect = new()
        {
            StrokeThickness = borderThick,
            Stroke = new SolidColorBrush(colorBorder),
            Fill = new SolidColorBrush(colorBackground),
            Width = _ViewFieldScaleWidth * width,
            Height = _ViewFieldScaleHeight * height
        };
        rect = (Rectangle)SetPosition(rect, new(pointBase.X, pointBase.Y), zero);
        //// Work Area
        //Point pointWA = new(pointBase.X + zero.X, pointBase.Y + zero.Y);

        //// Position
        //Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        //Canvas.SetBottom(rect, _ViewFieldScaleHeight * (pointWA.Y - _PointViewFieldLeftBottom.Y));
        //Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - height));

        Items.Add(rect);
    }

    private void PaintProduct(WoodenPart part, Point zero = default)
    {
        Rectangle rect = new()
        {
            StrokeThickness = _WidthBorder,
            Stroke = new SolidColorBrush(_ColorBorder),
            Width = _ViewFieldScaleWidth * part.Detail.NBreite,
            Height = _ViewFieldScaleHeight * part.Detail.NLaenge,
        };
        // Fill Image
        Rect viewport = ((double)rect.Width / (double)rect.Height) > 1.2 ? new Rect(0, 0, 100, 50) : ((double)rect.Height / (double)rect.Width) > 1.2 ? new Rect(0, 0, 50, 100) : new Rect(0, 0, 100, 100);
        BitmapImage imgSrc = ((double)rect.Width / (double)rect.Height) > 1.2 ? new BitmapImage(UriWoodHorizontal) : ((double)rect.Height / (double)rect.Width) > 1.2 ? new BitmapImage(UriWoodVertical) : new BitmapImage(UriWoodRound);
        rect.Fill =
            new ImageBrush()
            {
                TileMode = TileMode.FlipXY,
                Stretch = Stretch.None,
                Viewport = viewport,
                ImageSource = imgSrc,
                ViewportUnits = BrushMappingMode.Absolute
            };
        // Position
        //Point pointWA = new(part.Detail.NX + zero.X, part.Detail.NY + zero.Y);
        //Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        //Canvas.SetBottom(rect, _ViewFieldScaleHeight * (pointWA.Y - _PointViewFieldLeftBottom.Y));
        rect=(Rectangle)SetPosition( rect, new(part.Detail.NX, part.Detail.NY), zero);
        //Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - part.Detail.NBreite));
        // Paint!
        Items.Add(rect);
    }

    private void PaintProduct()
    {
        //var elementPos = product.Elements;
        //switch (ViewDirection)
        //{
        //    case ObjectViewDirection.Top:
        //        elementPos.OrderBy(s => s.ID);
        //        break;

        //    case ObjectViewDirection.South:
        //        elementPos.OrderByDescending(s => s.PosY);
        //        break;

        //    case ObjectViewDirection.North:
        //        elementPos.OrderBy(s => s.PosY);
        //        break;

        //    case ObjectViewDirection.West:
        //        elementPos.OrderByDescending(s => s.PosX);
        //        break;

        //    case ObjectViewDirection.East:
        //        elementPos.OrderBy(s => s.PosX);
        //        break;
        //}

        //foreach (var position in elementPos.OrderBy(s => s.Layer).ToList())
        //{
        //    Rectangle rect = new()
        //    {
        //        Name = position.Element.Name + "_" + position.PosID,
        //        StrokeThickness = _WidthBorder,
        //        Stroke = new SolidColorBrush(_ColorBorder),
        //    };
        //    ImageBrush ib = new()
        //    {
        //        TileMode = TileMode.FlipXY,
        //        Stretch = Stretch.None,
        //        ViewportUnits = BrushMappingMode.Absolute
        //    };

        //    ComboboxItems.Add(rect.Name);

        //    switch (ViewDirection)
        //    {
        //        case ObjectViewDirection.Top:
        //            {
        //                // Work Area
        //                Point3D pointWA = new(position.PosX + pointZero.X, position.PosY + pointZero.Y, 0);
        //                rect.Width = _ViewFieldScaleWidth * position.Element.SizeX;
        //                rect.Height = _ViewFieldScaleHeight * position.Element.SizeY;
        //                // Position
        //                Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        //                Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - position.Element.SizeY));
        //                // Fill Image
        //                switch (position.Element.Direction)
        //                {
        //                    case 0:
        //                        ib.Viewport = new Rect(0, 0, 100, 50);
        //                        ib.ImageSource = new BitmapImage(UriWoodHorizontal);
        //                        break;

        //                    case 1:
        //                        ib.Viewport = new Rect(0, 0, 50, 100);
        //                        ib.ImageSource = new BitmapImage(UriWoodVertical);
        //                        break;

        //                    case 2:
        //                        ib.TileMode = TileMode.Tile;
        //                        ib.Viewport = new Rect(0, 0, 100, 100);
        //                        ib.ImageSource = new BitmapImage(UriWoodRound);
        //                        break;
        //                }
        //                rect.Fill = ib;
        //            }
        //            break;

        //        case ObjectViewDirection.South:
        //            {
        //                // Work Area
        //                Point3D pointWA = new(position.PosX + pointZero.X, 0, position.PosZ);

        //                rect.Width = _ViewFieldScaleWidth * position.Element.SizeX;
        //                rect.Height = _ViewFieldScaleHeight * position.Element.SizeZ;
        //                // Position
        //                Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        //                Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Z - position.Element.SizeZ));
        //                // Fill Image
        //                switch (position.Element.Direction)
        //                {
        //                    case 0:
        //                        ib.Viewport = new Rect(0, 0, 100, 50);
        //                        ib.ImageSource = new BitmapImage(UriWoodHorizontal);
        //                        break;

        //                    case 1:
        //                        ib.TileMode = TileMode.Tile;
        //                        ib.Viewport = new Rect(0, 0, 100, 100);
        //                        ib.ImageSource = new BitmapImage(UriWoodRound);
        //                        break;

        //                    case 2:
        //                        ib.Viewport = new Rect(0, 0, 50, 100);
        //                        ib.ImageSource = new BitmapImage(UriWoodVertical);
        //                        break;
        //                }
        //                rect.Fill = ib;
        //            }
        //            break;

        //        case ObjectViewDirection.East:
        //            {
        //                // Work Area
        //                Point3D pointWA = new(0, position.PosY + pointZero.Y, position.PosZ);

        //                rect.Width = _ViewFieldScaleWidth * position.Element.SizeY;
        //                rect.Height = _ViewFieldScaleHeight * position.Element.SizeZ;
        //                // Position
        //                Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.Y - _PointViewFieldLeftBottom.X));
        //                Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Z - position.Element.SizeZ));
        //                switch (position.Element.Direction)
        //                {
        //                    case 0:
        //                        ib.TileMode = TileMode.Tile;
        //                        ib.Viewport = new Rect(0, 0, 100, 100);
        //                        ib.ImageSource = new BitmapImage(UriWoodRound);
        //                        break;

        //                    case 1:
        //                        ib.Viewport = new Rect(0, 0, 100, 50);
        //                        ib.ImageSource = new BitmapImage(UriWoodHorizontal);
        //                        break;

        //                    case 2:
        //                        ib.Viewport = new Rect(0, 0, 50, 100);
        //                        ib.ImageSource = new BitmapImage(UriWoodVertical);
        //                        break;
        //                }
        //                rect.Fill = ib;
        //            }
        //            break;
        //    }
        //    //rect.GotTouchCapture += Rect_GotTouchCapture;
        //    //rect.GotFocus += Rect_GotFocus;
        //    //rect.LostTouchCapture += Rect_LostTouchCapture;
        //    // Paint!
        //    Items.Add(rect);
        //    if (position.OutLN && ViewDirection == ObjectViewDirection.Top) PaintOutln(position, pointZero);
        //}
    }

    private void PaintNails(NailingData ns, Point zero = default)
    {
        // Fill
        Color nailColor = ns.NSaveShot ? Color.FromArgb(255, 7, 250, 218) : Color.FromArgb(255, 32, 122, 171);

        Ellipse nail = new()
        {
            Width = _ViewFieldScaleWidth * __NailDiametr,
            Height = _ViewFieldScaleHeight * __NailDiametr,
            Fill = new SolidColorBrush(nailColor)
        };

        // Position
        nail = (Ellipse)SetPosition(nail, new(ns.NX, ns.NY), zero);
        //Point pointWA = new(ns.NX + zero.X, ns.NY + zero.Y);
        //Canvas.SetLeft(nail, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        //Canvas.SetBottom(nail, _ViewFieldScaleHeight * (pointWA.Y - _PointViewFieldLeftBottom.Y));

        //Canvas.SetTop(nail, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - (_ViewFieldScaleWidth * __NailDiametr)));

        Items.Add(nail);
    }

    #endregion "Paint Essential Object"

    private Shape SetPosition( Shape element, Point newPosition, Point zero = default)
    {
        Point pointWA = new(newPosition.X + zero.X, newPosition.Y + zero.Y);
        Canvas.SetLeft(element, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        Canvas.SetBottom(element, _ViewFieldScaleHeight * (pointWA.Y - _PointViewFieldLeftBottom.Y));
        return element;
    }
}