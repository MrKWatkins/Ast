using System.Text;

namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// A constant value.
/// </summary>
public sealed class Constant : Expression
{
    internal Constant(int value)
    {
        Value = value;
    }

    /// <summary>
    /// The value.
    /// </summary>
    public int Value
    {
        get => Properties.GetOrThrow<int>(nameof(Value));
        init => Properties.Set(nameof(Value), value);
    }

    internal override void WriteSymbolicExpression(StringBuilder expression) => expression.Append(Value);
}