using Xunit.Abstractions;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

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