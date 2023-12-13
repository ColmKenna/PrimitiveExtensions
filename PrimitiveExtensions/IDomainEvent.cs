namespace PrimativeExtensions;

public interface IDomainEvent<T>
{
    DateTime OccurredOn { get; }
    string EventDescription { get; }    
}