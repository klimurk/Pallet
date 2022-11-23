using Pallet.Infrastructure.Converters.Converters.Base;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(int), typeof(string))]
internal class RoleNumConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Enum.GetName(typeof(IManagerUser.UserRoleNum), (int)value);
    }
}