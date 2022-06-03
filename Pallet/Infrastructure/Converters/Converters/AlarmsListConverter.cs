using Pallet.Infrastructure.Converters.Converters.Base;
using Pallet.Models;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(AlarmOPC), typeof(bool))]
internal class AlarmsListConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value is not IEnumerable<AlarmOPC>) return false;

        foreach (var alarm in (IEnumerable<AlarmOPC>)value)
        {
            switch (alarm.Value)
            {
                case int intVal:
                    if (intVal >= 1 ^ alarm.Info.Inverted) return true;
                    break;

                case bool boolVal:
                    if (boolVal ^ alarm.Info.Inverted) return true;
                    break;

                default: throw new NotSupportedException(nameof(AlarmsListConverter) + " not support " + alarm.Value.GetType());
            }
        }
        return false;
    }
}