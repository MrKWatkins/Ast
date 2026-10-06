namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Shared implementation of node replacement for the replacer processors.
/// </summary>
internal static class Replacement
{
    /// <summary>
    /// Replaces <paramref name="node" /> with <paramref name="replacement" /> if it is a different node. Returns the node the pipeline should
    /// treat as the result: the replacement if the root was replaced, otherwise the original node.
    /// </summary>
    internal static TBaseNode Apply<TBaseNode>(TBaseNode node, TBaseNode? replacement)
        where TBaseNode : Node<TBaseNode>
    {
        if (replacement == null || ReferenceEquals(node, replacement))
        {
            return node;
        }

        if (replacement.HasParent)
        {
            throw new InvalidOperationException($"Replacement node {replacement} already has a parent {replacement.Parent}.");
        }

        if (node.HasParent)
        {
            node.ReplaceWith(replacement);
            return node;
        }

        return replacement;
    }
}