// using Xunit.Abstractions;

using TUnit.Core.Logging;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public static class TypeExtensions
{
    public static void Dump(this IEnumerable<Type> types, ILogger outputHelper)
    {
        if (!types.Any())
            outputHelper.LogInformation("No types found.");

        foreach (var type in types)
            outputHelper.LogInformation(type.FullName);
    }
}