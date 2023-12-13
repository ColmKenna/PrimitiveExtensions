namespace PrimativeExtensions;

public abstract class EventSourcedRoot<T> 
    where T : EventSourcedRoot<T> 
{
    protected readonly List<IDomainEvent<T>> _domainEvents = new();
    public virtual IReadOnlyList<IDomainEvent<T>> DomainEvents => _domainEvents.AsReadOnly();

    public virtual Validation<T> AddEvent(IDomainEvent<T> domainEvent)
    {
        
        _domainEvents.Add(domainEvent);
        return HandleEvent(domainEvent);
    }

    public IEnumerable<TR> GetEvents<TR>() where TR : IDomainEvent<T>
    {
        return _domainEvents.OfType<TR>();
    }

    public IEnumerable< IDomainEvent<T>> GetEvents<TR1,TR2>() 
        where TR1 : IDomainEvent<T> 
        where TR2 : IDomainEvent<T>
    {
        var events = new List<IDomainEvent<T>>();
        events.AddRange(_domainEvents.OfType<TR1>().Cast<IDomainEvent<T>>());
        events.AddRange(_domainEvents.OfType<TR2>().Cast<IDomainEvent<T>>());
        return events.OrderByDescending(x => x.OccurredOn);

    }
    
    
    

    public T GetStateOn(DateTime dateToGetStateFor)
    {
        var root = GetNewState();
        var events = this._domainEvents.Where(x => x.OccurredOn <= dateToGetStateFor).OrderBy(x => x.OccurredOn);
        foreach (var domainEvent in events)
        {
            root.AddEvent(domainEvent);
        }

        return root;
    }

    protected abstract Validation<T> HandleEvent(IDomainEvent<T> domainEvent);

    public abstract T GetNewState();

}