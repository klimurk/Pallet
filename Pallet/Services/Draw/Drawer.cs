using Pallet.Database.Entities.ProfileData.Products;
using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Entities.ProfileData.Tables;
using Pallet.Services.Draw.Interface;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pallet.Services.Draw;

/// <summary>
/// The drawer service.
/// Working as collection of System.Windows.Shapes items which will be added to ItemsControl with canvas as ItemsPanelTemplate
/// </summary>

public partial class Drawer : IDrawer
{
    private readonly ILogger<Drawer> _Logger;

    #region "Class Variables & Constants"

    // View Field Variables
    private Point2D _PointViewFieldLeftBottom;// View Field - Left Bottom Point2D coordinates

    private Point2D _PointViewFieldRightUp;// View Field - Right upper Point2D coordinates

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

    private Profile _ActiveProfile;
    private static readonly Uri UriWoodRound = new("pack://application:,,,/Resources/Wood/WoodR.bmp");
    private static readonly Uri UriWoodHorizontal = new("pack://application:,,,/Resources/Wood/WoodH.bmp");
    private static readonly Uri UriWoodVertical = new("pack://application:,,,/Resources/Wood/WoodV.bmp");

    #endregion "Class Variables & Constants"

    #region "Public Properties & Methods"

    public bool IsTableSideFront { get; set; } // Work Table Side true = Side A,false = Side B

    public ushort LayerVis { get; set; }// Layers Visible

    public bool IsShowTable { get; set; } // Show Work Table

    public bool IsShowTableL { get; set; }// Show Wood product on the left side of the Table

    public bool IsShowTableR { get; set; }// Show Wood product on the right side of the Table

    public ObjectViewDirection ViewDirection { get; set; }// View Direction - S, E, N, W, T

    public ObservableCollection<Shape> Items { get; set; }
    public ObservableCollection<string> ComboboxItems { get; set; }

    public double CanvasWidth { get; set; }

    public double CanvasHeight { get; set; }

    public bool IsStatusTAB { get; set; }

    public bool IsStatusWRK { get; set; }

    public bool IsStatusPROD { get; set; }

    public bool IsStatusPREP { get; set; }

    #endregion "Public Properties & Methods"

    public Drawer(ILogger<Drawer> Logger)
    {
        Items = new();
        ComboboxItems = new();
        _Logger = Logger;

        IsShowTable = true;
        IsShowTableL = true;
        IsShowTableR = true;
        IsStatusTAB = true;
        IsTableSideFront = true;
    }

    #region "Main Menu & Status Bar Events"

    private void ViewFieldNormalize()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Корректировка вида");

        Point2D _PointCenter = new(
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

        ComboboxItems?.Clear();
    }

    #endregion "Main Menu & Status Bar Events"

    #region "Paint Objects functions"

    public void CanvasPaintProfile(Profile profile)
    {
        _Logger.LogInformation("Отрисовка профиля");
        var timer = Stopwatch.StartNew();
        _ActiveProfile = profile;
        if (profile is null) { CanvasClear(); return; }
        Table table = _ActiveProfile.Table;
        CanvasClear();

        if (_ActiveProfile.Table is not null && IsShowTable)
        {
            _PointViewFieldLeftBottom = IsTableSideFront
                ? new(-0.02 * table.SideASizeX, -0.02 * table.SideASizeY)
                : new(-0.02 * table.SideBSizeX, -0.02 * table.SideBSizeY);

            _PointViewFieldRightUp = IsTableSideFront
                ? new(1.02 * table.SideASizeX, 1.02 * table.SideASizeY)
                : new(1.02 * table.SideBSizeX, 1.02 * table.SideBSizeY);

            ViewFieldNormalize();

            CanvasPaintTable(table);
            _Logger.LogInformation("Стол отрисован за {0} с", timer.Elapsed.TotalSeconds);
        }
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
        //foreach (var product in _ActiveProfile.ProfileProducts.Select(profProduct => profProduct.Product))
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
        var products = _ActiveProfile.ProfileProducts.OrderBy(p => p.Position).Select(profProduct => profProduct.Product).ToList();
        if (products.Count > 0)
        {
            if (IsShowTableL)
            {
                Point2D refpnt = IsTableSideFront
                    ? new(table.WorkAreaAOffsetX + table.PlaceA1OffsetX, table.WorkAreaAOffsetY + table.PlaceA1OffsetY)
                    : new(table.WorkAreaBOffsetX + table.PlaceB1OffsetX, table.WorkAreaBOffsetY + table.PlaceB1OffsetY);

                Product prod = IsTableSideFront ? products[0] : products[2];
                List<Nail> nails = prod.Nails.ToList();

                PaintProduct(prod, refpnt, 1);

                foreach (Nail nail in nails) PaintNail(nail, refpnt);

                _Logger.LogInformation("Продукт 1 отрисован за {0} с", timer.Elapsed.TotalSeconds);
            }

            if (IsShowTableR)
            {
                Point2D refpnt = IsTableSideFront
                    ? new(table.WorkAreaAOffsetX + table.PlaceA2OffsetX, table.WorkAreaAOffsetY + table.PlaceA2OffsetY)
                    : new(table.WorkAreaBOffsetX + table.PlaceB2OffsetX, table.WorkAreaBOffsetY + table.PlaceB2OffsetY);

                Product prod = IsTableSideFront ? products[1] : products[3];
                List<Nail> nails = prod.Nails.ToList();

                PaintProduct(prod, refpnt, 1);

                foreach (Nail nail in nails) PaintNail(nail, refpnt);

                _Logger.LogInformation("Продукт 2 отрисован {0} с", timer.Elapsed.TotalSeconds);
            }
        }
        _Logger.LogInformation("Профиль отрисован за {0} с", timer.Elapsed.TotalSeconds);
    }

