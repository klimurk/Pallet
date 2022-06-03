using Pallet.Infrastructure.Converters.Converters.Base;
using System.Windows.Media.Imaging;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(string), typeof(BitmapImage))]
internal class ImageConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new BitmapImage(new Uri($"pack://application:,,,/{value}"));
}