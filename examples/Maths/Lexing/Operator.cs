namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Lexer token representing an operator, '+', '-', '*' or '/'.
/// </summary>
public sealed record Operator : Token
{
    internal Operator(int index, char symbol) 
        : base(index, 1)
    {
        Symbol = symbol;
    }
    
    public char Symbol { get; }

    public override string ToString() => new (Symbol, 1);
}