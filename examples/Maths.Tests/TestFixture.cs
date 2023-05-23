using MrKWatkins.Ast.Examples.Maths.Lexing;
using MrKWatkins.Ast.Examples.Maths.Parsing;
using MrKWatkins.Ast.Examples.Maths.Tree;

namespace MrKWatkins.Ast.Examples.Maths.Tests;

public abstract class TestFixture
{
    [Pure]
    protected static Function ParseWithoutProcessing(string expression)
    {
        using var reader = new StringReader(expression);
        var lexer = new Lexer(reader);
        return Parser.ParseWithoutProcessing(lexer);
    }
}