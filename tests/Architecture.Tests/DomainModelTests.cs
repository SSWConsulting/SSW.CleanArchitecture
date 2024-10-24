using FluentAssertions;
using NetArchTest.Rules;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class DomainModelTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public void DomainModel_ShouldInheritsBaseClasses()
    {
        // Arrange
        var domainModels = Types.InAssembly(typeof(AggregateRoot<>).Assembly)
            .That()
            .DoNotResideInNamespaceContaining("Common")
            .And().DoNotHaveNameEndingWith("Id")
            .And().DoNotHaveNameEndingWith("Spec")
            .And().DoNotHaveNameEndingWith("Errors")
            .And().MeetCustomRule(new IsNotEnumRule());

        // Act
        var result = domainModels
            .Should()
            .Inherit(typeof(AggregateRoot<>))
            .Or().Inherit(typeof(Entity<>))
            .Or().ImplementInterface(typeof(IDomainEvent))
            .Or().ImplementInterface(typeof(IValueObject));

        // Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
        result.GetResult().DumpFailingTypes(outputHelper);
    }
}