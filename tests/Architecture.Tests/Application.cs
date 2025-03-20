using MediatR;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class Application : TestBase
{
    private static readonly Type _requestHandler = typeof(IRequestHandler<,>);

    [Test]
    public void CommandHandlers_ShouldHaveCorrectSuffix()
    {
        var types = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .And()
            .ImplementInterface(_requestHandler);

        var result = types
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        result.Should().BeSuccessful();
    }

    [Test]
    public void QueryHandlers_ShouldHaveCorrectSuffix()
    {
        var types = Types
                .InAssembly(ApplicationAssembly)
                .That()
                .ResideInNamespaceContaining("Queries")
                .And()
                .ImplementInterface(_requestHandler);

        var result = types
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        result.Should().BeSuccessful();
    }
}