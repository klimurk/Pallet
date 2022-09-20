using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Infrastructure.Converters.ParametricConverters.Base;
using System.Windows.Markup;

namespace Pallet.Infrastructure.Converters.ParametricConverters;

[ValueConversion(typeof(bool), null)]
internal class NailCounterConverter : ParametricConverter
{
    [ConstructorArgument(nameof(NailsLeftStartBlinking))]
    public string NailsLeftStartBlinking
    {
        get => (string)GetValue(NailsLeftStartBlinkingProperty);
        set => SetValue(NailsLeftStartBlinkingProperty, value);
    }

    private static readonly DependencyProperty NailsLeftStartBlinkingProperty =
        DependencyProperty.Register(
        nameof(NailsLeftStartBlinking),
        typeof(string),
        typeof(NailCounterConverter),
        new PropertyMetadata(""));

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((Nailer)value)?.NailLeftCounter <= int.Parse(NailsLeftStartBlinking);

    protected override Freezable CreateInstanceCore() => new NailCounterConverter { NailsLeftStartBlinking = NailsLeftStartBlinking };
}