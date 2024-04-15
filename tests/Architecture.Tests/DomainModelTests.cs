using FluentAssertions;
using NetArchTest.Rules;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class DomainModelTests
{
    private readonly ITestOutputHelper _outputHelper;

    public DomainModelTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void DomainModel_ShouldInheritsBaseClasses()
    {
        // Arrange
        var domainModels = Types.InAssembly(typeof(AggregateRoot<>).Assembly)
            .That()
            .DoNotResideInNamespaceContaining("Common")
            .And().DoNotHaveNameEndingWith("Id")
            .And().DoNotHaveNameEndingWith("Spec")
            .And().MeetCustomRule(new IsNotEnumRule());

        // Act
        var result = domainModels
            .Should()
            .Inherit(typeof(AggregateRoot<>))
            .Or().Inherit(typeof(Entity<>))
            .Or().Inherit(typeof(BaseEntity<>))
            .Or().Inherit(typeof(DomainEvent))
            .Or().ImplementInterface(typeof(IValueObject));

        // Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
        result.GetResult().DumpFailingTypes(_outputHelper);
    }
}