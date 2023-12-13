using System.Collections;

namespace PrimativeExtensions;

public class Either<T, U>  
{
    private readonly T _left;
    private readonly U _right;
    private readonly bool _isLeft;

    public Either(T left)
    {
        _left = left;
        _isLeft = true;
    }

    public Either(U right)
    {
        _right = right;
        _isLeft = false;
    }

    public T Left
    {
        get
        {
            if (_isLeft) return _left;
            throw new InvalidOperationException("Either contains a Right value");
        }
    }

    public U Right
    {
        get
        {
            if (!_isLeft) return _right;
            throw new InvalidOperationException("Either contains a Left value");
        }
    }
    
    public static implicit operator Either<T, U>(T left) => new Either<T, U>(left);
    public static implicit operator Either<T, U>(U right) => new Either<T, U>(right);

    

    public bool IsLeft => _isLeft;
    public bool IsRight => !_isLeft;
    
    public R Match<R>( Func<T, R> left, Func<U, R> right)
    {
        if (IsLeft) return left(Left);
        return right(Right);
    }
    
    public void Match( Action<T> left, Action<U> right)
    {
        if (IsLeft) 
            left(Left);
        right(Right);
    }
    
    public IEnumerable<R> AsEnumerable<R>() where R: T,U
    {
        if(IsLeft && Left is R)
            yield return (R)Left;
        if(IsRight && Right is R)
            yield return (R)Right;
    }


}

public class Either<T1, T2, T3>
{
    private readonly T1 type1;
    private readonly T2 type2;
    private readonly T3 type3;
    private readonly int _state;

    public Either(T1 value1)
    {
        type1 = value1;
        _state = 1;
    }

    public Either(T2 value2)
    {
        type2 = value2;
        _state = 2;
    }

    public Either(T3 value3)
    {
        type3 = value3;
        _state = 3;
    }

    public T1 Type1
    {
        get
        {
            if (_state == 1) return type1;
            throw new InvalidOperationException("Either does not contain a Type1 value");
        }
    }

    public T2 Type2
    {
        get
        {
            if (_state == 2) return type2;
            throw new InvalidOperationException("Either does not contain a Type2 value");
        }
    }

    public T3 Type3
    {
        get
        {
            if (_state == 3) return type3;
            throw new InvalidOperationException("Either does not contain a Type3 value");
        }
    }

    public static implicit operator Either<T1, T2, T3>(T1 value1) => new Either<T1, T2, T3>(value1);
    public static implicit operator Either<T1, T2, T3>(T2 value2) => new Either<T1, T2, T3>(value2);
    public static implicit operator Either<T1, T2, T3>(T3 value3) => new Either<T1, T2, T3>(value3);

    public int State => _state;

