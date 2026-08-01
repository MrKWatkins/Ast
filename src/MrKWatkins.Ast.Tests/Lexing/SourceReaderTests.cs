using MrKWatkins.Ast.Lexing;
using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Lexing;

public sealed class SourceReaderTests
{
    [Test]
    public void Constructor()
    {
        var file = new TextFile("Test", "Some test code.");

        var reader = new SourceReader(file);
        reader.File.Should().BeTheSameInstanceAs(file);
        reader.Index.Should().Equal(0);
        reader.LineIndex.Should().Equal(0);
        reader.ColumnIndex.Should().Equal(0);
        reader.AtEnd.Should().BeFalse();
        reader.Current.Should().Equal('S');
    }

    [Test]
    public void Constructor_EmptyFile()
    {
        var reader = new SourceReader(new TextFile("Test", ""));
        reader.AtEnd.Should().BeTrue();
        reader.Current.Should().Equal('\0');
    }

    [Test]
    public void Peek()
    {
        var reader = new SourceReader(new TextFile("Test", "abc"));
        reader.Advance();

        reader.Peek(-2).Should().Equal('\0');
        reader.Peek(-1).Should().Equal('a');
        reader.Peek(0).Should().Equal('b');
        reader.Peek(1).Should().Equal('c');
        reader.Peek(2).Should().Equal('\0');
    }

    [Test]
    public void Advance()
    {
        var reader = new SourceReader(new TextFile("Test", "ab\ncd"));

        reader.Advance();
        reader.Index.Should().Equal(1);
        reader.LineIndex.Should().Equal(0);
        reader.ColumnIndex.Should().Equal(1);
        reader.Current.Should().Equal('b');

        reader.Advance();
        reader.Current.Should().Equal('\n');
        reader.ColumnIndex.Should().Equal(2);

        reader.Advance();
        reader.Index.Should().Equal(3);
        reader.LineIndex.Should().Equal(1);
        reader.ColumnIndex.Should().Equal(0);
        reader.Current.Should().Equal('c');
    }

    [Test]
    public void Advance_CarriageReturnLineFeed()
    {
        var reader = new SourceReader(new TextFile("Test", "a\r\nb"));

        reader.Advance();
        reader.Advance();
        reader.LineIndex.Should().Equal(0);
        reader.ColumnIndex.Should().Equal(2);

        reader.Advance();
        reader.LineIndex.Should().Equal(1);
        reader.ColumnIndex.Should().Equal(0);
        reader.Current.Should().Equal('b');
    }

    [Test]
    public void Advance_LoneCarriageReturn()
    {
        var reader = new SourceReader(new TextFile("Test", "a\rb"));

        reader.Advance();
        reader.Advance();
        reader.LineIndex.Should().Equal(1);
        reader.ColumnIndex.Should().Equal(0);
        reader.Current.Should().Equal('b');
    }

    [Test]
    public void Advance_CarriageReturnAtEndOfFile()
    {
        var reader = new SourceReader(new TextFile("Test", "a\r"));

        reader.Advance();
        reader.Advance();
        reader.LineIndex.Should().Equal(1);
        reader.ColumnIndex.Should().Equal(0);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void Advance_AtEndOfFile()
    {
        var reader = new SourceReader(new TextFile("Test", "a"));

        reader.Advance();
        reader.AtEnd.Should().BeTrue();

        reader.Advance();
        reader.Index.Should().Equal(1);
        reader.LineIndex.Should().Equal(0);
        reader.ColumnIndex.Should().Equal(1);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void Advance_Count()
    {
        var reader = new SourceReader(new TextFile("Test", "abcd"));

        reader.Advance(2);
        reader.Index.Should().Equal(2);
        reader.Current.Should().Equal('c');
    }

    [Test]
    public void Advance_Count_StopsAtEndOfFile()
    {
        var reader = new SourceReader(new TextFile("Test", "ab"));

        reader.Advance(5);
        reader.Index.Should().Equal(2);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void Advance_Count_Negative()
    {
        var reader = new SourceReader(new TextFile("Test", "ab"));

        reader.Invoking(r => r.Advance(-1))
            .Should().Throw<ArgumentOutOfRangeException>()
            .That.Should().HaveParamName("count");
    }

    [Test]
    public void AdvanceWhile()
    {
        var reader = new SourceReader(new TextFile("Test", "123abc"));

        reader.AdvanceWhile(char.IsAsciiDigit).Should().Equal(3);
        reader.Index.Should().Equal(3);
        reader.Current.Should().Equal('a');
    }

    [Test]
    public void AdvanceWhile_PredicateImmediatelyFalse()
    {
        var reader = new SourceReader(new TextFile("Test", "abc"));

        reader.AdvanceWhile(char.IsAsciiDigit).Should().Equal(0);
        reader.Index.Should().Equal(0);
    }

    [Test]
    public void AdvanceWhile_StopsAtEndOfFile()
    {
        var reader = new SourceReader(new TextFile("Test", "123"));

        reader.AdvanceWhile(char.IsAsciiDigit).Should().Equal(3);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void Mark()
    {
        var reader = new SourceReader(new TextFile("Test", "ab\ncd"));
        reader.Advance(4);

        var mark = reader.Mark();
        mark.Index.Should().Equal(4);
        mark.LineIndex.Should().Equal(1);
        mark.ColumnIndex.Should().Equal(1);
    }

    [Test]
    public void ReadToken()
    {
        var file = new TextFile("Test", "a<<b");
        var reader = new SourceReader(file);
        reader.Advance();

        var token = reader.ReadToken(TestTokenKind.Symbol, 2);
        token.Kind.Should().Equal(TestTokenKind.Symbol);
        token.File.Should().BeTheSameInstanceAs(file);
        token.StartIndex.Should().Equal(1);
        token.Length.Should().Equal(2);
        token.StartLineIndex.Should().Equal(0);
        token.StartColumnIndex.Should().Equal(1);
        token.Text.ToString().Should().Equal("<<");

        reader.Index.Should().Equal(3);
        reader.Current.Should().Equal('b');
    }

    [Test]
    public void ReadToken_StopsAtEndOfFile()
    {
        var reader = new SourceReader(new TextFile("Test", "ab"));
        reader.Advance();

        var token = reader.ReadToken(TestTokenKind.Word, 5);
        token.StartIndex.Should().Equal(1);
        token.Length.Should().Equal(1);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void ReadToken_Negative()
    {
        var reader = new SourceReader(new TextFile("Test", "ab"));

        reader.Invoking(r => _ = r.ReadToken(TestTokenKind.Word, -1))
            .Should().Throw<ArgumentOutOfRangeException>()
            .That.Should().HaveParamName("length");
    }

    [Test]
    public void CreateToken()
    {
        var file = new TextFile("Test", "ab\n123 x");
        var reader = new SourceReader(file);
        reader.Advance(3);

        var mark = reader.Mark();
        reader.AdvanceWhile(char.IsAsciiDigit);

        var token = reader.CreateToken(TestTokenKind.Number, mark);
        token.Kind.Should().Equal(TestTokenKind.Number);
        token.File.Should().BeTheSameInstanceAs(file);
        token.StartIndex.Should().Equal(3);
        token.Length.Should().Equal(3);
        token.StartLineIndex.Should().Equal(1);
        token.StartColumnIndex.Should().Equal(0);
        token.Text.ToString().Should().Equal("123");
    }
}