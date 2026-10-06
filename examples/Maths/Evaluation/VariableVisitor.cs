using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Visitor for a variable. Looks up the value of the variable in the context.
/// </summary>
internal sealed class VariableVisitor : Visitor<EvaluationContext, MathsNode, Variable, int>
{
    protected override int VisitNode(EvaluationContext context, Variable variable) => context.Arguments[variable.Name];
}