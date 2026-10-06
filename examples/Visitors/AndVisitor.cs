using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Visitor for <see cref="And" />. Short-circuits: the right operand is only visited if the left operand is <c>true</c>.
/// This is something a listener cannot do, as a listener always walks the whole tree.
/// </summary>
internal sealed class AndVisitor : NodeVisitor<EvaluationContext, Expression, And, bool>
{
    protected override bool VisitNode(EvaluationContext context, And and) => Visit(context, and.Left) && Visit(context, and.Right);
}