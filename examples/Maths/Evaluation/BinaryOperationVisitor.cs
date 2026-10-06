using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Visitor for a binary operation. Visits the left and right operands to get their values, then combines them using the operator.
/// </summary>
internal sealed class BinaryOperationVisitor : Visitor<EvaluationContext, MathsNode, BinaryOperation, int>
{
    protected override int VisitNode(EvaluationContext context, BinaryOperation operation)
    {
        var left = Visit(context, operation.Left);
        var right = Visit(context, operation.Right);

        return operation.Operator switch
        {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => left / right,
            _ => throw new NotSupportedException($"The operator {operation.Operator} is not supported.")
        };
    }
}