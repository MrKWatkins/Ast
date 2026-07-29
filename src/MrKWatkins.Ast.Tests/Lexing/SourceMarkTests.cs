using MrKWatkins.Ast.Lexing;

namespace MrKWatkins.Ast.Tests.Lexing;

public sealed class SourceMarkTests : EqualityTestFixture
{
    [Test]
    public void Constructor()
    {
        var mark = new SourceMark(10, 2, 3);
        mark.Index.Should().Equal(10);
        mark.LineIndex.Should().Equal(2);
        mark.ColumnIndex.Should().Equal(3);
    }

    [Test]
    public void Equality()
    {
        var mark = new SourceMark(10, 2, 3);

        AssertEqual(mark, new SourceMark(10, 2, 3), true);
        AssertEqual(mark, new SourceMark(11, 2, 3), false);
        AssertEqual(mark, new SourceMark(10, 3, 3), false);
        AssertEqual(mark, new SourceMark(10, 2, 4), false);
    }

    [Test]
    public void ToString_Members() => new SourceMark(10, 2, 3).ToString().Should().Equal("SourceMark { Index = 10, LineIndex = 2, ColumnIndex = 3 }");
}