namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Logical and of two expressions.
/// </summary>
public sealed class And(Expression left, Expression right) : BinaryOperation(left, right)
{
}