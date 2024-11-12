using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using System.Reflection;
using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class DomainModel(ITestOutputHelper outputHelper) : TestBase
{
    private static Type AggregateRoot = typeof(AggregateRoot<>);
    private static Type Entity = typeof(Entity<>);
    private static Type DomainEvent = typeof(IDomainEvent);
    private static Type ValueObject = typeof(IValueObject);

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
            .Or().ImplementInterface(ValueObject);

        // Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
        result.GetResult().DumpFailingTypes(outputHelper);
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

    // [Fact]
    // public void EntitiesAndAggregates_Should_Have_PrivateSetter()
    // {
    //     var entityTypes = Types.InAssembly(DomainAssembly)
    //         .That()
    //         .AreClasses()
    //         .And()
    //         .Inherit(Entity)
    //         .Or()
    //         .Inherit(AggregateRoot)
    //         .GetTypes()
    //         .Where(t => t != AggregateRoot && !t.FullName.EndsWith("Id"));
    //
    //
    //     foreach (var entityType in entityTypes)
    //     {
    //         var properties = entityType.GetProperties();
    //         foreach (var property in properties)
    //         {
    //             if (property.CanWrite)
    //             {
    //                 property.SetMethod.Should().NotBeNull()
    //                     .And.Match(setMethod => setMethod.IsPrivate || setMethod.IsFamily || setMethod.IsFamilyOrAssembly,
    //                         $"{property.Name} should have a private or protected setter.", property.DeclaringType?.FullName);
    //             }
    //         }
    //     }
    // }
}