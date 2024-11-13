using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using System.Reflection;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class DomainModel : TestBase
{
    private static readonly Type AggregateRoot = typeof(AggregateRoot<>);
    private static readonly Type Entity = typeof(Entity<>);
    private static readonly Type DomainEvent = typeof(IDomainEvent);
    private static readonly Type ValueObject = typeof(IValueObject);

    [Fact]
    public void DomainModel_ShouldInheritsBaseClasses()
    {
        // Arrange
        var domainModels = Types.InAssembly(DomainAssembly)
            .That()
            .DoNotResideInNamespaceContaining("Common")
            .And().DoNotHaveNameEndingWith("Id")
            .And().DoNotHaveNameEndingWith("Spec")
            .And().DoNotHaveNameEndingWith("Errors")
            .And().MeetCustomRule(new IsNotEnumRule());

        // Act
        var result = domainModels
            .Should()
            .Inherit(AggregateRoot)
            .Or().Inherit(Entity)
            .Or().ImplementInterface(DomainEvent)
            .Or().ImplementInterface(ValueObject)
            .GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void EntitiesAndAggregates_ShouldHavePrivateParameterlessConstructor()
    {
        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(Entity)
            .Or()
            .Inherit(AggregateRoot)
            .GetTypes()
            .Where(t => t != AggregateRoot);

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