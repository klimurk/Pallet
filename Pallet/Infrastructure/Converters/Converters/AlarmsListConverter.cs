using Pallet.Infrastructure.Converters.Converters.Base;
using Pallet.Models;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(AlarmOpc), typeof(bool))]
internal class AlarmsListConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value is not IEnumerable<AlarmOpc>) return false;

        foreach (var alarm in (IEnumerable<AlarmOpc>)value)
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