namespace MrKWatkins.Ast.Tests;

// TODO: FirstOrThrow tests.
public sealed class EnumerableExtensionsTests
{
    [Test]
    public void DuplicateGroupsBy_ReturnsTheExpectedGroups()
    {
        var source = new[] { "One", "Two", "Three", "Four", "Five" };

        var expected = new[] { new[] { "One", "Two" }, new[] { "Four", "Five" } };

        source.DuplicateGroupsBy(s => s.Length).Should().BeEquivalentTo(expected);
    }
        
    [Test]
    public void DuplicatesBy_ReturnsTheExpectedGroups()
    {
        var source = new[] { "One", "Two", "Three", "Four", "Five" };

        var expected = new[] { "One", "Two", "Four", "Five" };

        source.DuplicatesBy(s => s.Length).Should().BeEquivalentTo(expected);
    }
}