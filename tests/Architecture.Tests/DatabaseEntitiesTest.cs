using Domain.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class DatabaseEntities
{
    [Fact]
    public void Entities_Should_Inherits_BaseComponent()
    {
        var entityTypes = Types.InAssembly(typeof(Infrastructure.DependencyInjection).Assembly)
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
            .Inherit(typeof(BaseEntity<>));
        
        result.GetTypes().Count().Should().BePositive();
        result.GetResult().IsSuccessful.Should().BeTrue();
    }
}