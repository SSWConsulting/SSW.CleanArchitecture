// using Xunit.Abstractions;

using TUnit.Core.Logging;
using TestResult = NetArchTest.Rules.TestResult;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;
public static class TestResultExtensions
{
    public static void DumpFailingTypes(this TestResult result, ILogger logger)
    {
        if (result.IsSuccessful)
            return;

        logger.LogInformation("Failing Types:");

        foreach (var type in result.FailingTypes)
            logger.LogInformation(type.FullName);
    }

    public static TestResultAssertions Should(this TestResult result) => new(result);
}