using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Infrastructure.Converters.ParametricConverters.Base;
using System.Windows.Markup;

namespace Pallet.Infrastructure.Converters.ParametricConverters;

[ValueConversion(typeof(bool), null)]
internal class ActiveProfileConverter : ParametricConverter
{
    [ConstructorArgument("ActiveProfile")]
    public string ActiveProfile
    {
        get => (string)GetValue(ActiveProfileProperty);
        set => SetValue(ActiveProfileProperty, value);
    }

    private static readonly DependencyProperty ActiveProfileProperty =
        DependencyProperty.Register(
        nameof(ActiveProfile),
        typeof(string),
        typeof(ActiveProfileConverter),
        new PropertyMetadata(""));

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((Profile)value).Name == ActiveProfile;

    protected override Freezable CreateInstanceCore() => new ActiveProfileConverter { ActiveProfile = ActiveProfile };
}