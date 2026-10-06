using MrKWatkins.Ast.Examples.Maths.Evaluation;
using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Evaluation;

public sealed class BinaryOperationVisitorTests
{
    [Test]
    public void VisitNode_OperatorNotSupported()
    {
        var expression = new BinaryOperation('#', new Constant(5), new Constant(10));

        var context = new EvaluationContext(new Dictionary<string, int>());

        var visitor = CompositeVisitor<EvaluationContext, MathsNode, int>
            .Build()
            .With(new BinaryOperationVisitor())
            .With(new ConstantVisitor())
            .ToVisitor();

        visitor.Invoking(v => v.Visit(context, expression)).Should().Throw<NotSupportedException>();
    }
}