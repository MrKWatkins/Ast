namespace MrKWatkins.Ast.Enumeration;

public interface IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    [Pure]
    IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true);
}