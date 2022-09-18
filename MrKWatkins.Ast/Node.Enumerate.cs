namespace MrKWatkins.Ast;

public  abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    public static class Enumerate
    {
        [Pure]
        public static IEnumerable<TNode> BreadthFirst(TNode root, bool includeRoot = true) => 
            Enumeration.BreadthFirst<TNode>.Instance.Enumerate(root, includeRoot);
        
        [Pure]
        public static IEnumerable<TNode> DepthFirstPreOrder(TNode root, bool includeRoot = true) => 
            Enumeration.DepthFirstPreOrder<TNode>.Instance.Enumerate(root, includeRoot);
        
        [Pure]
        public static IEnumerable<TNode> DepthFirstPostOrder(TNode root, bool includeRoot = true) => 
            Enumeration.DepthFirstPostOrder<TNode>.Instance.Enumerate(root, includeRoot);
    }
}