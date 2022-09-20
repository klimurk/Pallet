using Pallet.Database.Entities.OPC;
using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(Alarm), typeof(bool))]
internal class AlarmsListConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value is not IEnumerable<Alarm>) return false;

        foreach (var alarm in (IEnumerable<Alarm>)value)
        {
            switch (alarm.Value)
            {
                case int intVal:
                    if (intVal >= 1 ^ alarm.Inverted) return true;
                    break;

                case bool boolVal:
                    if (boolVal ^ alarm.Inverted) return true;
                    break;

                default: throw new NotSupportedException(nameof(AlarmsListConverter) + " not support " + alarm.Value.GetType());
            }
        }
        return false;
    }
}