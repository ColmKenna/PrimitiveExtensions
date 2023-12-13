using System.Collections;

namespace PrimativeExtensions;

public class Maybe<T> :IEnumerable<T>
{
    private readonly T _value;
    public T Value
    {
        get
        {
            if (HasValue) return _value;
            throw new InvalidOperationException("No value exists.");
        }
    }
    public bool HasValue { get; }

    public Maybe(T value)
    {
        _value = value;
        HasValue = true;
    }
    
    public Maybe<R> Select<R>(Func<T, R> selector) 
        => HasValue ? new Maybe<R>(selector(_value)) : new Maybe<R>();

    public Task<Maybe<R>> Select<R>(Func<T, Task<R>> selector)
        => HasValue ? 
            selector(_value).ContinueWith(x => new Maybe<R>(x.Result)) :
            Task.FromResult(new Maybe<R>());

    // public  bool Any() => HasValue;
    // public  bool Any(Func<T, bool> predicate) => HasValue && predicate(Value);
    public  Maybe<R> SelectMany<R>( Func<T, Maybe<R>> selector)
        => HasValue ? selector(Value) : new Maybe<R>();

    public IEnumerable<T> Where(Func<T, bool> predicate)
    {
        if (HasValue && predicate(Value))
        {
            yield return Value;
        }
    }
    
    public TR Match<TR>(Func<T, TR> some, Func<TR> none)
        => HasValue ? some(Value) : none();


    public Maybe()
    {
        _value = default(T);
        HasValue = false;
    }
    
    
    public static Maybe<T> None = new Maybe<T>();
    public static Maybe<T> Something(T value) => new Maybe<T>(value);
    
    public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);


    public IEnumerator<T> GetEnumerator()
    {
        if (HasValue)
        {
            yield return _value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class MaybeExtension
{

    public static Maybe<T> FirstOrMaybe<T>(this IEnumerable<T> source)
        => source.Any() ? new Maybe<T>(source.First()) : new Maybe<T>();

    public static Maybe<T> FirstOrMaybe<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source.Any(predicate) ? new Maybe<T>(source.First(predicate)) : new Maybe<T>();
    
    
}


