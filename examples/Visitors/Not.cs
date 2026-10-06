namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Logical not of an expression.
/// </summary>
public sealed class Not : Expression
{
    public Not(Expression operand)
    {
        Children.Add(operand);
    }

    public Expression Operand => Children.First;
}