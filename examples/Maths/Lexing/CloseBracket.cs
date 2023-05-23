namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Lexer token for a close bracket, ')'.
/// </summary>
public sealed record CloseBracket : Token
{
    internal CloseBracket(int index) 
        : base(index, 1)
    {
    }
    
    public override string ToString() => ")";
}