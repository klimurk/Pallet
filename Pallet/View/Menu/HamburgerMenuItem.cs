using System.Windows.Controls;

namespace Pallet.View.Menu;

public class HamburgerMenuItem : RadioButton
{
    static HamburgerMenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenuItem), new FrameworkPropertyMetadata(typeof(HamburgerMenuItem)));
    }
}