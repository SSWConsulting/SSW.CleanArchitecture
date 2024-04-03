using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record StrengthUpdatedEvent(Hero Hero) : DomainEvent;