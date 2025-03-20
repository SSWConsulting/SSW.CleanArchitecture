using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Text;
using TestResult = NetArchTest.Rules.TestResult;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public class TestResultAssertions : ReferenceTypeAssertions<TestResult, TestResultAssertions>
{
    public TestResultAssertions(TestResult instance) : base(instance)
    {
    }

    protected override string Identifier => "TestResult";

    public AndConstraint<TestResultAssertions> BeSuccessful(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(s => s.IsSuccessful)
            .FailWith(GetFailureMessage());

        return new AndConstraint<TestResultAssertions>(this);
    }

    private string GetFailureMessage()
    {
        if (Subject.IsSuccessful)
            return string.Empty;

        var sb = new StringBuilder();
        sb.AppendLine("The following types failed the test:");

        foreach (var name in Subject.FailingTypeNames)
            sb.AppendLine(name);

        return sb.ToString();
    }
}