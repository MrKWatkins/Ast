namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Lexer token for an open bracket, '('.
/// </summary>
public sealed record OpenBracket : Token
{
    internal OpenBracket(int index) 
        : base(index, 1)
    {
    }

    public override string ToString() => "(";
}