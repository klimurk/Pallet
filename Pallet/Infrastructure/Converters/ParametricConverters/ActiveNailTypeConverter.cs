using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Infrastructure.Converters.ParametricConverters.Base;
using System.Windows.Markup;

namespace Pallet.Infrastructure.Converters.ParametricConverters;

[ValueConversion(typeof(bool), null)]
internal class ActiveNailTypeConverter : ParametricConverter
{
    [ConstructorArgument(nameof(ActiveNailType))]
    public string ActiveNailType
    {
        get => (string)GetValue(ActiveNailTypeProperty);
        set => SetValue(ActiveNailTypeProperty, value);
    }

    private static readonly DependencyProperty ActiveNailTypeProperty =
        DependencyProperty.Register(
        nameof(ActiveNailType),
        typeof(string),
        typeof(ActiveNailTypeConverter),
        new PropertyMetadata(""));

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((Nailer)value)?.Name == ActiveNailType;

    protected override Freezable CreateInstanceCore() => new ActiveNailTypeConverter { ActiveNailType = ActiveNailType };
}