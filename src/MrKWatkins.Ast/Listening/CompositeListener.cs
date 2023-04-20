namespace MrKWatkins.Ast.Listening;

public sealed class CompositeListener<TBaseNode> : Listener<TBaseNode>, ICompositeListenerBuilder<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly ListenerLookup<TBaseNode, Listener<TBaseNode>> listeners = new();
    
    [Pure]
    public static ICompositeListenerBuilder<TBaseNode> Build() => new CompositeListener<TBaseNode>();

    [Pure]
    public static ICompositeListenerWithContextBuilder<TContext, TBaseNode> BuildWithContext<TContext>() => new CompositeListenerWithContext<TContext, TBaseNode>();
    
    private CompositeListener()
    {
    }

    protected internal override void BeforeListenToNode(TBaseNode node) => listeners.Get(node)?.BeforeListenToNode(node);
    
    protected internal override void ListenToNode(TBaseNode node) => listeners.Get(node)?.ListenToNode(node);
    
    protected internal override void AfterListenToNode(TBaseNode node) => listeners.Get(node)?.AfterListenToNode(node);

    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> ICompositeListenerBuilder<TBaseNode>.With(Listener<TBaseNode> listener)
    {
        listeners.Add<TBaseNode>(listener);

        return this;
    }

    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> ICompositeListenerBuilder<TBaseNode>.With<TNode>(Listener<TBaseNode, TNode> listener) 
    {
        listeners.Add<TNode>(listener);

        return this;
    }

    [Pure]
    CompositeListener<TBaseNode> ICompositeListenerBuilder<TBaseNode>.ToListener()
    {
        if (listeners.Count == 0)
        {
            throw new InvalidOperationException("No listeners have been registered.");
        }
        return this;
    }
}