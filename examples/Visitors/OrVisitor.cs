using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Visitor for <see cref="Or" />. Short-circuits: the right operand is only visited if the left operand is <c>false</c>.
/// </summary>
internal sealed class OrVisitor : NodeVisitor<EvaluationContext, Expression, Or, bool>
{
    protected override bool VisitNode(EvaluationContext context, Or or) => Visit(context, or.Left) || Visit(context, or.Right);
}