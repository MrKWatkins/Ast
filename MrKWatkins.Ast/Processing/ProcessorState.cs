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

    [Pure]
    internal TContext GetContext<TContext>() => (TContext) context!;
    
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

        return new(
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

        return new(
            exceptions,
            node =>
        {
            if (node is TTypedNode typedNode && exceptions.Trap<TNode, TTypedNode>(typedNode, "ShouldProcessNode", shouldProcessNode))
            {
                exceptions.Trap<TNode, TTypedNode>(typedNode, "ProcessNode", processNode);
            }
        });
    }

    [Pure]
    internal static ProcessorState<TNode> CreateWithContext<TContext>(TContext context, Func<TContext, TNode, bool> shouldProcessNode, Action<TContext, TNode> processNode)
    {
        var exceptions = new Exceptions();

        return new(exceptions,
            node =>
            {
                if (exceptions.Trap(context, node, "ShouldProcessNode", shouldProcessNode))
                {
                    exceptions.Trap(context, node, "ProcessNode", processNode);
                }
            }, 
            context);
    }

    [Pure]
    internal static ProcessorState<TNode> CreateWithContext<TContext, TTypedNode>(TContext context, Func<TContext, TTypedNode, bool> shouldProcessNode, Action<TContext, TTypedNode> processNode)
        where TTypedNode : TNode
    {
        var exceptions = new Exceptions();

        return new(exceptions,
            node =>
            {
                if (node is TTypedNode typedNode && exceptions.Trap<TContext, TNode, TTypedNode>(context, typedNode, "ShouldProcessNode", shouldProcessNode))
                {
                    exceptions.Trap<TContext, TNode, TTypedNode>(context, typedNode, "ProcessNode", processNode);
                }
            },
            context);
    }
}