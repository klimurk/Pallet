using Pallet.Infrastructure.Converters.Converters.Base;
using Pallet.ViewModels.SubView;

namespace Pallet.Infrastructure.Converters.Converters;

internal class ActualWidthBackConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var PalletViewModel = App.Host.Services.GetService(typeof(PalletViewModel)) as PalletViewModel;

        PalletViewModel.CanvasWidth = (double)value;
        return value;
    }
}