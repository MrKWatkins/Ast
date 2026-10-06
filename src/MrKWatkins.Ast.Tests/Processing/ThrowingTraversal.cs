using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

/// <summary>
/// A traversal that throws, to test exception handling around traversing the tree. Throws either when <see cref="Enumerate" /> is called
/// or lazily when the result is enumerated.
/// </summary>
public sealed class ThrowingTraversal(Exception exception, bool lazily = true) : ITraversal<TestNode>
{
    public IEnumerable<TestNode> Enumerate(TestNode root, bool includeRoot = true, Func<TestNode, bool>? shouldEnumerateDescendants = null) =>
        lazily ? EnumerateLazily() : throw exception;

    private IEnumerable<TestNode> EnumerateLazily()
    {
        throw exception;
#pragma warning disable CS0162 // Unreachable code detected; the yield makes this an iterator so the throw happens on enumeration rather than on the call.
        yield break;
#pragma warning restore CS0162
    }
}