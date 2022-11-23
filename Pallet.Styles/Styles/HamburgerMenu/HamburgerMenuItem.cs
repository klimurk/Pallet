using System.Windows;
using System.Windows.Controls;

namespace Pallet.Styles.Styles.HamburgerMenu;

public class HamburgerMenuItem : RadioButton
{
    static HamburgerMenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenuItem), new FrameworkPropertyMetadata(typeof(HamburgerMenuItem)));
    }
}