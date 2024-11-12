using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class Presentation(ITestOutputHelper outputHelper) : TestBase
{
    private static readonly Type IDbContext = typeof(IApplicationDbContext);
    private static readonly Type DbContext = typeof(ApplicationDbContext);

    [Fact]
    public void Endpoints_ShouldNotReferenceDbContext()
    {
        var result = Types
            .InAssembly(PresentationAssembly)
            .That()
            .HaveNameEndingWith("Endpoints")
            .ShouldNot()
            .HaveDependencyOnAny(DbContext.FullName, IDbContext.FullName)
            .GetResult();

        // TODO: can we write a custom fluent assertion for this?
        result.DumpFailingTypes(outputHelper);
        result.IsSuccessful.Should().BeTrue();
    }
}