using MediatR;
using SSW.CleanArchitecture.Architecture.UnitTests.Common;
using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests;

public class Application(ITestOutputHelper outputHelper) : TestBase
{
    private static readonly Type IRequestHandler = typeof(IRequestHandler<,>);

    [Fact]
    public void CommandHandlers_ShouldHaveCorrectSuffix()
    {
        var types = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .And()
            .ImplementInterface(IRequestHandler)
            ;

        types.GetTypes().Dump(outputHelper);

        var result = types
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        // TODO: can we write a custom fluent assertion for this?
        result.DumpFailingTypes(outputHelper);
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_ShouldHaveCorrectSuffix()
    {
        var types = Types
                .InAssembly(ApplicationAssembly)
                .That()
                .ResideInNamespaceContaining("Queries")
                .And()
                .ImplementInterface(IRequestHandler)
            ;

        types.GetTypes().Dump(outputHelper);

        var result = types
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        // TODO: can we write a custom fluent assertion for this?
        result.DumpFailingTypes(outputHelper);
        result.IsSuccessful.Should().BeTrue();
    }
}