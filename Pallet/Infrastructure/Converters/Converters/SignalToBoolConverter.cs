using Pallet.InternalDatabase.Entities.OPC;
using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(Signal), typeof(bool))]
internal class SignalToBoolConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value;
}