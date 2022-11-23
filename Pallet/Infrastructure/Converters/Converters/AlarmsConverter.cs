using Pallet.InternalDatabase.Entities.OPC;
using Pallet.Infrastructure.Converters.Converters.Base;
using System.Windows.Markup;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(IEnumerable<Alarm>), typeof(IEnumerable<Alarm>))]
internal class AlarmsConverter : Converter
{
    [ConstructorArgument("Cutoff")]
    public int Cutoff { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value is not IEnumerable<Alarm>) return false;
        List<Alarm> retList = new();
        foreach (var alarm in (IEnumerable<Alarm>)value)
        {
            switch (alarm.Value)
            {
                case int intVal:
                    if (intVal >= Cutoff ^ alarm.Inverted) retList.Add(alarm);
                    break;

                case bool boolVal:
                    if (boolVal ^ alarm.Inverted) retList.Add(alarm);
                    break;

                default: throw new NotSupportedException(nameof(AlarmsConverter) + " not support " + ((Alarm)value).Value.GetType());
            }
        }

        return retList;
    }
}