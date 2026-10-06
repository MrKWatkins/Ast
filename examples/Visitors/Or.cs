namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Logical or of two expressions.
/// </summary>
public sealed class Or : BinaryOperation
{
    public Or(Expression left, Expression right)
        : base(left, right)
    {
    }
}