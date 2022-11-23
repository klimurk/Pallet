using Pallet.Infrastructure.Converters.Converters.Base;
using Pallet.ViewModels.Base;

namespace Pallet.Infrastructure.Converters.Converters;

/// <summary>
/// The debug converter. Only for development
/// </summary>
[ValueConversion(typeof(ViewModel), typeof(string))]
internal class ViewModelConverter : Converter
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
        return value?.GetType().Name;
    }
}