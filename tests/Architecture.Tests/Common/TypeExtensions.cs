using TUnit.Core.Logging;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public static class TypeExtensions
{
    public static void Dump(this IEnumerable<Type> types, ILogger outputHelper)
    {
        if (!types.Any())
        {
            outputHelper.LogInformation("No types found.");
            return;
        }

        foreach (var type in types)
        {
            if (type.FullName is not null)
                outputHelper.LogInformation(type.FullName);
        }
    }
}