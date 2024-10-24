using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Common.Interfaces;

public interface IAggregateRoot
{
    // public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    public void AddDomainEvent(IDomainEvent domainEvent);
    //
    // public void RemoveDomainEvent(DomainEvent domainEvent);
    //
    // public void ClearDomainEvents();

    IReadOnlyList<IDomainEvent> PopDomainEvents();
}