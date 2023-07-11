using MrKWatkins.Ast.Examples.Maths.Evaluation;
using MrKWatkins.Ast.Examples.Maths.Tree;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Evaluation;

public sealed class BinaryOperationListenerTests
{
    [Test]
    public void AfterListenToNode_OperatorNotSupported()
    {
        var expression = new BinaryOperation('#', new Constant(5), new Constant(10));

        var context = new EvaluationContext(new Dictionary<string, int>());
        context.Values.Push(10);
        context.Values.Push(5);

        var listener = new BinaryOperationListener();

        listener.Invoking(l => l.Listen(context, expression)).Should().Throw<NotSupportedException>();
    }
}