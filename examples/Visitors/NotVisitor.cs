using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Visitor for <see cref="Not" />. Negates the value of the operand.
/// </summary>
internal sealed class NotVisitor : NodeVisitor<EvaluationContext, Expression, Not, bool>
{
    protected override bool VisitNode(EvaluationContext context, Not not) => !Visit(context, not.Operand);
}