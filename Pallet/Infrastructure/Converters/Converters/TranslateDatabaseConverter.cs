using Pallet.Database.Entities.Base.Interfaces;
using Pallet.Infrastructure.Converters.Converters.Base;

namespace Pallet.Infrastructure.Converters.Converters;

[ValueConversion(typeof(object), typeof(string))]
internal class TranslateDatabaseConverter : Converter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IDBTranslateble item) return null;
        switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
        {
            case "en":
                return item.DescriptionEn;

            case "de":
                return item.DescriptionDe;

            default:
                return item.DescriptionLocal;
        }
    }
}