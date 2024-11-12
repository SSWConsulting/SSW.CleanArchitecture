using Mono.Cecil;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public class IsEnumRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => type.IsEnum;
}