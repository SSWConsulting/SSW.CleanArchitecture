using SSW.CleanArchitecture.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSW.CleanArchitecture.Domain.Common.Base;

public abstract class BaseEntity<TId> : AuditableEntity, IDomainEvents
{
    private readonly List<DomainEvent> _domainEvents = new();

    public TId Id { get; set; } = default!;

    [NotMapped]
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
