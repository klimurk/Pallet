namespace Pallet.Infrastructure.Converters.ParametricConverters.Base;

public abstract class ParametricConverter : Freezable, IValueConverter
{
    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}