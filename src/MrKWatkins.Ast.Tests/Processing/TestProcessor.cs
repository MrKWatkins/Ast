using System.Collections.Concurrent;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class TestProcessor : Processor<TestNode>
{
    private readonly ConcurrentQueue<TestNode> processed = new();

    public Action<TestNode>? ProcessNodeOverride { get; init; }

    public IEnumerable<TestNode> Processed => processed;

    public override void Process(TestNode node)
    {
        ProcessNodeOverride?.Invoke(node);
        processed.Enqueue(node);
    }
}

public sealed class TestProcessor<TContext>(TContext? expectedContext) : Processor<TContext, TestNode>
    where TContext : class
{
    public TestProcessor()
        : this(null)
    {
    }

    private readonly ConcurrentQueue<TestNode> processed = new();

    public Action<TestNode>? ProcessNodeOverride { get; init; }

    public IEnumerable<TestNode> Processed => processed;

    public override void Process(TContext context, TestNode node)
    {
        if (expectedContext != null)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
        }
        ProcessNodeOverride?.Invoke(node);
        processed.Enqueue(node);
    }
}