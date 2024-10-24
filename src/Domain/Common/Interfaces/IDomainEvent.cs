using MediatR;

namespace SSW.CleanArchitecture.Domain.Common.Interfaces;

/// <summary>
/// Can be raised by an AggregateRoot to notify subscribers of a domain event.
/// </summary>
public interface IDomainEvent : INotification;