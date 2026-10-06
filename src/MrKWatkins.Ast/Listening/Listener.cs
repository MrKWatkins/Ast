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
    /// Listen to the specified node and its descendants.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node to listen to.</param>
    public void Listen(TContext context, TNode node)
    {
        BeforeListenToNode(context, node);

        ListenToNode(context, node);

        if (ShouldListenToDescendants(context, node))
        {
            foreach (var child in node.Children)
            {
                Listen(context, child);
            }
        }

        AfterListenToNode(context, node);
    }

    /// <summary>
    /// Called before a node *and its descendants* are listened to.
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
    /// Called after a node *and its descendants* have been listened to.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node that has been listened to.</param>
    protected internal virtual void AfterListenToNode(TContext context, TNode node)
    {
    }

    /// <summary>
    /// Return a value indicating whether child nodes should be listened to or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node whose children should be listened to or not.</param>
    /// <returns><c>true</c> if child nodes should be listened to, <c>false</c> otherwise.</returns>
    protected internal virtual bool ShouldListenToDescendants(TContext context, TNode node) => true;
}

/// <summary>
/// A <see cref="Listener{TContext, TNode}" /> that does not take a context object. Any state the listener needs must be held by the
/// listener itself.
/// </summary>
/// <remarks>
/// Exceptions are not handled; if the listener throws then the exception will escape from the <see cref="Listen(TNode)"/> method and no
/// further nodes will be processed.
/// </remarks>
/// <typeparam name="TNode">The type of the nodes to listen to.</typeparam>
public abstract class Listener<TNode> : Listener<NoContext, TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Listen to the specified node and its descendants.
    /// </summary>
    /// <param name="node">The node to listen to.</param>
    public void Listen(TNode node) => Listen(default, node);

    /// <inheritdoc />
    protected internal sealed override void BeforeListenToNode(NoContext context, TNode node) => BeforeListenToNode(node);

    /// <summary>
    /// Called before a node *and its descendants* are listened to.
    /// </summary>
    /// <param name="node">The node about to be listened to.</param>
    protected internal virtual void BeforeListenToNode(TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override void ListenToNode(NoContext context, TNode node) => ListenToNode(node);

    /// <summary>
    /// Called when the node is listened to.
    /// </summary>
    /// <param name="node">The node being listened to.</param>
    protected internal virtual void ListenToNode(TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override void AfterListenToNode(NoContext context, TNode node) => AfterListenToNode(node);

    /// <summary>
    /// Called after a node *and its descendants* have been listened to.
    /// </summary>
    /// <param name="node">The node that has been listened to.</param>
    protected internal virtual void AfterListenToNode(TNode node)
    {
    }

    /// <inheritdoc />
    protected internal sealed override bool ShouldListenToDescendants(NoContext context, TNode node) => ShouldListenToDescendants(node);

    /// <summary>
    /// Return a value indicating whether child nodes should be listened to or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="node">The node whose children should be listened to or not.</param>
    /// <returns><c>true</c> if child nodes should be listened to, <c>false</c> otherwise.</returns>
    protected internal virtual bool ShouldListenToDescendants(TNode node) => true;
}