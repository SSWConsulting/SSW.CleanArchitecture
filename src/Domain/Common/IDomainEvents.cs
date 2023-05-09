namespace Domain.Common;

public interface IDomainEvents
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void AddDomainEvent(BaseEvent domainEvent);

    void ClearDomainEvents();

    void RemoveDomainEvent(BaseEvent domainEvent);
}