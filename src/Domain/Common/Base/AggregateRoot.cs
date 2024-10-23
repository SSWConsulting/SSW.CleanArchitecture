using SSW.CleanArchitecture.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSW.CleanArchitecture.Domain.Common.Base;

/// <summary>
/// Cluster of objects treated as a single unit.
/// Can contain entities, value objects, and other aggregates.
/// Enforce business rules (i.e. invariants)
/// Can be created externally.
/// Can raise domain events.
/// Represent a transactional boundary (i.e. all changes are saved or none are saved)
/// </summary>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<DomainEvent> _domainEvents = [];

    [NotMapped] public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}