    private void CanvasPaintTable(Table table)
    {
        // Paint Work Table and Work Area
        PaintRectCol(
            "WorkTable",
            new(0d, 0d),
            IsTableSideFront ? table.SideASizeX : table.SideBSizeX,
            IsTableSideFront ? table.SideASizeY : table.SideBSizeY,
            0.5,
            Colors.Black, Colors.DarkGray,
            new(0d, 0d));
        PaintRectCol(
            "WorkArea",
            new(0d, 0d),
            IsTableSideFront ? table.WorkAreaASizeX : table.WorkAreaBSizeX,
            IsTableSideFront ? table.WorkAreaASizeY : table.WorkAreaBSizeY,
            0.3,
            Colors.Black, Colors.LightGray,
            new(IsTableSideFront ? table.WorkAreaAOffsetX : table.WorkAreaBOffsetX,
            IsTableSideFront ? table.WorkAreaAOffsetY : table.WorkAreaBOffsetY));
    }

    #endregion "Paint Objects functions"

    #region "Paint Essential Object"

    private void PaintLine(string name, Point2D pointBase, double width, double height, double borderThick, Color borderColor, Point2D zero)
    {
        // Work Area
        Point2D pointWA = new(pointBase.X + zero.X, pointBase.Y + zero.Y);

        var X1 = _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X);
        var Y1 = _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y);

        // Paint!

        Items.Add(new Line()
        {
            Name = name,
            StrokeThickness = borderThick,
            Stroke = new SolidColorBrush(borderColor),
            X1 = X1,
            Y1 = Y1,
            X2 = X1 + (_ViewFieldScaleWidth * width),
            Y2 = Y1 - (_ViewFieldScaleHeight * height)
        });
    }

    private void PaintRectCol(string name, Point2D pointBase, double width, double height, double borderThick, Color colorBorder, Color colorBackground, Point2D pointZero)
    {
        // Work Area
        Point2D pointWA = new(pointBase.X + pointZero.X, pointBase.Y + pointZero.Y);

        // Definition
        Rectangle rect = new()
        {
            Name = name,
            StrokeThickness = borderThick,
            Stroke = new SolidColorBrush(colorBorder),
            Fill = new SolidColorBrush(colorBackground),
            Width = _ViewFieldScaleWidth * width,
            Height = _ViewFieldScaleHeight * height
        };

        // Position
        Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - height));

        // Paint!
        try
        {
            Items.Add(rect);
        }
        catch (Exception) { }
    }

    private void PaintProduct(Product product, Point2D pointZero, int side)
    {
        var elementPos = product.Elements;
        switch (ViewDirection)
        {
            case ObjectViewDirection.Top:
                elementPos.OrderBy(s => s.ID);
                break;

            case ObjectViewDirection.South:
                elementPos.OrderByDescending(s => s.PosY);
                break;

            case ObjectViewDirection.North:
                elementPos.OrderBy(s => s.PosY);
                break;

            case ObjectViewDirection.West:
                elementPos.OrderByDescending(s => s.PosX);
                break;

            case ObjectViewDirection.East:
                elementPos.OrderBy(s => s.PosX);
                break;
        }

        foreach (var position in elementPos.OrderBy(s => s.Layer).ToList())
        {
            Rectangle rect = new()
            {
                Name = position.Element.Name + "_" + position.PosID,
                StrokeThickness = _WidthBorder,
                Stroke = new SolidColorBrush(_ColorBorder),
            };
            ImageBrush ib = new()
            {
                TileMode = TileMode.FlipXY,
                Stretch = Stretch.None,
                ViewportUnits = BrushMappingMode.Absolute
            };

            ComboboxItems.Add(rect.Name);

            switch (ViewDirection)
            {
                case ObjectViewDirection.Top:
                    {
                        // Work Area
                        Point3D pointWA = new(position.PosX + pointZero.X, position.PosY + pointZero.Y, 0);
                        rect.Width = _ViewFieldScaleWidth * position.Element.SizeX;
                        rect.Height = _ViewFieldScaleHeight * position.Element.SizeY;
                        // Position
                        Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
                        Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - position.Element.SizeY));
                        // Fill Image
                        switch (position.Element.Direction)
                        {
                            case 0:
                                ib.Viewport = new Rect(0, 0, 100, 50);
                                ib.ImageSource = new BitmapImage(UriWoodHorizontal);
                                break;

                            case 1:
                                ib.Viewport = new Rect(0, 0, 50, 100);
                                ib.ImageSource = new BitmapImage(UriWoodVertical);
                                break;

                            case 2:
                                ib.TileMode = TileMode.Tile;
                                ib.Viewport = new Rect(0, 0, 100, 100);
                                ib.ImageSource = new BitmapImage(UriWoodRound);
                                break;
                        }
                        rect.Fill = ib;
                    }
                    break;

                case ObjectViewDirection.South:
                    {
                        // Work Area
                        Point3D pointWA = new(position.PosX + pointZero.X, 0, position.PosZ);

                        rect.Width = _ViewFieldScaleWidth * position.Element.SizeX;
                        rect.Height = _ViewFieldScaleHeight * position.Element.SizeZ;
                        // Position
                        Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
                        Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Z - position.Element.SizeZ));
                        // Fill Image
                        switch (position.Element.Direction)
                        {
                            case 0:
                                ib.Viewport = new Rect(0, 0, 100, 50);
                                ib.ImageSource = new BitmapImage(UriWoodHorizontal);
                                break;

                            case 1:
                                ib.TileMode = TileMode.Tile;
                                ib.Viewport = new Rect(0, 0, 100, 100);
                                ib.ImageSource = new BitmapImage(UriWoodRound);
                                break;

                            case 2:
                                ib.Viewport = new Rect(0, 0, 50, 100);
                                ib.ImageSource = new BitmapImage(UriWoodVertical);
                                break;
                        }
                        rect.Fill = ib;
                    }
                    break;

                case ObjectViewDirection.East:
                    {
                        // Work Area
                        Point3D pointWA = new(0, position.PosY + pointZero.Y, position.PosZ);

                        rect.Width = _ViewFieldScaleWidth * position.Element.SizeY;
                        rect.Height = _ViewFieldScaleHeight * position.Element.SizeZ;
                        // Position
                        Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.Y - _PointViewFieldLeftBottom.X));
                        Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Z - position.Element.SizeZ));
                        switch (position.Element.Direction)
                        {
                            case 0:
                                ib.TileMode = TileMode.Tile;
                                ib.Viewport = new Rect(0, 0, 100, 100);
                                ib.ImageSource = new BitmapImage(UriWoodRound);
                                break;

                            case 1:
                                ib.Viewport = new Rect(0, 0, 100, 50);
                                ib.ImageSource = new BitmapImage(UriWoodHorizontal);
                                break;

                            case 2:
                                ib.Viewport = new Rect(0, 0, 50, 100);
                                ib.ImageSource = new BitmapImage(UriWoodVertical);
                                break;
                        }
                        rect.Fill = ib;
                    }
                    break;
            }
            //rect.GotTouchCapture += Rect_GotTouchCapture;
            //rect.GotFocus += Rect_GotFocus;
            //rect.LostTouchCapture += Rect_LostTouchCapture;
            // Paint!
            Items.Add(rect);
            if (position.OutLN && ViewDirection == ObjectViewDirection.Top) PaintOutln(position, pointZero);
        }
    }

    //private void Rect_GotFocus(object sender, RoutedEventArgs e)
    //{
    //    if (sender is not Rectangle rect) return;
    //    rect.ToolTip =
    //        _ActiveProfile.ProfileProducts.First(s => s.Product.Elements.Any(pos => pos.Element.Name + pos.PosID == rect.Name)).Product // find product where contain object with name
    //        .Elements.First(s => s.Element.Name == rect.Name) // find element positions where contain name of object
    //        .Element.Name;
    //    HighlightSelected(rect.Name);
    //    ProductElementTouch?.Invoke(this, e);
    //}

    //private void Rect_LostTouchCapture(object? sender, TouchEventArgs e) => HighlightSelected();

    //private void Rect_GotTouchCapture(object? sender, TouchEventArgs e)
    //{
    //    if (sender is not Rectangle rect) return;
    //    rect.ToolTip =
    //        _ActiveProfile.ProfileProducts.First(s => s.Product.Elements.Any(pos => pos.Element.Name + pos.PosID == rect.Name)).Product // find product where contain object with name
    //        .Elements.First(s => s.Element.Name == rect.Name) // find element positions where contain name of object
    //        .Element.Name;
    //    HighlightSelected(rect.Name);
    //    ProductElementTouch?.Invoke(this, e);
    //}

    //public event EventHandler ProductElementTouch;

    private void PaintOutln(ElementPosition elementPosition, Point2D zero)
    {
        // Work Area

        Point2D pointWA = new(elementPosition.PosX + zero.X, elementPosition.PosY + zero.Y);

        // Definition
        Rectangle rect = new()
        {
            Name = "OUTLN_" + elementPosition.Element.Name + "_" + elementPosition.PosID,
            StrokeThickness = _WidthBorder,
            Stroke = new SolidColorBrush(_ColorBorder),
            StrokeDashArray = new DoubleCollection() { 1 },
            Width = _ViewFieldScaleWidth * elementPosition.Element.SizeX,
            Height = _ViewFieldScaleHeight * elementPosition.Element.SizeY
        };
        // Position
        Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - elementPosition.Element.SizeY));
        // Paint!
        Items.Add(rect);
    }

    private void PaintNail(Nail ns, Point2D zero)
    {
        // Fill
        string hexColor = ns.Nailer.Color.ToString("X");
        List<string> list = new();
        int k = 0;
        while (k < hexColor.Length - 1)
        {
            list.Add(hexColor.Substring(k, 2));
            k += 2;
        }
        // Definition
        int? id = Items.Where(s => s.Name.Contains("NAIL_"))?.Count();
        id ??= 0;
        Ellipse nail = new()
        {
            Name = "NAIL_" + ns.Product.Name + "_" + (id + 1).ToString(),
            Uid = id.ToString(),
            Width = _ViewFieldScaleWidth * __NailDiametr,
            Height = _ViewFieldScaleHeight * __NailDiametr,
            Fill = new SolidColorBrush(
                Color.FromArgb(
                    byte.Parse(list[0], NumberStyles.HexNumber),
                    byte.Parse(list[1], NumberStyles.HexNumber),
                    byte.Parse(list[2], NumberStyles.HexNumber),
                    byte.Parse(list[3], NumberStyles.HexNumber)
                    )
                )
        };

        // Work Area
        Point2D pointWA = new(ns.PosX + zero.X, ns.PosY + zero.Y);

        // Position
        Canvas.SetLeft(nail, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
        Canvas.SetTop(nail, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - __NailDiametr));

        Items.Add(nail);
    }

    public void Highlight_Nail(Nail nail)
    {
        _Logger.LogInformation("Подсветка гвоздей");
        var timer = Stopwatch.StartNew();

        _Logger.LogInformation("Добавление подсветки для гвоздя");
        Ellipse nailToHighlight = Items.First(s => s is Ellipse el && el.Name.Contains("NAIL_" + nail.Product.Name)) as Ellipse;
        //foreach (Shape obj in Items.Where(s => s is Ellipse &&))
        //{
        Ellipse highlight = new()
        {
            Name = "_HIGHLIGHT_" + nailToHighlight.Name,
            Stroke = new SolidColorBrush(Colors.Yellow),
            StrokeThickness = 3 * _WidthBorder,
            Fill = null,
            Width = nailToHighlight.Width,
            Height = nailToHighlight.Height
        };

        Canvas.SetLeft(highlight, Canvas.GetLeft(nailToHighlight));
        Canvas.SetTop(highlight, Canvas.GetTop(nailToHighlight));

        Items.Add(highlight);

        _Logger.LogInformation("Подсвечен гвоздь {0}", highlight.Name);
        //}
        _Logger.LogInformation("Подсветка выбранного за {0} с", timer.Elapsed.TotalSeconds);
    }

    #endregion "Paint Essential Object"
}