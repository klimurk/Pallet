namespace Pallet.Extensions;

public static class IListExtensioncs
{
    public static IEnumerable<T> Shuffle<T>(this IList<T> list, Random rnd)
    {
        for (var i = list.Count; i > 0; i--)
            list.Swap(0, rnd.Next(0, i));
        return list;
    }
    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
