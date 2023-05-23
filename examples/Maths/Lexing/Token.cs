namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Base class for a lexer token.
/// </summary>
public abstract record Token
{
    private protected Token(int startIndex, int length)
    {
        StartIndex = startIndex;
        Length = length;
    }

    /// <summary>
    /// The start index for the token in the input stream.
    /// </summary>
    public int StartIndex { get; }
    
    /// <summary>
    /// The length of the token.
    /// </summary>
    public int Length { get; }
}