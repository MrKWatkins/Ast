using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;
using LinqExpression = System.Linq.Expressions.Expression;

namespace MrKWatkins.Ast.Examples.Maths.Compilation;

/// <summary>
/// Listener for a constant. Just needs to push the value of the constant onto the stack.
/// </summary>
internal sealed class ConstantListener : Listener<CompilationContext, MathsNode, Constant>
{
    protected override void ListenToNode(CompilationContext context, Constant constant) =>
        context.Values.Push(LinqExpression.Constant(constant.Value));
}