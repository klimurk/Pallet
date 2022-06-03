﻿using Pallet.Infrastructure.Converters.MultiConverters.Base;

namespace Pallet.Infrastructure.Converters.MultiConverters;

internal class UserPasswordCommandConverter : MultiConverter
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => new Tuple<object, object>(values[0], values[1]);
}