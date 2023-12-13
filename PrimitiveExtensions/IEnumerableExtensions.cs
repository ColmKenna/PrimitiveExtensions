namespace PrimativeExtensions;

public static class IEnumerableExtensions
{
    
    public static bool Empty<T>(this IEnumerable<T> source) => !source.Any();
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
    {
        foreach (var i in source)
        {
            yield return i;
        }
        yield return item;
    }
    
    public static IEnumerable<T> Concat<T>(this T source, T item)
    {
        yield return source;
        yield return item;
    }
    
    public static IEnumerable<T> Concat<T>(this T source, params T[] items)
    {
        yield return source;
        foreach (T item in items)
        {
            yield return item;
        }
    }

    public static bool ContainedIn<T>(this T item, IEnumerable<T> source) => source.Contains(item);

    public static bool ContainedIn<T>(this T item, params T[] source) => source.Contains(item);

    public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> items) => items.All(source.Contains);
    public static bool ContainsAll<T>(this IEnumerable<T> source, params T[] items) => source.ContainsAll(items.AsEnumerable());  

    public static bool ContainsSameItems<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        return first.OrderBy(t => t).SequenceEqual(second.OrderBy(t => t));
    }
    
    public static bool IsComposedOf<T>(this IEnumerable<T> first, params T[] second)
    {
        return first.ContainsSameItems(second.AsEnumerable());
    }
    
    public static bool ContainsSameItemsInOrder<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        return first.SequenceEqual(second);
    }

    public static bool IsComposedOfInOrder<T>(this IEnumerable<T> first, params T[] second)
    {
        return first.ContainsSameItemsInOrder(second.AsEnumerable());
    }
    
    public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter = ", ") => string.Join(delimiter, source);
    
}