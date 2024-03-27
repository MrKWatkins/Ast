namespace MrKWatkins.Ast.Processing;

internal sealed class ProcessorState<TNode> : IDisposable
    where TNode : Node<TNode>
{
    private readonly object? context;

    internal ProcessorState(Exceptions exceptions, Action<TNode> processNodeIfShould, object? context = null)
    {
        Exceptions = exceptions;
        ProcessNodeIfShould = processNodeIfShould;
        this.context = context;
    }

    internal Exceptions Exceptions { get; }

    internal Action<TNode> ProcessNodeIfShould { get; }

    internal Action<ProcessorState<TNode>>? OnComplete { get; init; }

    public void Dispose()
    {
        if (context is IDisposable disposableContext)
        {
            disposableContext.Dispose();
        }
    }

    [Pure]
    internal static ProcessorState<TNode> Create(Func<TNode, bool> shouldProcessNode, Action<TNode> processNode)
    {
        var exceptions = new Exceptions();

        return new ProcessorState<TNode>(
            exceptions,
            node =>
            {
                if (exceptions.Trap(node, "ShouldProcessNode", shouldProcessNode))
                {
                    exceptions.Trap(node, "ProcessNode", processNode);
                }
            });
    }

    [Pure]
    internal static ProcessorState<TNode> Create<TTypedNode>(Func<TTypedNode, bool> shouldProcessNode, Action<TTypedNode> processNode)
        where TTypedNode : TNode
    {
        var exceptions = new Exceptions();

        return new ProcessorState<TNode>(
            exceptions,
            node =>
            {
                if (node is TTypedNode typedNode && exceptions.Trap<TNode, TTypedNode>(typedNode, "ShouldProcessNode", shouldProcessNode))
                {
                    exceptions.Trap<TNode, TTypedNode>(typedNode, "ProcessNode", processNode);
                }
            });
    }
}