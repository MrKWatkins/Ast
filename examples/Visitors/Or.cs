namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Logical or of two expressions.
/// </summary>
public sealed class Or(Expression left, Expression right) : BinaryOperation(left, right)
{
}