using System.Collections.Concurrent;
using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class TestOrderedProcessor : OrderedProcessor<TestNode>
{
    private readonly ConcurrentQueue<TestNode> processed = new();

    public Action<TestNode>? ProcessNodeOverride { get; init; }
    public Func<TestNode, bool>? ShouldProcessDescendentsOverride { get; init; }
    public ITraversal<TestNode>? TraversalOverride { get; init; }

    public IEnumerable<TestNode> Processed => processed;

    public override void Process(TestNode node)
    {
        processed.Enqueue(node);
        ProcessNodeOverride?.Invoke(node);
    }

    public override bool ShouldProcessDescendents(TestNode node) => ShouldProcessDescendentsOverride?.Invoke(node) ?? base.ShouldProcessDescendents(node);

    public override ITraversal<TestNode> GetTraversal(TestNode root) => TraversalOverride ?? base.GetTraversal(root);
}

public sealed class TestOrderedProcessor<TContext>(TContext? expectedContext) : OrderedProcessor<TContext, TestNode>
    where TContext : class
{
    public TestOrderedProcessor()
        : this(null)
    {
    }

    private readonly ConcurrentQueue<TestNode> processed = new();

    public Action<TestNode>? ProcessNodeOverride { get; init; }
    public Func<TestNode, bool>? ShouldProcessDescendentsOverride { get; init; }
    public ITraversal<TestNode>? TraversalOverride { get; init; }

    public IEnumerable<TestNode> Processed => processed;

    public override void Process(TContext context, TestNode node)
    {
        if (expectedContext != null)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
        }
        processed.Enqueue(node);
        ProcessNodeOverride?.Invoke(node);
    }

    public override bool ShouldProcessDescendents(TContext context, TestNode node)
    {
        context.Should().BeTheSameInstanceAs(expectedContext);
        return ShouldProcessDescendentsOverride?.Invoke(node) ?? base.ShouldProcessDescendents(context, node);
    }

    public override ITraversal<TestNode> GetTraversal(TContext context, TestNode root)
    {
        context.Should().BeTheSameInstanceAs(expectedContext);
        return TraversalOverride ?? base.GetTraversal(context, root);
    }
}