using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

/// <summary>
/// The alarm log color converter.
/// </summary>
[ValueConversion(typeof(int), null)]
internal class AlarmLogColorConverter : Converter
{
    /// <summary>
    /// Converts the.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Color number </returns>
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string alarm) return "";
        if (alarm == "SI0") return 3;
        if (alarm[..1] == "S") return 2;
        if (alarm[..1] == "M") return 1;
        return 0;
    }
}