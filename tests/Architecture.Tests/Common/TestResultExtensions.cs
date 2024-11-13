using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public static class TestResultExtensions
{
    public static void DumpFailingTypes(this TestResult result, ITestOutputHelper outputHelper)
    {
        if (result.IsSuccessful)
            return;

        outputHelper.WriteLine("Failing Types:");

        foreach (var type in result.FailingTypes)
            outputHelper.WriteLine(type.FullName);
    }

    public static TestResultAssertions Should(this TestResult result) => new(result);
}