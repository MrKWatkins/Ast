using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Listener for a binary operation. After listening to the node, i.e. the children have been processed, then there will be two values
/// on the stack representing the right and left values. (Left will be processed first and therefore lower in the stack) These can be
/// popped off and evaluated and then the result pushed back on.
/// </summary>
internal sealed class BinaryOperationListener : Listener<EvaluationContext, MathsNode, BinaryOperation>
{
    protected override void AfterListenToNode(EvaluationContext context, BinaryOperation operation)
    {
        var right = context.Values.Pop();
        var left = context.Values.Pop();

        context.Values.Push(
            operation.Operator switch
            {
                '+' => left + right,
                '-' => left - right,
                '*' => left * right,
                '/' => left / right,
                _ => throw new NotSupportedException($"The operator {operation.Operator} is not supported.")
            });
    }
}