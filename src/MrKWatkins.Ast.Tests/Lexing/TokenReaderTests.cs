using MrKWatkins.Ast.Lexing;
using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Lexing;

public sealed class TokenReaderTests
{
    [Test]
    public void Constructor_Empty() =>
        AssertThat.Invoking(() => new TokenReader<TestTokenKind>([]))
            .Should().Throw<ArgumentException>()
            .That.Should().HaveParamName("tokens");

    [Test]
    public void Count() => CreateReader().Count.Should().Equal(4);

    [Test]
    public void Current()
    {
        var reader = CreateReader();
        reader.Current.Kind.Should().Equal(TestTokenKind.Word);

        reader.Advance();
        reader.Current.Kind.Should().Equal(TestTokenKind.Number);
    }

    [Test]
    public void AtEnd()
    {
        var reader = CreateReader();
        reader.AtEnd.Should().BeFalse();

        reader.Advance();
        reader.Advance();
        reader.AtEnd.Should().BeFalse();

        reader.Advance();
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void Peek()
    {
        var reader = CreateReader();
        reader.Advance();

        reader.Peek(-2).Kind.Should().Equal(TestTokenKind.Word);
        reader.Peek(-1).Kind.Should().Equal(TestTokenKind.Word);
        reader.Peek(0).Kind.Should().Equal(TestTokenKind.Number);
        reader.Peek(1).Kind.Should().Equal(TestTokenKind.Symbol);
        reader.Peek(2).Kind.Should().Equal(TestTokenKind.EndOfFile);
        reader.Peek(3).Kind.Should().Equal(TestTokenKind.EndOfFile);
    }

    [Test]
    public void Advance()
    {
        var reader = CreateReader();

        reader.Advance();
        reader.Position.Should().Equal(1);
    }

    [Test]
    public void Advance_StopsAtLastToken()
    {
        var reader = CreateReader();

        reader.Advance();
        reader.Advance();
        reader.Advance();
        reader.Position.Should().Equal(3);
        reader.AtEnd.Should().BeTrue();

        reader.Advance();
        reader.Position.Should().Equal(3);
    }

    [Test]
    public void Position()
    {
        var reader = CreateReader();
        reader.Advance();
        reader.Advance();
        reader.Position.Should().Equal(2);

        reader.Position = 0;
        reader.Current.Kind.Should().Equal(TestTokenKind.Word);
    }

    [Test]
    public void Position_Negative()
    {
        var reader = CreateReader();

        reader.Invoking(r => r.Position = -1)
            .Should().Throw<ArgumentOutOfRangeException>()
            .That.Should().HaveParamName("value");
    }

    [Test]
    public void Position_BeyondLastToken()
    {
        var reader = CreateReader();

        reader.Invoking(r => r.Position = 4)
            .Should().Throw<ArgumentOutOfRangeException>()
            .That.Should().HaveParamName("value");
    }

    [Test]
    public void TryConsume()
    {
        var reader = CreateReader();

        reader.TryConsume(TestTokenKind.Word, out var token).Should().BeTrue();
        token.Kind.Should().Equal(TestTokenKind.Word);
        reader.Position.Should().Equal(1);
    }

    [Test]
    public void TryConsume_WrongKind()
    {
        var reader = CreateReader();

        reader.TryConsume(TestTokenKind.Number, out var token).Should().BeFalse();
        token.Should().Equal(default(Token<TestTokenKind>));
        reader.Position.Should().Equal(0);
    }

    [Test]
    public void SkipUntil()
    {
        var reader = CreateReader();

        reader.SkipUntil(TestTokenKind.Symbol);
        reader.Position.Should().Equal(2);
        reader.Current.Kind.Should().Equal(TestTokenKind.Symbol);
    }

    [Test]
    public void SkipUntil_MultipleKinds()
    {
        var reader = CreateReader();

        reader.SkipUntil(TestTokenKind.Symbol, TestTokenKind.Number);
        reader.Position.Should().Equal(1);
        reader.Current.Kind.Should().Equal(TestTokenKind.Number);
    }

    [Test]
    public void SkipUntil_AlwaysAdvances()
    {
        var reader = CreateReader();
        reader.Current.Kind.Should().Equal(TestTokenKind.Word);

        // The current token is not counted so recovery is guaranteed to make progress.
        reader.SkipUntil(TestTokenKind.Word);
        reader.Position.Should().Equal(3);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void SkipUntil_KindNotFound()
    {
        var reader = CreateReader();

        reader.SkipUntil((TestTokenKind) 100);
        reader.Position.Should().Equal(3);
        reader.AtEnd.Should().BeTrue();
    }

    [Test]
    public void SkipUntil_AtEnd()
    {
        var reader = CreateReader();
        reader.Position = 3;

        reader.SkipUntil(TestTokenKind.Word);
        reader.Position.Should().Equal(3);
    }

    [Pure]
    private static TokenReader<TestTokenKind> CreateReader()
    {
        var file = new TextFile("Test", "one 2 !");

        return new TokenReader<TestTokenKind>(
        [
            new Token<TestTokenKind>(TestTokenKind.Word, file, 0, 3, 0, 0),
            new Token<TestTokenKind>(TestTokenKind.Number, file, 4, 1, 0, 4),
            new Token<TestTokenKind>(TestTokenKind.Symbol, file, 6, 1, 0, 6),
            new Token<TestTokenKind>(TestTokenKind.EndOfFile, file, 7, 0, 0, 6)
        ]);
    }
}