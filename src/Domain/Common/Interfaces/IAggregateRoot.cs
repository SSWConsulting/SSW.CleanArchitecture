namespace SSW.CleanArchitecture.Domain.Common.Interfaces;

public interface IAggregateRoot
{
    void AddDomainEvent(IDomainEvent domainEvent);

    IReadOnlyList<IDomainEvent> PopDomainEvents();
}