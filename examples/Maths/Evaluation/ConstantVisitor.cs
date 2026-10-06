using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Visitor for a constant. Just needs to return the value of the constant.
/// </summary>
internal sealed class ConstantVisitor : NodeVisitor<EvaluationContext, MathsNode, Constant, int>
{
    protected override int VisitNode(EvaluationContext context, Constant constant) => constant.Value;
}