using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Pallet.View.Menu;

public class HamburgerMenu : Control
{
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register("IsOpen", typeof(bool), typeof(HamburgerMenu),
            new PropertyMetadata(false, OnIsOpenPropertyChanged));

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public static readonly DependencyProperty OpenCloseDurationProperty =
        DependencyProperty.Register("OpenCloseDuration", typeof(Duration), typeof(HamburgerMenu),
            new PropertyMetadata(Duration.Automatic));

    public Duration OpenCloseDuration
    {
        get => (Duration)GetValue(OpenCloseDurationProperty);
        set => SetValue(OpenCloseDurationProperty, value);
    }

    public static readonly DependencyProperty FallbackOpenWidthProperty =
        DependencyProperty.Register("FallbackOpenWidth", typeof(double), typeof(HamburgerMenu),
            new PropertyMetadata(100.0));

    public double FallbackOpenWidth
    {
        get => (double)GetValue(FallbackOpenWidthProperty);
        set => SetValue(FallbackOpenWidthProperty, value);
    }

    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(FrameworkElement), typeof(HamburgerMenu),
            new PropertyMetadata(null));

    public FrameworkElement Content
    {
        get => (FrameworkElement)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    static HamburgerMenu() => DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenu), new FrameworkPropertyMetadata(typeof(HamburgerMenu)));

    public HamburgerMenu() => Width = 0;

    private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is HamburgerMenu hamburgerMenu)
        {
            hamburgerMenu.OnIsOpenPropertyChanged();
        }
    }

    private void OnIsOpenPropertyChanged()
    {
        if (IsOpen)
        {
            OpenMenuAnimated();
        }
        else
        {
            CloseMenuAnimated();
        }
    }

    private void OpenMenuAnimated() => BeginAnimation(WidthProperty, new DoubleAnimation(GetDesiredContentWidth(), OpenCloseDuration));

    private void CloseMenuAnimated() => BeginAnimation(WidthProperty, new DoubleAnimation(0, OpenCloseDuration));

    private double GetDesiredContentWidth()
    {
        if (Content == null) return FallbackOpenWidth;

        Content.Measure(new Size(MaxWidth, MaxHeight));

        return Content.DesiredSize.Width;
    }
}