using Pallet.Infrastructure.Converters.Converters.Base;
using Pallet.Models;
using System.Windows.Markup;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(IEnumerable<AlarmOPC>), typeof(IEnumerable<AlarmOPC>))]
internal class AlarmsConverter : Converter
{
    [ConstructorArgument("Cutoff")]
    public int Cutoff { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value is not IEnumerable<AlarmOPC>) return false;
        List<AlarmOPC> retList = new();
        foreach (var alarm in (IEnumerable<AlarmOPC>)value)
        {
            switch (alarm.Value)
            {
                case int intVal:
                    if (intVal >= Cutoff ^ alarm.Info.Inverted) retList.Add(alarm);
                    break;

                case bool boolVal:
                    if (boolVal ^ alarm.Info.Inverted) retList.Add(alarm);
                    break;

                default: throw new NotSupportedException(nameof(AlarmsConverter) + " not support " + ((AlarmOPC)value).Value.GetType());
            }
        }

        return retList;
    }
}