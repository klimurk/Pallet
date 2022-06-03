using Pallet.Entities.Models;
using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(SignalOPC), typeof(bool))]
internal class SignalToBoolConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value;
}