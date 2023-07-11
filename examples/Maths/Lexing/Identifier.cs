namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Lexer token representing an arbitrary identifier.
/// </summary>
public sealed record Identifier : Token
{
    internal Identifier(int startIndex, string name)
        : base(startIndex, name.Length)
    {
        Name = name;
    }

    public string Name { get; }

    public override string ToString() => Name;
}