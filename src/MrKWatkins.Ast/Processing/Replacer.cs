namespace MrKWatkins.Ast.Processing;

/// <summary>
/// An <see cref="OrderedProcessor{TNode}" /> for optionally replacing nodes in a tree.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public abstract class Replacer<TNode> : OrderedProcessor<TNode>
    where TNode : Node<TNode>
{
    /// <inheritdoc />
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

            if (ProcessReplacements)
            {
                Process(newNode);
            }
        }
    }

    /// <summary>
    /// Optionally replace the specified node. 
    /// </summary>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// A new node to replace <paramref name="node" /> in the tree. Return <paramref name="node" /> or <c>null</c> to leave <paramref name="node" /> in the tree.
    /// </returns>
    [Pure]
    protected abstract TNode? ReplaceNode(TNode node);

    /// <summary>
    /// If set to <c>true</c> then replacement nodes and their children will be processed too. Defaults to <c>false</c>.
    /// </summary>
    protected virtual bool ProcessReplacements => false;
}

/// <summary>
/// An <see cref="OrderedProcessor{TNode}" /> for optionally replacing nodes of a specific type in a tree.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
public abstract class Replacer<TBaseNode, TNode> : OrderedProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
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

            if (ProcessReplacements)
            {
                Process(newNode);
            }
        }
    }

    /// <summary>
    /// Optionally replace the specified node. 
    /// </summary>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// A new node to replace <paramref name="node" /> in the tree. Return <paramref name="node" /> or <c>null</c> to leave <paramref name="node" /> in the tree.
    /// </returns>
    [Pure]
    protected abstract TBaseNode? ReplaceNode(TNode node);

    /// <summary>
    /// If set to <c>true</c> then replacement nodes and their children will be processed too. Defaults to <c>false</c>.
    /// </summary>
    protected virtual bool ProcessReplacements => false;
}

/// <summary>
/// An <see cref="OrderedProcessor{TNode}" /> for optionally replacing nodes of a specific type in a tree with new nodes of a specific type.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
/// <typeparam name="TReplacementNode">The type of the replacement nodes.</typeparam>
public abstract class Replacer<TBaseNode, TNode, TReplacementNode> : OrderedProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
    where TReplacementNode : TBaseNode
{
    /// <inheritdoc />
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

            if (ProcessReplacements)
            {
                Process(newNode);
            }
        }
    }

    /// <summary>
    /// Optionally replace the specified node. 
    /// </summary>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// A new node to replace <paramref name="node" /> in the tree. Return <paramref name="node" /> or <c>null</c> to leave <paramref name="node" /> in the tree.
    /// </returns>
    [Pure]
    protected abstract TReplacementNode? ReplaceNode(TNode node);

    /// <summary>
    /// If set to <c>true</c> then replacement nodes and their children will be processed too. Defaults to <c>false</c>.
    /// </summary>
    protected virtual bool ProcessReplacements => false;
}