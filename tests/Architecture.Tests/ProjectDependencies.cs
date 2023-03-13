using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ProjectDependencies
{
    private const string DomainLayerName = nameof(Domain);
    private const string ApplicationLayerName = nameof(Application);
    private const string InfrastructureLayerName = nameof(Infrastructure);
    private const string WebApiLayerName = nameof(WebApi);

    private readonly IEnumerable<Assembly> _scannedAssemblies = new[]
    {
        typeof(Domain.Common.BaseEntity).Assembly,
        typeof(Application.DependencyInjection).Assembly,
        typeof(Infrastructure.DependencyInjection).Assembly,
        typeof(WebApi.DependencyInjection).Assembly,
    };

    [Theory]
    [InlineData(DomainLayerName)]
    [InlineData(ApplicationLayerName)]
    [InlineData(InfrastructureLayerName)]
    [InlineData(WebApiLayerName)]
    public void ReferencedLayers_Should_Exists(string layerName)
    {
        // No Domain name :(
        var result = Types.InAssemblies(_scannedAssemblies)
            .That()
            .ResideInNamespaceStartingWith(layerName)
            .GetTypes()
            .Count();

        result.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [InlineData(ApplicationLayerName)]
    [InlineData(InfrastructureLayerName)]
    [InlineData(WebApiLayerName)]
    public void Domain_ShouldNot_Reference_OuterLayers(string blacklistReference)
    {
        // Classes in the presentation should not directly reference repositories
        var result = Types.InAssemblies(_scannedAssemblies)
            .That()
            .ResideInNamespaceStartingWith(DomainLayerName)
            .ShouldNot()
            .HaveDependencyOnAny(blacklistReference)
            .GetResult()
            .IsSuccessful;

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(InfrastructureLayerName)]
    [InlineData(WebApiLayerName)]
    public void Application_ShouldNot_Reference_OuterLayers(string blacklistReference)
    {
        // Classes in the presentation should not directly reference repositories
        var result = Types.InAssemblies(_scannedAssemblies)
            .That()
            .ResideInNamespaceStartingWith(ApplicationLayerName)
            .ShouldNot()
            .HaveDependencyOnAny(blacklistReference)
            .GetResult()
            .IsSuccessful;

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(WebApiLayerName)]
    public void Infrastructure_ShouldNot_Reference_OuterLayers(string blacklistReference)
    {
        // Classes in the presentation should not directly reference repositories
        var result = Types.InAssemblies(_scannedAssemblies)
            .That()
            .ResideInNamespaceStartingWith(InfrastructureLayerName)
            .ShouldNot()
            .HaveDependencyOnAny(blacklistReference)
            .GetResult()
            .IsSuccessful;

        result.Should().BeTrue();
    }
}