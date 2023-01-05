//using devDept.Eyeshot.Entities;
//using devDept.Geometry;
//using GeometRi;
using HelixToolkit.Wpf;

//using Microsoft.IdentityModel.Tokens;
using Pallet.ExternalDatabase.Models;
using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.Services.Draw.Interface;
using Pallet.Services.Managers.Interfaces;
using Pallet.Services.Models;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Point = System.Windows.Point;
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

    private static Vector3D AxisX = new(1, 0, 0);
    private static Vector3D AxisY = new(0, 1, 0);
    private static Vector3D AxisZ = new(0, 0, 1);

    private static Material greenMaterial = MaterialHelper.CreateMaterial(Colors.Green);
    private static Material redMaterial = MaterialHelper.CreateMaterial(Colors.Red);
    private static Material blueMaterial = MaterialHelper.CreateMaterial(Colors.Blue);
    private static Material insideMaterial = MaterialHelper.CreateMaterial(Colors.Yellow);

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

    public Model3D myModel { get; set; }

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

    public async Task CanvasClear()
    {
        _Logger.LogInformation("Очистка");
        Items?.Clear();
    }

    #endregion "Main Menu & Status Bar Events"

    #region "Paint Objects functions"

    public async Task CorrectSizes()
    {
    }

    public async Task CanvasPaintTask(Profile profile)
    {
        if (profile is null) return;
        PackageItem task = profile.Task;
        IEnumerable<NailingData> nailList = profile.Nails;
        IEnumerable<WoodenPart> parts = profile.Parts;

        _Logger.LogInformation("Отрисовка профиля");
        var timer = Stopwatch.StartNew();

        var minimumPart = parts.OrderBy(s => s.Detail.NX).ThenBy(s => s.Detail.NY).ThenBy(s => s.Detail.NY).First();
        Point3D minimumPoint = new(minimumPart.Detail.NX, minimumPart.Detail.NY, minimumPart.Detail.NZ);

        var modelGroup = new Model3DGroup();

        foreach (var wood in parts)
            modelGroup.Children.Add(Create3DModel(wood.Detail, minimumPoint));

        //foreach (var part in tempParts) PaintProduct(part);

        foreach (NailingData nail in nailList)
            modelGroup.Children.Add(Create3DModel(nail));
        //PaintNails(nail);

        myModel = modelGroup;
        OnRefreshModel();

        _Logger.LogInformation("Профиль отрисован за {0} с", timer.Elapsed.TotalSeconds);
    }

    //private void CanvasPaintTable(PackageItem task)
    //{
    //    // Paint Work Table and Work Area
    //    PaintRectCol(
    //        new(0d, 0d),
    //         task.NLength,
    //         task.NWidth,
    //        0.5,
    //        Colors.Black, Colors.DarkGray
    //       );
    //    PaintRectCol(
    //        new(0d, 0d),
    //         task.NLength,
    //         task.NWidth,
    //        0.3,
    //        Colors.Black, Colors.LightGray

    //        );
    //}

    #endregion "Paint Objects functions"

    #region "Paint Essential Object"

    //private void PaintLine(Point pointBase, double width, double height, double borderThick, Color borderColor, Point zero = default)
    //{
    //    // Work Area
    //    Point pointWA = new(pointBase.X + zero.X, pointBase.Y + zero.Y);

    //    var X1 = _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X);
    //    var Y1 = _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y);

    //    // Paint!

    //    Items.Add(new Line()
    //    {
    //        StrokeThickness = borderThick,
    //        Stroke = new SolidColorBrush(borderColor),
    //        X1 = X1,
    //        Y1 = Y1,
    //        X2 = X1 + (_ViewFieldScaleWidth * width),
    //        Y2 = Y1 - (_ViewFieldScaleHeight * height)
    //    });
    //}

    //private void PaintRectCol(Point pointBase, double width, double height, double borderThick, Color colorBorder, Color colorBackground, Point zero = default)
    //{
    //    // Definition
    //    Rectangle rect = new()
    //    {
    //        StrokeThickness = borderThick,
    //        Stroke = new SolidColorBrush(colorBorder),
    //        Fill = new SolidColorBrush(colorBackground),
    //        Width = _ViewFieldScaleWidth * width,
    //        Height = _ViewFieldScaleHeight * height
    //    };
    //    rect = (Rectangle)SetPosition(rect, new(pointBase.X, pointBase.Y), zero);

    //    Items.Add(rect);
    //}

    //private void PaintProduct(WoodenPart part, Point zero = default)
    //{
    //    double newWidth = part.Detail.NLaenge;
    //    double newHeight = part.Detail.NBreite;
    //    Rectangle rect = new()
    //    {
    //        StrokeThickness = _WidthBorder,
    //        Stroke = new SolidColorBrush(_ColorBorder),
    //        Width = _ViewFieldScaleWidth * newWidth,
    //        Height = _ViewFieldScaleHeight * newHeight,
    //    };
    //    // Fill Image
    //    Rect viewport = ((double)rect.Width / (double)rect.Height) > 1.2 ? new Rect(0, 0, 100, 50) : ((double)rect.Height / (double)rect.Width) > 1.2 ? new Rect(0, 0, 50, 100) : new Rect(0, 0, 100, 100);
    //    BitmapImage imgSrc = ((double)rect.Width / (double)rect.Height) > 1.2 ? new BitmapImage(UriWoodHorizontal) : ((double)rect.Height / (double)rect.Width) > 1.2 ? new BitmapImage(UriWoodVertical) : new BitmapImage(UriWoodRound);
    //    rect.Fill =
    //        new ImageBrush()
    //        {
    //            TileMode = TileMode.FlipXY,
    //            Stretch = Stretch.None,
    //            Viewport = viewport,
    //            ImageSource = imgSrc,
    //            ViewportUnits = BrushMappingMode.Absolute
    //        };

    //    rect = (Rectangle)SetPosition(rect, new(part.Detail.NX, part.Detail.NY), zero);
    //    Items.Add(rect);
    //}

    public event EventHandler? RefreshModelEventHandler;

    public void OnRefreshModel() => RefreshModelEventHandler?.Invoke(this, new());

    //private void PaintProduct()
    //{
    //    //var elementPos = product.Elements;
    //    //switch (ViewDirection)
    //    //{
    //    //    case ObjectViewDirection.Top:
    //    //        elementPos.OrderBy(s => s.ID);
    //    //        break;

    //    //    case ObjectViewDirection.South:
    //    //        elementPos.OrderByDescending(s => s.PosY);
    //    //        break;

    //    //    case ObjectViewDirection.North:
    //    //        elementPos.OrderBy(s => s.PosY);
    //    //        break;

    //    //    case ObjectViewDirection.West:
    //    //        elementPos.OrderByDescending(s => s.PosX);
    //    //        break;

    //    //    case ObjectViewDirection.East:
    //    //        elementPos.OrderBy(s => s.PosX);
    //    //        break;
    //    //}

    //    //foreach (var position in elementPos.OrderBy(s => s.Layer).ToList())
    //    //{
    //    //    Rectangle rect = new()
    //    //    {
    //    //        Name = position.Element.Name + "_" + position.PosID,
    //    //        StrokeThickness = _WidthBorder,
    //    //        Stroke = new SolidColorBrush(_ColorBorder),
    //    //    };
    //    //    ImageBrush ib = new()
    //    //    {
    //    //        TileMode = TileMode.FlipXY,
    //    //        Stretch = Stretch.None,
    //    //        ViewportUnits = BrushMappingMode.Absolute
    //    //    };

    //    //    ComboboxItems.Add(rect.Name);

    //    //    switch (ViewDirection)
    //    //    {
    //    //        case ObjectViewDirection.Top:
    //    //            {
    //    //                // Work Area
    //    //                Point3D pointWA = new(position.PosX + pointZero.X, position.PosY + pointZero.Y, 0);
    //    //                rect.Width = _ViewFieldScaleWidth * position.Element.SizeX;
    //    //                rect.Height = _ViewFieldScaleHeight * position.Element.SizeY;
    //    //                // Position
    //    //                Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
    //    //                Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - position.Element.SizeY));
    //    //                // Fill Image
    //    //                switch (position.Element.Direction)
    //    //                {
    //    //                    case 0:
    //    //                        ib.Viewport = new Rect(0, 0, 100, 50);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodHorizontal);
    //    //                        break;

    //    //                    case 1:
    //    //                        ib.Viewport = new Rect(0, 0, 50, 100);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodVertical);
    //    //                        break;

    //    //                    case 2:
    //    //                        ib.TileMode = TileMode.Tile;
    //    //                        ib.Viewport = new Rect(0, 0, 100, 100);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodRound);
    //    //                        break;
    //    //                }
    //    //                rect.Fill = ib;
    //    //            }
    //    //            break;

    //    //        case ObjectViewDirection.South:
    //    //            {
    //    //                // Work Area
    //    //                Point3D pointWA = new(position.PosX + pointZero.X, 0, position.PosZ);

    //    //                rect.Width = _ViewFieldScaleWidth * position.Element.SizeX;
    //    //                rect.Height = _ViewFieldScaleHeight * position.Element.SizeZ;
    //    //                // Position
    //    //                Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
    //    //                Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Z - position.Element.SizeZ));
    //    //                // Fill Image
    //    //                switch (position.Element.Direction)
    //    //                {
    //    //                    case 0:
    //    //                        ib.Viewport = new Rect(0, 0, 100, 50);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodHorizontal);
    //    //                        break;

    //    //                    case 1:
    //    //                        ib.TileMode = TileMode.Tile;
    //    //                        ib.Viewport = new Rect(0, 0, 100, 100);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodRound);
    //    //                        break;

    //    //                    case 2:
    //    //                        ib.Viewport = new Rect(0, 0, 50, 100);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodVertical);
    //    //                        break;
    //    //                }
    //    //                rect.Fill = ib;
    //    //            }
    //    //            break;

    //    //        case ObjectViewDirection.East:
    //    //            {
    //    //                // Work Area
    //    //                Point3D pointWA = new(0, position.PosY + pointZero.Y, position.PosZ);

    //    //                rect.Width = _ViewFieldScaleWidth * position.Element.SizeY;
    //    //                rect.Height = _ViewFieldScaleHeight * position.Element.SizeZ;
    //    //                // Position
    //    //                Canvas.SetLeft(rect, _ViewFieldScaleWidth * (pointWA.Y - _PointViewFieldLeftBottom.X));
    //    //                Canvas.SetTop(rect, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Z - position.Element.SizeZ));
    //    //                switch (position.Element.Direction)
    //    //                {
    //    //                    case 0:
    //    //                        ib.TileMode = TileMode.Tile;
    //    //                        ib.Viewport = new Rect(0, 0, 100, 100);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodRound);
    //    //                        break;

    //    //                    case 1:
    //    //                        ib.Viewport = new Rect(0, 0, 100, 50);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodHorizontal);
    //    //                        break;

    //    //                    case 2:
    //    //                        ib.Viewport = new Rect(0, 0, 50, 100);
    //    //                        ib.ImageSource = new BitmapImage(UriWoodVertical);
    //    //                        break;
    //    //                }
    //    //                rect.Fill = ib;
    //    //            }
    //    //            break;
    //    //    }
    //    //    //rect.GotTouchCapture += Rect_GotTouchCapture;
    //    //    //rect.GotFocus += Rect_GotFocus;
    //    //    //rect.LostTouchCapture += Rect_LostTouchCapture;
    //    //    // Paint!
    //    //    Items.Add(rect);
    //    //    if (position.OutLN && ViewDirection == ObjectViewDirection.Top) PaintOutln(position, pointZero);
    //    //}
    //}

    //private void PaintNails(NailingData ns, Point zero = default)
    //{
    //    // Fill
    //    Color nailColor = ns.NSaveShot ? Color.FromArgb(255, 7, 250, 218) : Color.FromArgb(255, 32, 122, 171);

    //    System.Windows.Shapes.Ellipse nail = new()
    //    {
    //        Width = _ViewFieldScaleWidth * __NailDiametr,
    //        Height = _ViewFieldScaleHeight * __NailDiametr,
    //        Fill = new SolidColorBrush(nailColor)
    //    };

    //    // Position
    //    nail = (System.Windows.Shapes.Ellipse)SetPosition(nail, new(ns.NX, ns.NY), zero);
    //    //Point pointWA = new(ns.NX + zero.X, ns.NY + zero.Y);
    //    //Canvas.SetLeft(nail, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
    //    //Canvas.SetBottom(nail, _ViewFieldScaleHeight * (pointWA.Y - _PointViewFieldLeftBottom.Y));

    //    //Canvas.SetTop(nail, _ViewFieldScaleHeight * (_PointViewFieldRightUp.Y - pointWA.Y - (_ViewFieldScaleWidth * __NailDiametr)));

    //    Items.Add(nail);
    //}

    #endregion "Paint Essential Object"

    //private Shape SetPosition(Shape element, Point newPosition, Point zero = default)
    //{
    //    Point pointWA = new(newPosition.X + zero.X, newPosition.Y + zero.Y);
    //    Canvas.SetLeft(element, _ViewFieldScaleWidth * (pointWA.X - _PointViewFieldLeftBottom.X));
    //    Canvas.SetBottom(element, _ViewFieldScaleHeight * (pointWA.Y - _PointViewFieldLeftBottom.Y));
    //    return element;
    //}

    private GeometryModel3D Create3DModel(T3dVerpackungDetail woodDetail, Point3D startpoint = null)
    {
        double x = woodDetail.NX - startpoint.X;
        double y = woodDetail.NY - startpoint.Y;
        double z = woodDetail.NZ - startpoint.Z;
        double length = woodDetail.NLaenge;
        double width = woodDetail.NBreite;
        double height = woodDetail.NHoehe;
        double rotateX = woodDetail.NRotateX;
        double rotateY = woodDetail.NRotateY;
        double rotateZ = woodDetail.NRotateZ;

        var meshBuilder = new MeshBuilder(false, false);
        meshBuilder.AddBox(new Point3D(length / 2, width / 2, height / 2), length, width, height);

        MeshGeometry3D mesh = meshBuilder.ToMesh(true);

        GeometryModel3D model = new(mesh, redMaterial) { BackMaterial = insideMaterial };

        Matrix3D transformationMatrix = model.Transform.Value;

        Point3D rotatePoint;
        if (rotateX == 0 && rotateY != 0 && rotateZ == 0)
        {
            transformationMatrix.Translate(new Vector3D(x + width, z, y));
            rotatePoint = new(x + width, z, y);
            transformationMatrix.RotateAt(new Quaternion(AxisZ, rotateY), rotatePoint);
        }
        else if (rotateX == 0 && rotateY == 0 && rotateZ != 0)
        {
            transformationMatrix.Translate(new Vector3D(x, z, y - height));
            rotatePoint = new(x, 0, y);
            transformationMatrix.RotateAt(new Quaternion(AxisY, -rotateZ), rotatePoint);
        }
        else if (rotateX != 0 && rotateY == 0 && rotateZ == 0)
        {
            rotatePoint = new(0, z, y);
            transformationMatrix.Translate(new Vector3D(x, z, y - height));
            transformationMatrix.RotateAt(new Quaternion(AxisX, -rotateX), rotatePoint);
        }
        else if (rotateZ != 0 && rotateX != 0)
        {
            transformationMatrix.Translate(new Vector3D(x, z, y));
            rotatePoint = new(x, z, y);
            transformationMatrix.RotateAt(new Quaternion(AxisX, rotateX), rotatePoint);
            transformationMatrix.RotateAt(new Quaternion(AxisZ, rotateZ), rotatePoint);
        }
        else if (rotateX == 0 && rotateY != 0 && rotateZ != 0)
        {
            transformationMatrix.Translate(new Vector3D(x, z, y));
            rotatePoint = new(x, z, y);
            transformationMatrix.RotateAt(new Quaternion(AxisY, -rotateZ), rotatePoint);
            transformationMatrix.RotateAt(new Quaternion(AxisZ, -rotateY), rotatePoint);
        }
        else
        {
            transformationMatrix.Translate(new Vector3D(x, z, y));
        }

        model.Transform = new MatrixTransform3D(transformationMatrix);
        return model;
    }

    private GeometryModel3D Create3DModel(NailingData nail, Point3D startpoint = null)
    {
        double x = nail.NX - startpoint.X;
        double y = nail.NY - startpoint.Y;
        double z = nail.NZ - startpoint.Z;
        double length = 200;
        double width = 200;
        double height = 200;

        var meshBuilder = new MeshBuilder(false, false);
        meshBuilder.AddEllipsoid(new Point3D(x, y, z), length, width, height);

        MeshGeometry3D mesh = meshBuilder.ToMesh(true);

        return new GeometryModel3D(mesh, nail.NSaveShot ? greenMaterial : blueMaterial);
    }
}