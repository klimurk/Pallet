﻿using System.Collections.ObjectModel;

namespace Pallet.Extensions
{


public static class ObservableCollectionExtensions
{
    public static void AddClear<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
    {
        collection.Clear();
        collection.Add(items);
    }

    public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
            collection.Add(item);
    }
}
}