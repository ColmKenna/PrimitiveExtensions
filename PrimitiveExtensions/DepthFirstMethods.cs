namespace PrimativeExtensions;

public static class DepthFirstMethods
{
    public static Either<List<T>, CircularReferenceResult<T>> ToList<T>(
        T root,
        Func<T, IEnumerable<T>> children)
    {
        var list = new List<T>();
        Action<T> action = x => list.Add(x);
        var result = DepthFirst(root, children, action);
        return result.IsSuccessful ? list : result;
    }

    public static Either<CircularReferenceResult<T>, IList<T>> ToListWhere<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Func<T, bool> predicate)
    {
        var list = new List<T>();
        Action<T> action = x =>
        {
            if (predicate(x))
            {
                list.Add(x);
            }
        };
        var result = DepthFirst(root, children, action);
        return result.IsSuccessful ? list : result;
    }


    public static CircularReferenceResult<T> DepthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Action<T> action)
    {
        return DepthFirst(root, children, action, x => false);
    }

    public static CircularReferenceResult<T> DepthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Action<T> action,
        Func<T, bool> stopCondition)
    {
        var nodeStack = new Stack<T>();
        var pathStack = new Stack<T>();
        nodeStack.Push(root);

        while (nodeStack.Count > 0)
        {
            var currentNode = nodeStack.Pop();
            action(currentNode);
            if (stopCondition(currentNode))
            {
                return CircularReferenceResult<T>.Success();
            }

            pathStack.Push(currentNode);
            foreach (var child in children(currentNode).Reverse())
            {
                if (pathStack.Contains(child))
                {
                    return CircularReferenceResult<T>.Failure(child); // Circular reference detected
                }

                nodeStack.Push(child);
            }

            if (!children(currentNode).Any())
            {
                pathStack.Pop();
                if (pathStack.Any())
                {
                    var a = pathStack.Peek();
                    if (nodeStack.Any())
                    {
                        var b = nodeStack.Peek();
                        while (!children(a).Contains(b) && pathStack.Any())
                        {
                            a = pathStack.Pop();
                        }
                    }
                }
            }
        }

        return CircularReferenceResult<T>.Success();
    }

    public static Either<CircularReferenceResult<T>, Maybe<T>> SearchForFirstItemDepthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Func<T, bool> predicate)
    {
        var list = new List<T>();
        Action<T> action = x =>
        {
            if (predicate(x))
            {
                list.Add(x);
            }
        };
        Func<T, bool> stopCondition = x =>
        {
            if (predicate(x))
            {
                return true;
            }

            return false;
        };
        var result = DepthFirst(root, children, action, stopCondition);

        return result.IsSuccessful
            ? list.Any() ? new Maybe<T>(list[0]) : Maybe<T>.None
            : result;
    }

    //Struct can be simplified as there are no circular references
    public static IEnumerable<Either<T, CircularReferenceResult<T>>> ToIEnumerable<T>(
        T root,
        Func<T, IEnumerable<T>> children)
    {
        return ToIEnumerable(root, children, x => false);
    }

    public static Either<Maybe<T>, CircularReferenceResult<T>> First<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Func<T, bool> predicate)
    {
        foreach (var item in ToIEnumerable(root, children))
        {
            if (item.IsRight)
            {
                var right =    item.Right ;
                if (right.IsSuccessful)
                {
                    return Maybe<T>.None;
                }
                return right;
            }

            if (item.IsLeft && predicate(item.Left))
            {
                 return Maybe<T>.Something( item.Left);
            }
        }
        return Maybe<T>.None;
    }

    public static Either<bool, CircularReferenceResult<T>> Any<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Func<T, bool> predicate)
    {
        var result = First(root, children, predicate);
        return result.IsLeft ? result.Left.HasValue : result.Right.IsSuccessful;
    }


    public static IEnumerable<Either<T, CircularReferenceResult<T>>> ToIEnumerable<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Func<T, bool> stopCondition)
    {
        var nodeStack = new Stack<T>();
        var pathStack = new Stack<T>();
        nodeStack.Push(root);

        while (nodeStack.Count > 0)
        {
            var currentNode = nodeStack.Pop();
            yield return currentNode;
            if (stopCondition(currentNode))
            {
                yield return CircularReferenceResult<T>.Success();
                yield break;
            }

            pathStack.Push(currentNode);
            foreach (var child in children(currentNode).Reverse())
            {
                if (pathStack.Contains(child))
                {
                    yield return CircularReferenceResult<T>.Failure(child); // Circular reference detected
                    yield break;
                }

                nodeStack.Push(child);
            }

            if (!children(currentNode).Any())
            {
                pathStack.Pop();
                if (pathStack.Any())
                {
                    var a = pathStack.Peek();
                    if (nodeStack.Any())
                    {
                        var b = nodeStack.Peek();
                        while (!children(a).Contains(b) && pathStack.Any())
                        {
                            a = pathStack.Pop();
                        }
                    }
                }
            }
        }

        yield return CircularReferenceResult<T>.Success();
    }
}