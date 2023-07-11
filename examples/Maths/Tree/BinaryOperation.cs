using System.Text;

namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// A binary operation. The expression has a left and right side and an operator.
/// </summary>
public sealed class BinaryOperation : Expression
{
    internal BinaryOperation(char @operator, Expression left, Expression right)
    {
        Operator = @operator;
        Children.Add(left);
        Children.Add(right);
    }

    /// <summary>
    /// The operator.
    /// </summary>
    public char Operator
    {
        get => Properties.GetOrThrow<char>(nameof(Operator));
        init => Properties.Set(nameof(Operator), value);
    }

    /// <summary>
    /// The left side of the operation.
    /// </summary>
    public Expression Left => (Expression)FirstChild;

    /// <summary>
    /// The right side of the operation.
    /// </summary>
    public Expression Right => (Expression)LastChild;

    internal override void WriteSymbolicExpression(StringBuilder expression)
    {
        expression.Append('(');
        expression.Append(Operator);
        foreach (var child in Children.OfType<Expression>())
        {
            expression.Append(' ');
            child.WriteSymbolicExpression(expression);
        }
        expression.Append(')');
    }
}