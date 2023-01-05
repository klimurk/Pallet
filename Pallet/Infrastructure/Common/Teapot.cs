using Pallet.Infrastructure.Common;
using System.Windows.Media.Media3D;

namespace HelixToolkit.Wpf
{
    public partial class Teapot
    {
        private readonly ToolTipHelper _tooltipHelper;  // keep the ToolTipHelper during the life of your Teapot but replace the Content whenever you want

        private ModelUIElement3D _uiModel; // this has to be created and have its Model set to the suitable GeometryModel3D. You may want to replace an existing ModelVisual3D by this.

        public Teapot(/*...*/)
        {
            _tooltipHelper = new ToolTipHelper { ToolTipContent = "MyToolTip" };
            _uiModel.MouseEnter += _tooltipHelper.OnMouseEnter;
            _uiModel.MouseLeave += _tooltipHelper.OnMouseLeave;

            //...
        }
    }
}