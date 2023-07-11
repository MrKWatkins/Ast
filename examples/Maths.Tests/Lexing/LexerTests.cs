using System.Collections;
using MrKWatkins.Ast.Examples.Maths.Lexing;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Lexing;

public sealed class LexerTests
{
    [Test]
    public void Next_ThrowsIfFinished()
    {
        using var reader = new StringReader("");

        var lexer = new Lexer(reader);

        var tokens = lexer.ToList();
        tokens.Should().HaveCount(1);
        tokens[0].Should().BeOfType<EndOfFile>();

        lexer.Invoking(l => l.Next())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Input has already been consumed.");
    }

    [Test]
    public void Next_UnexpectedCharacter()
    {
        using var reader = new StringReader("_");

        var lexer = new Lexer(reader);

        lexer.Invoking(l => l.Next())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Unexpected character '_'.");
    }

    [TestCase("1 + x * 3", "1 + x * 3 EOF")]
    [TestCase("15/30+(someVar  +  456)", "15 / 30 + ( someVar + 456 ) EOF")]
    public void GetEnumerator(string input, string expected)
    {
        using var reader = new StringReader(input);

        var lexer = new Lexer(reader);

        var tokens = lexer.ToList();

        var actual = string.Join(" ", tokens);
        actual.Should().BeEquivalentTo(expected);

        foreach (var token in tokens)
        {
            if (token is EndOfFile endOfFile)
            {
                endOfFile.StartIndex.Should().Be(input.Length);
                endOfFile.Length.Should().Be(0);
            }
            else
            {
                token.ToString().Should().Be(input.Substring(token.StartIndex, token.Length));
            }
        }
    }

    [Test]
    public void GetEnumerator_Untyped()
    {
        using var reader = new StringReader("123456 * 7890");

        var lexer = new Lexer(reader);

        var enumerable = (IEnumerable)lexer;
        var tokens = enumerable.OfType<Token>().ToList();

        var actual = string.Join(" ", tokens);
        actual.Should().BeEquivalentTo("123456 * 7890 EOF");
    }

    [Test]
    public void Peek()
    {
        using var reader = new StringReader("123 + 456");

        var lexer = new Lexer(reader);

        var number1 = lexer.Peek();
        number1.Should().Be(new Number(0, 3, 123));
        lexer.Peek().Should().BeSameAs(number1);
        lexer.Next().Should().BeSameAs(number1);

        var @operator = lexer.Peek();
        @operator.Should().Be(new Operator(4, '+'));
        lexer.Peek().Should().BeSameAs(@operator);
        lexer.Next().Should().BeSameAs(@operator);

        var number2 = lexer.Peek();
        number2.Should().Be(new Number(6, 3, 456));
        lexer.Peek().Should().BeSameAs(number2);
        lexer.Next().Should().BeSameAs(number2);

        var eof = lexer.Peek();
        eof.Should().Be(new EndOfFile(9));
        lexer.Peek().Should().BeSameAs(eof);
        lexer.Next().Should().BeSameAs(eof);
    }

    [Test]
    public void Peek_ThrowsIfFinished()
    {
        using var reader = new StringReader("");

        var lexer = new Lexer(reader);

        var tokens = lexer.ToList();
        tokens.Should().HaveCount(1);
        tokens[0].Should().BeOfType<EndOfFile>();

        lexer.Invoking(l => l.Peek())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Input has already been consumed.");
    }
}