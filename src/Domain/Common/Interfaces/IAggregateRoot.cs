namespace SSW.CleanArchitecture.Domain.Common.Interfaces;

public interface IAggregateRoot
{
    public void AddDomainEvent(IDomainEvent domainEvent);

    IReadOnlyList<IDomainEvent> PopDomainEvents();
}