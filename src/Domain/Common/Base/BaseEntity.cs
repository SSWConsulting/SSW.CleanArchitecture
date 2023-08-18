using SSW.CleanArchitecture.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSW.CleanArchitecture.Domain.Common.Base;

public abstract class BaseEntity<TId> : AuditableEntity, IDomainEventEmitter
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public TId Id { get; set; } = default!;

    [NotMapped]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
