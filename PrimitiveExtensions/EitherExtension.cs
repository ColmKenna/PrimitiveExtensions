namespace PrimativeExtensions;

public static class EitherExtension
{
    public static void Match<T,U>(this IEnumerable<Either<T,U>> either, 
        Action<T> left, Action<U> right)
    {
        foreach (var item in either)
        {
            item.Match(left,right);
        }
    }

    public static void MatchAll<T,U>(this IEnumerable<Either<T,U>> either, 
        Action<IList<T>> left, Action<U> right)
    {
        var list = new List<T>();
        foreach (var item in either)
        {
            item.Match(x => list.Add(x), right);
            if(item.IsRight) return;
        }
        left(list);
    }

    
    public static IEnumerable<R> Match<T,U,R>(this IEnumerable<Either<T,U>> either, 
        Func<T,R> left, Func<U,R> right)
    {
        foreach (var item in either)
        {
            yield return item.Match(left,right);
        }
    }
    
    
    public static void Match<T,U>(this Either<IEnumerable<T>, U> either, 
        Action<IEnumerable<T>> left, Action<U> right)
    {
        either.Match(left,right  );
    }
    
    
    
}