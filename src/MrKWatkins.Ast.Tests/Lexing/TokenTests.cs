using MrKWatkins.Ast.Lexing;
using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Lexing;

public sealed class TokenTests : EqualityTestFixture
{
    [Test]
    public void Constructor()
    {
        var file = new TextFile("Test", "Some test code.");

        var token = new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 0, 5);
        token.Kind.Should().Equal(TestTokenKind.Word);
        token.File.Should().BeTheSameInstanceAs(file);
        token.StartIndex.Should().Equal(5);
        token.Length.Should().Equal(4);
        token.StartLineIndex.Should().Equal(0);
        token.StartColumnIndex.Should().Equal(5);
    }

    [Test]
    public void Text()
    {
        var file = new TextFile("Test", "Some test code.");

        var token = new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 0, 5);
        token.Text.ToString().Should().Equal("test");
    }

    [Test]
    public void Position()
    {
        var file = new TextFile("Test", "One\nTwo");

        var token = new Token<TestTokenKind>(TestTokenKind.Word, file, 4, 3, 1, 0);

        var position = token.Position;
        position.File.Should().BeTheSameInstanceAs(file);
        position.StartIndex.Should().Equal(4);
        position.Length.Should().Equal(3);
        position.StartLineIndex.Should().Equal(1);
        position.StartColumnIndex.Should().Equal(0);
        position.Text.Should().Equal("Two");
    }

    [Test]
    public void Position_StartsOnLineTerminator()
    {
        var file = new TextFile("Test", "ab\ncd");

        // A token for the "\n"; its column is past the end of the line "ab" so is clamped to the last column.
        var token = new Token<TestTokenKind>(TestTokenKind.Symbol, file, 2, 1, 0, 2);

        var position = token.Position;
        position.StartIndex.Should().Equal(2);
        position.Length.Should().Equal(1);
        position.StartLineIndex.Should().Equal(0);
        position.StartColumnIndex.Should().Equal(1);
    }

    [Test]
    public void Position_StartsOnLineTerminator_EmptyLine()
    {
        var file = new TextFile("Test", "\nx");

        var token = new Token<TestTokenKind>(TestTokenKind.Symbol, file, 0, 1, 0, 0);

        var position = token.Position;
        position.StartLineIndex.Should().Equal(0);
        position.StartColumnIndex.Should().Equal(0);
    }

    [Test]
    public void Position_ZeroLengthAtEndOfFile()
    {
        var file = new TextFile("Test", "ab");

        var token = new Token<TestTokenKind>(TestTokenKind.EndOfFile, file, 2, 0, 0, 2);

        var position = token.Position;
        position.StartIndex.Should().Equal(1);
        position.Length.Should().Equal(0);
        position.StartLineIndex.Should().Equal(0);
        position.StartColumnIndex.Should().Equal(1);
    }

    [Test]
    public void Position_ZeroLengthAtEndOfFile_AfterTrailingNewLine()
    {
        var file = new TextFile("Test", "ab\n");

        // The token's line is past the last line in the file so is clamped to the end of the last line.
        var token = new Token<TestTokenKind>(TestTokenKind.EndOfFile, file, 3, 0, 1, 0);

        var position = token.Position;
        position.StartLineIndex.Should().Equal(0);
        position.StartColumnIndex.Should().Equal(1);
    }

    [Test]
    public void Position_ZeroLengthAtEndOfFile_AfterTrailingNewLine_EmptyLastLine()
    {
        var file = new TextFile("Test", "ab\n\n");

        var token = new Token<TestTokenKind>(TestTokenKind.EndOfFile, file, 4, 0, 2, 0);

        var position = token.Position;
        position.StartLineIndex.Should().Equal(1);
        position.StartColumnIndex.Should().Equal(0);
    }

    [Test]
    public void Position_EmptyFile()
    {
        var file = new TextFile("Test", "");

        var token = new Token<TestTokenKind>(TestTokenKind.EndOfFile, file, 0, 0, 0, 0);

        token.Invoking(t => t.Position)
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("The file is empty so the token does not have a position.");
    }

    [Test]
    public void ToString_WithText()
    {
        var file = new TextFile("Test", "Some test code.");

        var token = new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 0, 5);
        token.ToString().Should().Equal("Word \"test\"");
    }

    [Test]
    public void ToString_ZeroLength()
    {
        var file = new TextFile("Test", "Some test code.");

        var token = new Token<TestTokenKind>(TestTokenKind.EndOfFile, file, 15, 0, 0, 14);
        token.ToString().Should().Equal("EndOfFile");
    }

    [Test]
    public void Equality()
    {
        var file = new TextFile("Test", "Some test code.");
        var token = new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 0, 5);

        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 0, 5), true);
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Number, file, 5, 4, 0, 5), false);
        // TextFile has value equality based on its name, so a token from a file with the same name is equal.
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, new TextFile("Test", "Some test code."), 5, 4, 0, 5), true);
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, new TextFile("Other", "Some test code."), 5, 4, 0, 5), false);
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, file, 6, 4, 0, 6), false);
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 3, 0, 5), false);
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 1, 5), false);
        AssertEqual(token, new Token<TestTokenKind>(TestTokenKind.Word, file, 5, 4, 0, 6), false);
    }
}