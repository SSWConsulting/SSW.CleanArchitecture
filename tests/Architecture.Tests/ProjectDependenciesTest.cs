using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ProjectDependenciesTest
{
    private const string DomainLayerName = nameof(Domain);
    private const string ApplicationLayerName = nameof(Application);
    private const string InfrastructureLayerName = nameof(Infrastructure);
    private const string WebApiLayerName = nameof(WebApi);

    private readonly string[] _availableProjects = {
        DomainLayerName, ApplicationLayerName, InfrastructureLayerName, WebApiLayerName,
    };

    public ProjectDependenciesTest()
    {
        foreach (var project in _availableProjects)
        {
            Assembly.Load(project);
        }
    }

    [Theory]
    [InlineData(DomainLayerName)]
    [InlineData(ApplicationLayerName)]
    [InlineData(InfrastructureLayerName)]
    [InlineData(WebApiLayerName)]
    public void ReferencedLayers_Should_Exists(string layerName)
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespaceStartingWith(layerName)
            .GetTypes()
            .Count();

        result.Should().BePositive();
    }
    
    [Theory]
    [InlineData(ApplicationLayerName)]
    [InlineData(InfrastructureLayerName)]
    [InlineData(WebApiLayerName)]
    public void Domain_ShouldNot_Reference_OuterLayers(string blacklistReference)
    {
        var relatedTypes = Types.InCurrentDomain()
            .That()
            .ResideInNamespaceStartingWith(DomainLayerName);
            
        var result = relatedTypes
            .ShouldNot()
            .HaveDependencyOnAny(blacklistReference)
            .GetResult()
            .IsSuccessful;

        relatedTypes.GetTypes().Count().Should().BePositive();
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(InfrastructureLayerName)]
    [InlineData(WebApiLayerName)]
    public void Application_ShouldNot_Reference_OuterLayers(string blacklistReference)
    {
        var relatedTypes = Types.InCurrentDomain()
            .That()
            .ResideInNamespaceStartingWith(ApplicationLayerName);
            
        var result = relatedTypes
            .ShouldNot()
            .HaveDependencyOnAny(blacklistReference)
            .GetResult()
            .IsSuccessful;

        relatedTypes.GetTypes().Count().Should().BePositive();
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(WebApiLayerName)]
    public void Infrastructure_ShouldNot_Reference_OuterLayers(string blacklistReference)
    {
        var relatedTypes = Types.InCurrentDomain()
            .That()
            .ResideInNamespaceStartingWith(InfrastructureLayerName);
            
        var result = relatedTypes
            .ShouldNot()
            .HaveDependencyOnAny(blacklistReference)
            .GetResult()
            .IsSuccessful;
        
        relatedTypes.GetTypes().Count().Should().BePositive();
        result.Should().BeTrue();
    }
}