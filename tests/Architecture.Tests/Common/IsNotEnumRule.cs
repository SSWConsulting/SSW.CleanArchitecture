using Mono.Cecil;
using NetArchTest.Rules;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public class IsNotEnumRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => !type.IsEnum;
}