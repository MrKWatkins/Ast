namespace MrKWatkins.Ast;

public interface INodeFactory<TNode>
    where TNode : Node<TNode>
{
    [Pure]
    TNode Create(Type nodeType);

    [Pure]
    T Create<T>()
        where T : TNode
        => (T) Create(typeof(T));
}