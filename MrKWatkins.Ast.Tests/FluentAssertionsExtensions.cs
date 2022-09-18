using FluentAssertions.Collections;
using FluentAssertions.Execution;
using FluentAssertions.Specialized;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests;

// TODO: Create MrKWatkins.Ast.FluentAssertions package with some of these assertions, generalized for TestNode. Pull in methods from MrKWatkins.OakAsm.
[PublicAPI]
public static class FluentAssertionsExtensions
{
    public static AndConstraint<TAssertions> HaveSameOrderAs<TCollection, TAssertions>(
        this GenericCollectionAssertions<TCollection, TestNode, TAssertions> assertions, params TestNode[] expected)
        where TCollection : IEnumerable<TestNode>
        where TAssertions : GenericCollectionAssertions<TCollection, TestNode, TAssertions> =>
        assertions.HaveSameOrderAs((IEnumerable<TestNode>) expected);

    public static AndConstraint<TAssertions> HaveSameOrderAs<TCollection, TAssertions>(
        this GenericCollectionAssertions<TCollection, TestNode, TAssertions> assertions, [InstantHandle] IEnumerable<TestNode> expected)
        where TCollection : IEnumerable<TestNode>
        where TAssertions : GenericCollectionAssertions<TCollection, TestNode, TAssertions>
    {
        var expectedList = expected.ToList();
        var actualList = assertions.Subject.ToList();

        Execute.Assertion
            .UsingLineBreaks
            .ForCondition(expectedList.SequenceEqual(actualList))
            .FailWith(() =>
            {
                var longestName = expectedList.Concat(actualList).Max(n => n.ToString().Length);
                
                [Pure]
                string NodesText(IEnumerable<TestNode> nodes) => string.Join(' ', nodes.Select(n => n.Name.PadRight(longestName, ' ')));
                
                return new FailReason(
                    $"Order did not match expectation.{Environment.NewLine}" +
                    $"Expected: {NodesText(expectedList)}{Environment.NewLine}" +
                    $"Actual:   {NodesText(actualList)}");
            });

        return new AndConstraint<TAssertions>((TAssertions) assertions);
    }

    public static ExceptionAssertions<ProcessingException<TestNode>> WithParameters(
        this ExceptionAssertions<ProcessingException<TestNode>> assertions,
        string expectedMessage, TestNode expectedNode,
        string because = "", params object[] becauseArgs) =>
        assertions.WithParameters(expectedMessage, expectedNode, null, because, becauseArgs);
    
    public static ExceptionAssertions<ProcessingException<TestNode>> WithParameters(
        this ExceptionAssertions<ProcessingException<TestNode>> assertions, 
        string expectedMessage, Exception expectedInnerException, TestNode? expectedNode,
        string because = "", params object[] becauseArgs) =>
        assertions.WithParameters(expectedMessage, expectedNode, () => assertions.Which.InnerException.Should().Be(expectedInnerException), because, becauseArgs);
    
    private static ExceptionAssertions<ProcessingException<TestNode>> WithParameters(
        this ExceptionAssertions<ProcessingException<TestNode>> assertions, 
        string expectedMessage, TestNode? expectedNode, Action? extraAssertions,
        string because = "", params object[] becauseArgs)
    {
        using (new AssertionScope())
        {
            var message = $"{expectedMessage} (Node '{expectedNode}')";
            assertions.WithMessage(message, because, becauseArgs);
            
            assertions.Which.Node.Should().Be(expectedNode, because, becauseArgs);
            extraAssertions?.Invoke();
        }

        return assertions;
    }
}