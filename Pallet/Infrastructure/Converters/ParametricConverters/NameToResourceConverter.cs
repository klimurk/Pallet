using Pallet.Infrastructure.Converters.ParametricConverters.Base;
using Pallet.Services.Managers.Interfaces;
using System.Windows.Markup;

namespace Pallet.Infrastructure.Converters.ParametricConverters;

[ValueConversion(typeof(string), typeof(string))]
internal class NameToResourceConverter : ParametricConverter
{
    [ConstructorArgument(nameof(ResourceName))]
    public string ResourceName
    {
        get => (string)GetValue(ResourceNameProperty);
        set => SetValue(ResourceNameProperty, value);
    }

    private static readonly DependencyProperty ResourceNameProperty =
        DependencyProperty.Register(
        nameof(ResourceName),
        typeof(string),
        typeof(ActiveNailTypeConverter),
        new PropertyMetadata(""));

    private IManagerLanguage _ManagerLanguage;

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (_ManagerLanguage is null)
        {
            _ManagerLanguage = App.Services.GetService(typeof(IManagerLanguage)) as IManagerLanguage;
            _ManagerLanguage.ManageNewResource(ResourceName);
        }

        return _ManagerLanguage.ReadString(ResourceName, value as string);
    }

    protected override Freezable CreateInstanceCore() => new NameToResourceConverter { ResourceName = ResourceName };
}