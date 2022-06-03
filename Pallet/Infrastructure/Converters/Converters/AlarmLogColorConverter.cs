using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(int), null)]
internal class AlarmLogColorConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string alarm) return null;
        if (alarm == "SI0") return 3;
        if (alarm[..1] == "S") return 2;
        if (alarm[..1] == "M") return 1;
        return 0;
    }
}