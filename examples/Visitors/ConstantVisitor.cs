using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Visitor for constants. Returns the value of the constant.
/// </summary>
internal sealed class ConstantVisitor : NodeVisitor<EvaluationContext, Expression, Constant, bool>
{
    protected override bool VisitNode(EvaluationContext context, Constant constant) => constant.Value;
}