namespace MrKWatkins.Ast.Listening;

public interface ICompositeListenerBuilder<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> With(Listener<TBaseNode> listener);
    
    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> With<TNode>(Listener<TBaseNode, TNode> listener)
        where TNode : TBaseNode;

    [Pure]
    CompositeListener<TBaseNode> ToListener();
}