namespace PrimativeExtensions;

public static class BreadthFirstMethods
{
    public static Either<CircularReferenceResult<T>, IList<T>> SearchBreadthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Func<T, bool> predicate) where T: class
    {
        var list = new List<T>();
        Action<T> action = x =>
        {
            if (predicate(x))
            {
                list.Add(x);
            }
        };
        var result = BreadthFirst(root, children, action);
        return result.IsSuccessful ? list : result;
    }


    public static IList<T> GetPath<T>(T node, IDictionary<T, T> parents)
    {
        var path = new List<T>();
        while (parents.ContainsKey(node))
        {
            path.Add(parents[node]);
            node = parents[node];
        }

        return path;
    }


    public static CircularReferenceResult<T> CheckPath<T>(
        T node,
        IList<KeyValuePair<T, T>> parents,
        IList<T> currentPath) where T : class
    {
        var currentParents = parents.Where(x => x.Key == node);
        if (currentParents.Any())
        {
            foreach (var parent in currentParents.Where(x => x.Key == node))
            {
                if (parent.Value == node || currentPath.Contains(parent.Value)) 
                {
                    return CircularReferenceResult<T>.Failure(node);
                }
            
                var recursiveResult = CheckPath(parent.Value, parents, currentPath.Concat( parent.Value ).ToList());
                if (!recursiveResult.IsSuccessful)
                {
                    return recursiveResult;
                }
            }
        }

        return CircularReferenceResult<T>.Success(); // No circular references found
    }


    public static CircularReferenceResult<T> BreadthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Action<T> action) where T : class
    {
        var parents = new List<KeyValuePair<T, T>>();
        var queue = new Queue<T>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            action(currentNode);
            foreach (var child in children(currentNode))
            {
                parents.Add(new KeyValuePair<T, T>(child, currentNode));
                var checkPath = CheckPath(child, parents , new List<T>());
                if (!checkPath.IsSuccessful)
                {
                    return checkPath;
                }
                queue.Enqueue(child);
            }
        }

        return CircularReferenceResult<T>.Success(); // No circular references found
    }

    public static CircularReferenceResult<T> BreadthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children,
        Action<T> action,
        Func<T, bool> stopCondition)
    {
        var parents = new Dictionary<T, T>();
        var visited = new HashSet<T>();
        var queue = new Queue<T>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();

            // var checkPath = BreadthFirstMethods.CheckPath(currentNode, parents, comparer);
            // if (!checkPath.IsSuccessful)
            // {
            //     return checkPath;
            // }

            visited.Add(currentNode);
            action(currentNode);
            if (stopCondition(currentNode))
            {
                return CircularReferenceResult<T>.Success(); // Stop condition met, early exit
            }

            foreach (var child in children(currentNode))
            {
                parents[child] = currentNode;
                queue.Enqueue(child);
            }
        }

        return CircularReferenceResult<T>.Success(); // No circular references found
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
        var result = BreadthFirst(root, children, action,  stopCondition);

        return result.IsSuccessful
            ? list.Any() ? new Maybe<T>(list[0]) : Maybe<T>.None
            : result;
    }
    
    public static IEnumerable<T> BreadthFirst<T>(
        T root,
        Func<T, IEnumerable<T>> children) where T : class
    {
        var queue = new Queue<T>();
        var visited = new HashSet<T>();

        queue.Enqueue(root);
        visited.Add(root);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            yield return currentNode;

            foreach (var child in children(currentNode))
            {
                if (!visited.Contains(child))
                {
                    queue.Enqueue(child);
                    visited.Add(child);
                }
            }
        }
    }

}