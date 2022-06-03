namespace Pallet.Infrastructure.Common;

public class DataTriggerAssists
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.RegisterAttached(
            "Value",
            typeof(object),
            typeof(DataTriggerAssists),
            new FrameworkPropertyMetadata(null, OnValueChanged));

    public static object GetValue(DependencyObject d)
    {
        return d.GetValue(ValueProperty);
    }

    public static void SetValue(DependencyObject d, object value)
    {
        d.SetValue(ValueProperty, value);
    }

    public static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
    {
        if (d is DataTrigger trigger)
            trigger.Value = args.NewValue;
    }
}