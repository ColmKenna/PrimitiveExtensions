namespace PrimativeExtensions;

public class CircularReferenceResult<T>
{
    public bool IsSuccessful { get; private set; }
    public T CircularReference { get; private set; }

    private CircularReferenceResult() { }

    public static CircularReferenceResult<T> Success()
    {
        return new CircularReferenceResult<T>
        {
            IsSuccessful = true
        };
    }

    public static CircularReferenceResult<T> Failure(T circularReference)
    {
        return new CircularReferenceResult<T>
        {
            IsSuccessful = false,
            CircularReference = circularReference
        };
    }
}

public static class CircularReferenceResultExtension
{
    
    
    public static Either<TResult,  CircularReferenceResult<T>>  Max<T,TResult>(this IEnumerable<Either<T,  CircularReferenceResult<T>>> either, Func<T,TResult> maxFunc)
    {

        var currentMax = default(TResult);
        foreach (var aa in either)
        {
            if (aa.IsRight)
            {
                return aa.Right;
            }

            if (aa.IsLeft)
            {
                currentMax = currentMax.Concat(maxFunc(aa.Left)).Max();
            }
            
        }

        return currentMax;

    }
    




    

}