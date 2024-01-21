using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Listener for a constant. Just needs to push the value of the constant onto the stack.
/// </summary>
internal sealed class ConstantListener : Listener<EvaluationContext, MathsNode, Constant>
{
    protected override void ListenToNode(EvaluationContext context, Constant constant) =>
        context.Values.Push(constant.Value);
}