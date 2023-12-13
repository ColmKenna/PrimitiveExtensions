namespace PrimativeExtensions;


public static class ValidationExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this Validation<IEnumerable<T>> validation)
    {
        if (validation.IsFailure)
        {
            yield break;
        }

        foreach (var item in validation.Value)
        {
            yield return item;
        }
    }
    
    public static TR OnSuccess<T,U, TR>(this Validation<T> validation, Func<T, TR> func)
    {
        if (validation.IsFailure)
        {
            throw new InvalidCastException(validation.ErrorMessage);
        }

        return func(validation.Value);
    }
    
    public static void Match<T,U> (this Validation<T> validation, Validation<U> otherValidation, Action<T,U> success, Action<Validation<T>> failure, Action<Validation<U>> failure2)
    {
        if (validation.IsFailure)
        {
            failure(validation);
            return;
        }

        if (otherValidation.IsFailure)
        {
            failure2(otherValidation);
            return;
        }

        success(validation.Value, otherValidation.Value);
    }
    
    public static TR  Match<T,U,TR> (this Validation<T> validation, Validation<U> otherValidation, Func<T,U,TR> success, Func<Validation<T>,TR> failure, Func<Validation<U>,TR> failure2)
    {
        if (validation.IsFailure)
        {
            return failure(validation);
        }

        if (otherValidation.IsFailure)
        {
            return failure2(otherValidation);
        }

        return success(validation.Value, otherValidation.Value);
    }
    
    public static IEnumerable<T> SelectAllValid<T>(this IEnumerable<Validation<T>> validations) => validations.Where(x => x.IsValid).Select(x => x.Value);

    public static IEnumerable<Validation<T>> SelectAllInValid<T>(this IEnumerable<Validation<T>> validations) => validations.Where(x => x.IsFailure);
        
    public static bool AllValid<T>(this IEnumerable<Validation<T>> validations) => validations.All(x => x.IsValid);
    
    public static Validation<(T,U)> ToValidation<T,U>(this (T, Validation<U>) tuple)
    {
        if (tuple.Item2.IsFailure)
        {
            return Validation<(T, U)>.Fail(tuple.Item2.ErrorMessage);
        }

        return Validation<(T, U)>.Success((tuple.Item1, tuple.Item2.Value));
    }
    
}