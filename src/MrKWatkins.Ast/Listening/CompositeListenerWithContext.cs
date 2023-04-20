namespace MrKWatkins.Ast.Listening;

public sealed class CompositeListenerWithContext<TContext, TBaseNode> : ListenerWithContext<TContext, TBaseNode>, ICompositeListenerWithContextBuilder<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly ListenerLookup<TBaseNode, ListenerWithContext<TContext, TBaseNode>> listeners = new();

    [Pure]
    public static ICompositeListenerWithContextBuilder<TContext, TBaseNode> Build() => new CompositeListenerWithContext<TContext, TBaseNode>();

    internal CompositeListenerWithContext()
    {
    }

    protected internal override void BeforeListenToNode(TContext context, TBaseNode node) => listeners.Get(node)?.BeforeListenToNode(context, node);
    
    protected internal override void ListenToNode(TContext context, TBaseNode node) => listeners.Get(node)?.ListenToNode(context, node);
    
    protected internal override void AfterListenToNode(TContext context, TBaseNode node) => listeners.Get(node)?.AfterListenToNode(context, node);

    [MustUseReturnValue]
    ICompositeListenerWithContextBuilder<TContext, TBaseNode> ICompositeListenerWithContextBuilder<TContext, TBaseNode>.With(ListenerWithContext<TContext, TBaseNode> listener) 
    {
        listeners.Add<TBaseNode>(listener);

        return this;
    }

    [MustUseReturnValue]
    ICompositeListenerWithContextBuilder<TContext, TBaseNode> ICompositeListenerWithContextBuilder<TContext, TBaseNode>.With<TNode>(ListenerWithContext<TContext, TBaseNode, TNode> listener) 
    {
        listeners.Add<TNode>(listener);

        return this;
    }

    [Pure]
    CompositeListenerWithContext<TContext, TBaseNode> ICompositeListenerWithContextBuilder<TContext, TBaseNode>.ToListener()
    {
        if (listeners.Count == 0)
        {
            throw new InvalidOperationException("No listeners have been registered.");
        }
        return this;
    }
}