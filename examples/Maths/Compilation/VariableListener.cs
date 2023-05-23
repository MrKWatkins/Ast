using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Maths.Compilation;

/// <summary>
/// Listener for a variable. Looks up the value of the variable and pushes it onto the stack.
/// </summary>
internal sealed class VariableListener : ListenerWithContext<CompilationContext, MathsNode, Variable>
{
    protected override void ListenToNode(CompilationContext context, Variable variable) =>
        context.Values.Push(context.Parameters[variable.Name]);
}