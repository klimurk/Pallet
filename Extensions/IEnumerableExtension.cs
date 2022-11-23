namespace Pallet.Extensions;

public static class IEnumerableExtension
{
    public static IEnumerable<T> DeepWhere<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        foreach (T item in items)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static int IndexOf<T>(this IEnumerable<T> obj, T value)
    {
        return obj.IndexOf(value, null);
    }

    public static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
    {
        comparer = comparer ?? EqualityComparer<T>.Default;
        var found = obj
            .Select((a, i) => new { a, i })
            .FirstOrDefault(x => comparer.Equals(x.a, value));
        return found == null ? -1 : found.i;
    }
}
