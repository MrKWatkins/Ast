namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Shared implementation of node replacement for the replacer processors.
/// </summary>
internal static class Replacement
{
    /// <summary>
    /// Replaces <paramref name="node" /> with <paramref name="replacement" /> if it is a different node. If <paramref name="node" /> has a parent the
    /// replacement is swapped into the tree in its place and <c>null</c> is returned. If <paramref name="node" /> is the root there is no tree to update
    /// so the replacement is returned, i.e. a non-null return value is the new root of the tree.
    /// </summary>
    internal static TBaseNode? Apply<TBaseNode>(TBaseNode node, TBaseNode? replacement)
        where TBaseNode : Node<TBaseNode>
    {
        if (replacement == null || ReferenceEquals(node, replacement))
        {
            return null;
        }

        if (replacement.HasParent)
        {
            throw new InvalidOperationException($"Replacement node {replacement} already has a parent {replacement.Parent}.");
        }

        if (node.HasParent)
        {
            node.ReplaceWith(replacement);
            return null;
        }

        return replacement;
    }

    /// <summary>
    /// As <see cref="Apply{TBaseNode}" /> but for use outside a pipeline, where there is nothing to receive a new root. Throws if the root would be replaced.
    /// </summary>
    internal static void ApplyOutsidePipeline<TBaseNode>(TBaseNode node, TBaseNode? replacement)
        where TBaseNode : Node<TBaseNode>
    {
        if (Apply(node, replacement) != null)
        {
            throw new InvalidOperationException("The root node can only be replaced by running the replacer in a pipeline.");
        }
    }
}