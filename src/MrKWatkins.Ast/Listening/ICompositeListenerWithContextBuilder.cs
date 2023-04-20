namespace MrKWatkins.Ast.Listening;

public interface ICompositeListenerWithContextBuilder<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    [MustUseReturnValue]
    ICompositeListenerWithContextBuilder<TContext, TBaseNode> With(ListenerWithContext<TContext, TBaseNode> listener);
    
    [MustUseReturnValue]
    ICompositeListenerWithContextBuilder<TContext, TBaseNode> With<TNode>(ListenerWithContext<TContext, TBaseNode, TNode> listener)
        where TNode : TBaseNode;

    [Pure]
    CompositeListenerWithContext<TContext, TBaseNode> ToListener();
}