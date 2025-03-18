using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using SSW.CleanArchitecture.Infrastructure.Persistence;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class Presentation : TestBase
{
    private static readonly Type _dbContextInterface = typeof(IApplicationDbContext);
    private static readonly Type _dbContextImplementation = typeof(ApplicationDbContext);

    [Test]
    public void Endpoints_ShouldNotReferenceDbContext()
    {
        var types = Types
            .InAssembly(PresentationAssembly)
            .That()
            .HaveNameEndingWith("Endpoints");

        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(_dbContextImplementation.FullName, _dbContextInterface.FullName)
            .GetResult();

        result.Should().BeSuccessful();
    }
}