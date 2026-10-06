namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Base type for operations with left and right operands.
/// </summary>
public abstract class BinaryOperation : Expression
{
    private protected BinaryOperation(Expression left, Expression right)
    {
        Children.Add(left, right);
    }

    public Expression Left => Children.First;

    public Expression Right => Children.Last;
}