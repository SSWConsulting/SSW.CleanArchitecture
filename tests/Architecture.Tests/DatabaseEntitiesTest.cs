using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure;
using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class DatabaseEntities
{
    private readonly ITestOutputHelper _outputHelper;

    public DatabaseEntities(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    // // TODO: Fix this test
    [Fact]
    public void Entities_ShouldInheritsBaseComponent()
    {
        var entityTypes = Types.InAssembly(typeof(DependencyInjection).Assembly)
            .That()
            .Inherit(typeof(DbContext))
            .GetTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Select(p => p.PropertyType)
            .Select(t => t.GetGenericArguments().FirstOrDefault()?.Name)
            .ToArray();

        var result = Types.InAssembly(typeof(BaseEntity<>).Assembly)
            .That()
            .HaveName(entityTypes)
            .Should()
            .Inherit(typeof(BaseEntity<>))
            .Or()
            .Inherit(typeof(IAggregateRoot))
            ;

        result.GetResult().IsSuccessful.Should().BeTrue();
        result.GetResult().DumpFailingTypes(_outputHelper);
    }
}