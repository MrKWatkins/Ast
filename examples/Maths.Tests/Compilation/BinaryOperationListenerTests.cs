using System.Linq.Expressions;
using MrKWatkins.Ast.Examples.Maths.Compilation;
using MrKWatkins.Ast.Examples.Maths.Tree;
using LinqExpression = System.Linq.Expressions.Expression;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Compilation;

public sealed class BinaryOperationListenerTests
{
    [Test]
    public void AfterListenToNode_OperatorNotSupported()
    {
        var expression = new BinaryOperation('#', new Constant(5), new Constant(10));

        var context = new CompilationContext(new Dictionary<string, ParameterExpression>());
        context.Values.Push(LinqExpression.Constant(10));
        context.Values.Push(LinqExpression.Constant(5));

        var listener = new BinaryOperationListener();

        listener.Invoking(l => l.Listen(context, expression)).Should().Throw<NotSupportedException>();
    }
}