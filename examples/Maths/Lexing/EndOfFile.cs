namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Lexer token representing the end of the file.
/// </summary>
public sealed record EndOfFile : Token
{
    internal EndOfFile(int index) 
        : base(index, 0)
    {
    }
    
    public override string ToString() => "EOF";
}