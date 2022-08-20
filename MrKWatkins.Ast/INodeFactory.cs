namespace MrKWatkins.Ast;

public interface INodeFactory<out TNode>
    where TNode : Node<TNode>
{
    [Pure]
    TNode Create(Type nodeType);

    [Pure]
    TNode Create<T>() => Create(typeof(T));
}