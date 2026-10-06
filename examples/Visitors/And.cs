namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Logical and of two expressions.
/// </summary>
public sealed class And : BinaryOperation
{
    public And(Expression left, Expression right)
        : base(left, right)
    {
    }
}