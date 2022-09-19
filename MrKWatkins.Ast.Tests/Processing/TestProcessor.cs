using System.Collections.Concurrent;
using MrKWatkins.Ast.Enumeration;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class TestProcessor : Processor<TestNode>
{
    private readonly ConcurrentQueue<TestNode> processed = new();

    public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
    public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }
    public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }
    public IDescendentEnumerator<TestNode>? EnumeratorOverride { get; init; }

    public IEnumerable<TestNode> Processed => processed;

    protected internal override IDescendentEnumerator<TestNode> Enumerator => EnumeratorOverride ?? base.Enumerator;

    protected internal override void ProcessNode(TestNode node)
    {
        processed.Enqueue(node);
        ProcessNodeOverride?.Invoke(node);
    }

    protected internal override bool ShouldProcessNode(TestNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);

    protected internal override bool ShouldProcessChildren(TestNode node) => ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(node);
}