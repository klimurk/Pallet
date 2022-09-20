using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

/// <summary>
/// The debug converter. Only for development
/// </summary>
internal class DebugConverter : Converter
{
    /// <summary>
    /// Converts the.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>An object.</returns>
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        System.Diagnostics.Debugger.Break();
        return value;
    }
}