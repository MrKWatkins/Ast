using System.Collections.Concurrent;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class TestUnorderedProcessor : UnorderedProcessor<TestNode>
{
    private readonly ConcurrentQueue<TestNode> processed = new();

    public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
    public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }

    public IEnumerable<TestNode> Processed => processed;

    protected override void ProcessNode(TestNode node)
    {
        processed.Enqueue(node);
        ProcessNodeOverride?.Invoke(node);
    }

    protected override bool ShouldProcessNode(TestNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);
}