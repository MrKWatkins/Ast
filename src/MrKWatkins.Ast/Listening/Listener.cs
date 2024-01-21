namespace MrKWatkins.Ast.Listening;

/// <summary>
/// A listener for a syntax tree. A listener walks the tree and gets notified when nodes are reached. An alternative to processing. Useful to build something completely
/// new from the tree whereas processing is more useful to mutate the tree.
/// </summary>
/// <remarks>
/// Exceptions are not handled; if the listener throws then the exception will escape from the <see cref="Listen"/> method and no
/// further nodes will be processed.
/// </remarks>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TNode">The type of the nodes to listen to.</typeparam>
public abstract class Listener<TContext, TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Listen to the specified node and its descendents.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node to listen to.</param>
    public void Listen(TContext context, TNode node)
    {
        BeforeListenToNode(context, node);

        ListenToNode(context, node);

        if (ShouldListenToChildren(context, node))
        {
            foreach (var child in node.Children)
            {
                Listen(context, child);
            }
        }

        AfterListenToNode(context, node);
    }

    /// <summary>
    /// Called before a node *and its descendents* are listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node about to be listened to.</param>
    protected internal virtual void BeforeListenToNode(TContext context, TNode node)
    {
    }

    /// <summary>
    /// Called when the node is listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node being listened to.</param>
    protected internal virtual void ListenToNode(TContext context, TNode node)
    {
    }

    /// <summary>
    /// Called after a node *and its descendents* have been listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node that has been listened to.</param>
    protected internal virtual void AfterListenToNode(TContext context, TNode node)
    {
    }

    /// <summary>
    ///     Return a value indicating whether child nodes should be listened to or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node who's children should be listened to or not.</param>
    protected virtual bool ShouldListenToChildren(TContext context, TNode node) => true;
}

/// <summary>
/// A <see cref="Listener{TContext, TNode}" /> that only listens to nodes of a specific type. All other nodes will be ignored. The listener will
/// still proceed to descendents of nodes that aren't listened too, i.e. the entire tree will be walked.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of the nodes to listen to.</typeparam>
public abstract class Listener<TContext, TBaseNode, TNode> : Listener<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected internal sealed override void BeforeListenToNode(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            BeforeListenToNode(context, typedNode);
        }
    }

    /// <summary>
    /// Called before a node *and its descendents* are listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node about to be listened to.</param>
    protected virtual void BeforeListenToNode(TContext context, TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override void ListenToNode(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            ListenToNode(context, typedNode);
        }
    }

    /// <summary>
    /// Called when the node is listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node being listened to.</param>
    protected virtual void ListenToNode(TContext context, TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override void AfterListenToNode(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            AfterListenToNode(context, typedNode);
        }
    }

    /// <summary>
    /// Called after a node *and its descendents* have been listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node that has been listened to.</param>
    protected virtual void AfterListenToNode(TContext context, TNode node)
    {
    }
}