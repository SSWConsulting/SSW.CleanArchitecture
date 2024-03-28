using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heros;

public record StrengthUpdatedEvent(Hero Hero) : DomainEvent;