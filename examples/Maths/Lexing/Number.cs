namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Lexer token representing a number.
/// </summary>
public sealed record Number : Token
{
    internal Number(int startIndex, int length, int value)
        : base(startIndex, length)
    {
        Value = value;
    }

    public int Value { get; }

    public override string ToString() => Value.ToString();
}