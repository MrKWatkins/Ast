namespace MrKWatkins.Ast;

public interface INodeFactory<in TType, out TNode>
    where TType : Enum
    where TNode : Node<TType, TNode>
{
    [Pure]
    TNode Create(TType nodeType);
}