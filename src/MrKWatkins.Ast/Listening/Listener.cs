namespace MrKWatkins.Ast.Listening;

/// <summary>
/// A listener for a syntax tree. A listener walks the tree and gets notified when nodes are reached. An alternative to processing.
/// Useful to build something completely new from the tree. Processing is more useful to mutate the tree.
/// </summary>
/// <remarks>
/// Exceptions are not handled; if the listener throws then the exception will escape from the <see cref="Listen"/> method and no
/// further nodes will be processed.
/// </remarks>
/// <typeparam name="TNode">The type of the nodes in the tree.</typeparam>
public abstract class Listener<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Listen to the specified node and its descendents.
    /// </summary>
    /// <param name="node">The node to listen to.</param>
    public void Listen(TNode node)
    {
        BeforeListenToNode(node);
        
        ListenToNode(node);

        foreach (var child in node.Children)
        {
            Listen(child);
        }

        AfterListenToNode(node);
    }

    /// <summary>
    /// Called before a node *and its descendents* are listened to.
    /// </summary>
    /// <param name="node">The node about to be listened to.</param>
    protected internal virtual void BeforeListenToNode(TNode node)
    {
    }
    
    /// <summary>
    /// Called when the node is listened to.
    /// </summary>
    /// <param name="node">The node being listened to.</param>
    protected internal virtual void ListenToNode(TNode node)
    {
    }
    
    /// <summary>
    /// Called after a node *and its descendents* have been listened to.
    /// </summary>
    /// <param name="node">The node that has been listened to.</param>
    protected internal virtual void AfterListenToNode(TNode node)
    {
    }
}

/// <summary>
/// A <see cref="Listener{TNode}" /> that only listens to nodes of a specific type. All other nodes will be ignored. The listener will
/// still proceed to descendents of nodes that aren't listened too, i.e. the entire tree will be walked.
/// </summary>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of the nodes to listen to.</typeparam>
public abstract class Listener<TBaseNode, TNode> : Listener<TBaseNode>
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
}