    public R Match<R>(Func<T1, R> match1, Func<T2, R> match2, Func<T3, R> match3)
    {
        switch (_state)
        {
            case 1: return match1(type1);
            case 2: return match2(type2);
            case 3: return match3(type3);
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }

    public void Match(Action<T1> act1, Action<T2> act2, Action<T3> act3)
    {
        switch (_state)
        {
            case 1:
                act1(type1);
                break;
            case 2:
                act2(type2);
                break;
            case 3:
                act3(type3);
                break;
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }
    
    public IEnumerable<T> AsEnumerable<T>() where T: T1,T2,T3
    {
        if(_state == 1 && type1 is T t1)
            yield return t1;
        if (_state == 2 && type2 is T t2)
            yield return t2;
        if (_state == 3 && type3 is T t3)
            yield return t3;
    }
    
}

public class Either<T1, T2, T3, T4>
{
    private readonly T1 type1;
    private readonly T2 type2;
    private readonly T3 type3;
    private readonly T4 type4;
    private readonly int _state;

    public Either(T1 value1)
    {
        type1 = value1;
        _state = 1;
    }

    public Either(T2 value2)
    {
        type2 = value2;
        _state = 2;
    }

    public Either(T3 value3)
    {
        type3 = value3;
        _state = 3;
    }

    public Either(T4 value4)
    {
        type4 = value4;
        _state = 4;
    }

    public T1 Type1
    {
        get
        {
            if (_state == 1) return type1;
            throw new InvalidOperationException("Either does not contain a Type1 value");
        }
    }

    public T2 Type2
    {
        get
        {
            if (_state == 2) return type2;
            throw new InvalidOperationException("Either does not contain a Type2 value");
        }
    }

    public T3 Type3
    {
        get
        {
            if (_state == 3) return type3;
            throw new InvalidOperationException("Either does not contain a Type3 value");
        }
    }

    public T4 Type4
    {
        get
        {
            if (_state == 4) return type4;
            throw new InvalidOperationException("Either does not contain a Type4 value");
        }
    }

    public static implicit operator Either<T1, T2, T3, T4>(T1 value1) => new Either<T1, T2, T3, T4>(value1);
    public static implicit operator Either<T1, T2, T3, T4>(T2 value2) => new Either<T1, T2, T3, T4>(value2);
    public static implicit operator Either<T1, T2, T3, T4>(T3 value3) => new Either<T1, T2, T3, T4>(value3);
    public static implicit operator Either<T1, T2, T3, T4>(T4 value4) => new Either<T1, T2, T3, T4>(value4);

    public int State => _state;

    public R Match<R>(Func<T1, R> match1, Func<T2, R> match2, Func<T3, R> match3, Func<T4, R> match4)
    {
        switch (_state)
        {
            case 1: return match1(type1);
            case 2: return match2(type2);
            case 3: return match3(type3);
            case 4: return match4(type4);
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }

    public void Match(Action<T1> act1, Action<T2> act2, Action<T3> act3, Action<T4> act4)
    {
        switch (_state)
        {
            case 1:
                act1(type1);
                break;
            case 2:
                act2(type2);
                break;
            case 3:
                act3(type3);
                break;
            case 4:
                act4(type4);
                break;
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }
    
    public IEnumerable<R> Enumerable<R>() where R: T1,T2,T3,T4
    {
        if(_state == 1 && type1 is R r1)
            yield return r1;
        if (_state == 2 && type2 is R r2)
            yield return r2;
        if (_state == 3 && type3 is R r3)
            yield return r3;
        if (_state == 4 && type4 is R r4)
            yield return r4;
    }
        
}

public class Either<T1, T2, T3, T4, T5>
{
    private readonly T1 type1;
    private readonly T2 type2;
    private readonly T3 type3;
    private readonly T4 type4;
    private readonly T5 type5;
    private readonly int _state;

    public Either(T1 value1)
    {
        type1 = value1;
        _state = 1;
    }

    public Either(T2 value2)
    {
        type2 = value2;
        _state = 2;
    }

    public Either(T3 value3)
    {
        type3 = value3;
        _state = 3;
    }
    
    public Either(T4 value4)
    {
        type4 = value4;
        _state = 4;
    }
    
    public Either(T5 value5)
    {
        type5 = value5;
        _state = 5;
    }
    
    public T1 Type1
    {
        get
        {
            if (_state == 1) return type1;
            throw new InvalidOperationException("Either does not contain a Type1 value");
        }
    }

    public T2 Type2
    {
        get
        {
            if (_state == 2) return type2;
            throw new InvalidOperationException("Either does not contain a Type2 value");
        }
    }

    public T3 Type3
    {
        get
        {
            if (_state == 3) return type3;
            throw new InvalidOperationException("Either does not contain a Type3 value");
        }
    }
    
    public T4 Type4
    {
        get
        {
            if (_state == 4) return type4;
            throw new InvalidOperationException("Either does not contain a Type4 value");
        }
    }
    
    public T5 Type5
    {
        get
        {
            if (_state == 5) return type5;
            throw new InvalidOperationException("Either does not contain a Type5 value");
        }
    }
    
    public static implicit operator Either<T1, T2, T3, T4, T5>(T1 value1) => new Either<T1, T2, T3, T4, T5>(value1);
    public static implicit operator Either<T1, T2, T3, T4, T5>(T2 value2) => new Either<T1, T2, T3, T4, T5>(value2);
    public static implicit operator Either<T1, T2, T3, T4, T5>(T3 value3) => new Either<T1, T2, T3, T4, T5>(value3);
    public static implicit operator Either<T1, T2, T3, T4, T5>(T4 value4) => new Either<T1, T2, T3, T4, T5>(value4);
    public static implicit operator Either<T1, T2, T3, T4, T5>(T5 value5) => new Either<T1, T2, T3, T4, T5>(value5);

    public int State => _state;

    public R Match<R>(Func<T1, R> match1, Func<T2, R> match2, Func<T3, R> match3, Func<T4, R> match4, Func<T5, R> match5)
    {
        switch (_state)
        {
            case 1: return match1(type1);
            case 2: return match2(type2);
            case 3: return match3(type3);
            case 4: return match4(type4);
            case 5: return match5(type5);
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }

    public void Match(Action<T1> act1, Action<T2> act2, Action<T3> act3, Action<T4> act4, Action<T5> act5)
    {
        switch (_state)
        {
            case 1:
                act1(type1);
                break;
            case 2:
                act2(type2);
                break;
            case 3:
                act3(type3);
                break;
            case 4:
                act4(type4);
                break;
            case 5:
                act5(type5);
                break;
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }
    
    public IEnumerable<R> Enumerable<R>() where R: T1,T2,T3,T4,T5
    {
        if(_state == 1 && type1 is R r1)
            yield return r1;
        if (_state == 2 && type2 is R r2)
            yield return r2;
        if (_state == 3 && type3 is R r3)
            yield return r3;
        if (_state == 4 && type4 is R r4)
            yield return r4;
        if (_state == 5 && type5 is R r5)
            yield return r5;
    }
}

public class Either<T1, T2, T3, T4, T5, T6>
{
    private readonly T1 type1;
    private readonly T2 type2;
    private readonly T3 type3;
    private readonly T4 type4;
    private readonly T5 type5;
    private readonly T6 type6;
    private readonly int _state;

    public Either(T1 value1)
    {
        type1 = value1;
        _state = 1;
    }

    public Either(T2 value2)
    {
        type2 = value2;
        _state = 2;
    }

    public Either(T3 value3)
    {
        type3 = value3;
        _state = 3;
    }

    public Either(T4 value4)
    {
        type4 = value4;
        _state = 4;
    }

    public Either(T5 value5)
    {
        type5 = value5;
        _state = 5;
    }

    public Either(T6 value6)
    {
        type6 = value6;
        _state = 6;
    }
    
    public bool IsType1() => _state == 1;
    public bool IsType2() => _state == 2;
    public bool IsType3() => _state == 3;
    public bool IsType4() => _state == 4;
    public bool IsType5() => _state == 5;
    public bool IsType6() => _state == 6;

    public T1 Type1
    {
        get
        {
            if (_state == 1) return type1;
            throw new InvalidOperationException("Either does not contain a Type1 value");
        }
    }

    public T2 Type2
    {
        get
        {
            if (_state == 2) return type2;
            throw new InvalidOperationException("Either does not contain a Type2 value");
        }
    }

    public T3 Type3
    {
        get
        {
            if (_state == 3) return type3;
            throw new InvalidOperationException("Either does not contain a Type3 value");
        }
    }

    public T4 Type4
    {
        get
        {
            if (_state == 4) return type4;
            throw new InvalidOperationException("Either does not contain a Type4 value");
        }
    }

    public T5 Type5
    {
        get
        {
            if (_state == 5) return type5;
            throw new InvalidOperationException("Either does not contain a Type5 value");
        }
    }

    public T6 Type6
    {
        get
        {
            if (_state == 6) return type6;
            throw new InvalidOperationException("Either does not contain a Type6 value");
        }
    }

    public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T1 value1) => new Either<T1, T2, T3, T4, T5, T6>(value1);
    public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T2 value2) => new Either<T1, T2, T3, T4, T5, T6>(value2);
    public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T3 value3) => new Either<T1, T2, T3, T4, T5, T6>(value3);
    public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T4 value4) => new Either<T1, T2, T3, T4, T5, T6>(value4);
    public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T5 value5) => new Either<T1, T2, T3, T4, T5, T6>(value5);
    public static implicit operator Either<T1, T2, T3, T4, T5, T6>(T6 value6) => new Either<T1, T2, T3, T4, T5, T6>(value6);

    public static implicit operator Maybe<T1> (Either<T1, T2, T3, T4, T5, T6> either)
    {
        if (either._state == 1) return either.Type1;
        return Maybe<T1>.None;
    }
    
    public static implicit operator Maybe<T2> (Either<T1, T2, T3, T4, T5, T6> either)
    {
        if (either._state == 2) return either.Type2;
        return Maybe<T2>.None;
    }
    
    public static implicit operator Maybe<T3> (Either<T1, T2, T3, T4, T5, T6> either)
    {
        if (either._state == 3) return either.Type3;
        return Maybe<T3>.None;
    }
    
    public static implicit operator Maybe<T4> (Either<T1, T2, T3, T4, T5, T6> either)
    {
        if (either._state == 4) return either.Type4;
        return Maybe<T4>.None;
    }
    
    public static implicit operator Maybe<T5> (Either<T1, T2, T3, T4, T5, T6> either)
    {
        if (either._state == 5) return either.Type5;
        return Maybe<T5>.None;
    }
    
    public static implicit operator Maybe<T6> (Either<T1, T2, T3, T4, T5, T6> either)
    {
        if (either._state == 6) return either.Type6;
        return Maybe<T6>.None;
    }
    
    
    
    
    public R Match<R>(Func<T1, R> match1, Func<T2, R> match2, Func<T3, R> match3, Func<T4, R> match4, Func<T5, R> match5, Func<T6, R> match6)
    {
        switch (_state)
        {
            case 1: return match1(type1);
            case 2: return match2(type2);
            case 3: return match3(type3);
            case 4: return match4(type4);
            case 5: return match5(type5);
            case 6: return match6(type6);
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }

    public void RunOn(Action<T1> act1, Action<T2> act2, Action<T3> act3, Action<T4> act4, Action<T5> act5, Action<T6> act6)
    {
        switch (_state)
        {
            case 1:
                act1(type1);
                break;
            case 2:
                act2(type2);
                break;
            case 3:
                act3(type3);
                break;
            case 4:
                act4(type4);
                break;
            case 5:
                act5(type5);
                break;
            case 6:
                act6(type6);
                break;
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }
    

    public Type GetEitherType()
    {
        switch (_state)
        {
            case 1: return typeof(T1);
            case 2: return typeof(T2);
            case 3: return typeof(T3);
            case 4: return typeof(T4);
            case 5: return typeof(T5);
            case 6: return typeof(T6);
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }

    public override string ToString()
    {
        switch (_state)
        {
            case 1: return type1.ToString();
            case 2: return type2.ToString();
            case 3: return type3.ToString();
            case 4: return type4.ToString();
            case 5: return type5.ToString();
            case 6: return type6.ToString();
            default:
                throw new InvalidOperationException("Invalid state value");
        }
    }
    
    public IEnumerable<R> Enumerable<R>() where R: T1,T2,T3,T4,T5,T6
    {
        if(_state == 1 && type1 is R r1)
            yield return r1;
        if (_state == 2 && type2 is R r2)
            yield return r2;
        if (_state == 3 && type3 is R r3)
            yield return r3;
        if (_state == 4 && type4 is R r4)
            yield return r4;
        if (_state == 5 && type5 is R r5)
            yield return r5;
        if (_state == 6 && type6 is R r6)
            yield return r6;
    }
}
public static class EitherExtensions
{
    
    public static Either<List<T>, CircularReferenceResult<T>> ToEither<T>(
        this IEnumerable<Either<T, CircularReferenceResult<T>>> source)
    {
        var list = new List<T>();
        if (source.Empty()) return list;
        if (source.Any(x => x.IsRight))
        {
            return source.First(x => x.IsRight).Right;
        }

        return source.Select(x => x.Left).ToList();
    }



    
    
    
    
    
}