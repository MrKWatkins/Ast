namespace MrKWatkins.Ast;

public  abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    public static class Enumerate
    {
        [Pure]
        public static IEnumerable<TNode> BreadthFirst(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldProcessChildren = null) => 
            Enumeration.BreadthFirst<TNode>.Instance.Enumerate(root, includeRoot, shouldProcessChildren);
        
        [Pure]
        public static IEnumerable<TNode> DepthFirstPreOrder(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldProcessChildren = null) => 
            Enumeration.DepthFirstPreOrder<TNode>.Instance.Enumerate(root, includeRoot, shouldProcessChildren);
        
        [Pure]
        public static IEnumerable<TNode> DepthFirstPostOrder(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldProcessChildren = null) => 
            Enumeration.DepthFirstPostOrder<TNode>.Instance.Enumerate(root, includeRoot, shouldProcessChildren);
    }
}