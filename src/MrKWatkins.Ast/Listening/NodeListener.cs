namespace MrKWatkins.Ast.Listening;

/// <summary>
/// A <see cref="Listener{TContext, TNode}" /> that only listens to nodes of a specific type. All other nodes will be ignored. The listener will
/// still proceed to descendents of nodes that aren't listened too, i.e. the entire tree will be walked.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of the nodes to listen to.</typeparam>
public abstract class NodeListener<TContext, TBaseNode, TNode> : Listener<TContext, TBaseNode>
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

    /// <inheritdoc />
    protected internal sealed override bool ShouldListenToChildren(TContext context, TBaseNode node) =>
        node is not TNode typedNode || ShouldListenToChildren(context, typedNode);

    /// <summary>
    /// Return a value indicating whether child nodes should be listened to or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node whose children should be listened to or not.</param>
    /// <returns><c>true</c> if child nodes should be listened to, <c>false</c> otherwise.</returns>
    protected virtual bool ShouldListenToChildren(TContext context, TNode node) => true;
}

/// <summary>
/// A <see cref="Listener{TNode}" /> that only listens to nodes of a specific type and does not take a context object. All other nodes will be
/// ignored. The listener will still proceed to descendents of nodes that aren't listened too, i.e. the entire tree will be walked.
/// </summary>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of the nodes to listen to.</typeparam>
public abstract class NodeListener<TBaseNode, TNode> : Listener<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected internal sealed override void BeforeListenToNode(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            BeforeListenToNode(typedNode);
        }
    }

    /// <summary>
    /// Called before a node *and its descendents* are listened to.
    /// </summary>
    /// <param name="node">The node about to be listened to.</param>
    protected virtual void BeforeListenToNode(TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override void ListenToNode(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            ListenToNode(typedNode);
        }
    }

    /// <summary>
    /// Called when the node is listened to.
    /// </summary>
    /// <param name="node">The node being listened to.</param>
    protected virtual void ListenToNode(TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override void AfterListenToNode(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            AfterListenToNode(typedNode);
        }
    }

    /// <summary>
    /// Called after a node *and its descendents* have been listened to.
    /// </summary>
    /// <param name="node">The node that has been listened to.</param>
    protected virtual void AfterListenToNode(TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override bool ShouldListenToChildren(TBaseNode node) =>
        node is not TNode typedNode || ShouldListenToChildren(typedNode);

    /// <summary>
    /// Return a value indicating whether child nodes should be listened to or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="node">The node whose children should be listened to or not.</param>
    /// <returns><c>true</c> if child nodes should be listened to, <c>false</c> otherwise.</returns>
    protected virtual bool ShouldListenToChildren(TNode node) => true;
}