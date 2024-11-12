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


}

public static class TypeExtensions
{
    public static void Dump(this IEnumerable<Type> types, ITestOutputHelper outputHelper)
    {
        if (!types.Any())
            outputHelper.WriteLine("No types found.");

        foreach (var type in types)
            outputHelper.WriteLine(type.FullName);
    }
}