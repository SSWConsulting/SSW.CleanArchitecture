using MediatR;

namespace SSW.CleanArchitecture.Domain.Common.Base;

#pragma warning disable CA1040 // Avoid empty interfaces
public interface IDomainEvent : INotification
#pragma warning restore CA1040 // Avoid empty interfaces
{
};
