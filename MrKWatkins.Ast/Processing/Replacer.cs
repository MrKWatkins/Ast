namespace MrKWatkins.Ast.Processing;

public abstract class Replacer<TNode> : OrderedProcessor<TNode>
    where TNode : Node<TNode>
{
    protected sealed override void ProcessNode(TNode node)
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

public abstract class Replacer<TBaseNode, TNode> : OrderedProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    protected sealed override void ProcessNode(TNode node)
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

public abstract class Replacer<TBaseNode, TNode, TReplacementNode> : OrderedProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
    where TReplacementNode : TBaseNode
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
    protected abstract TReplacementNode? ReplaceNode(TNode node);
}