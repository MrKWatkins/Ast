using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Listener for a variable. Looks up the value of the variable and pushes it onto the stack.
/// </summary>
internal sealed class VariableListener : ListenerWithContext<EvaluationContext, MathsNode, Variable>
{
    protected override void ListenToNode(EvaluationContext context, Variable variable) =>
        context.Values.Push(context.Arguments[variable.Name]);
}