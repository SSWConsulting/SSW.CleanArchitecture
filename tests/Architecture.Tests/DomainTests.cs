using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using System.Reflection;
using TUnit.Core.Logging;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class DomainModel : TestBase
{
    private static readonly Type _aggregateRoot = typeof(AggregateRoot<>);
    private static readonly Type _entity = typeof(Entity<>);
    private static readonly Type _domainEvent = typeof(IDomainEvent);
    private static readonly Type _valueObject = typeof(IValueObject);

    [Test]
    public void DomainModel_ShouldInheritsBaseClasses()
    {
        // Arrange
        var domainModels = Types.InAssembly(DomainAssembly)
            .That()
            .DoNotResideInNamespaceContaining("Common")
            .And().DoNotHaveNameMatching(".*Id.*")
            .And().DoNotHaveNameMatching(".*Vogen.*")
            .And().DoNotHaveName("ThrowHelper")
            .And().DoNotHaveNameEndingWith("Spec")
            .And().DoNotHaveNameEndingWith("Errors")
            .And().MeetCustomRule(new IsNotEnumRule());

        domainModels.GetTypes().Dump(new DefaultLogger());

        // Act
        var result = domainModels
            .Should()
            .Inherit(_aggregateRoot)
            .Or().Inherit(_entity)
            .Or().ImplementInterface(_domainEvent)
            .Or().ImplementInterface(_valueObject)
            .GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Test]
    public void EntitiesAndAggregates_ShouldHavePrivateParameterlessConstructor()
    {
        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(_entity)
            .Or()
            .Inherit(_aggregateRoot)
            .GetTypes()
            .Where(t => t != _aggregateRoot);

        var failingTypes = new List<Type>();

        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
                failingTypes.Add(entityType);

            failingTypes.Should().BeEmpty();
        }
    }
}