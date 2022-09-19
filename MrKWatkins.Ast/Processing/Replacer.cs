namespace MrKWatkins.Ast.Processing;

public abstract class Replacer<TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    protected internal override void ProcessNode(TNode node)
    {
        var newNode = ReplaceNode(node);
        if (newNode != null && !ReferenceEquals(node, newNode))
        {
            if (newNode.HasParent)
            {
                throw new InvalidOperationException($"Replacement node {newNode} already has a parent {newNode.Parent}.");
            }
            node.ReplaceWith(newNode);
        }
    }

    [Pure]
    protected abstract TNode? ReplaceNode(TNode node);
}

public abstract class Replacer<TNode, TBaseNode> : Processor<TNode, TBaseNode>
    where TNode : TBaseNode
    where TBaseNode : Node<TBaseNode>
{
    protected override void ProcessNode(TNode node)
    {
        var newNode = ReplaceNode(node);
        if (newNode != null && !ReferenceEquals(node, newNode))
        {
            if (newNode.HasParent)
            {
                throw new InvalidOperationException($"Replacement node {newNode} already has a parent {newNode.Parent}.");
            }
            node.ReplaceWith(newNode);
        }
    }

    [Pure]
    protected abstract TBaseNode? ReplaceNode(TNode node);
}

public abstract class Replacer<TOriginalNode, TReplacementNode, TBaseNode> : Processor<TOriginalNode, TBaseNode>
    where TOriginalNode : TBaseNode
    where TReplacementNode : TBaseNode
    where TBaseNode : Node<TBaseNode>
{
    protected override void ProcessNode(TOriginalNode node)
    {
        var newNode = ReplaceNode(node);
        if (newNode != null && !ReferenceEquals(node, newNode))
        {
            if (newNode.HasParent)
            {
                throw new InvalidOperationException($"Replacement node {newNode} already has a parent {newNode.Parent}.");
            }
            node.ReplaceWith(newNode);
        }
    }

    [Pure]
    protected abstract TReplacementNode? ReplaceNode(TOriginalNode node);
}