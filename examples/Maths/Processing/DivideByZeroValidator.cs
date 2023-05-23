using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Examples.Maths.Processing;

/// <summary>
/// Validator to check for divide by zero.
/// </summary>
internal sealed class DivideByZeroValidator : Validator<MathsNode, BinaryOperation>
{
    protected override IEnumerable<Message> ValidateNode(BinaryOperation node)
    {
        if (node is { Operator: '/', Right: Constant { Value: 0 } })
        {
            yield return Message.Error("Divide by zero.");
        }
    }
}