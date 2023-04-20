namespace MrKWatkins.Ast.Listening;

public abstract class ListenerWithContext<TContext, TNode>
    where TNode : Node<TNode>
{
    public void Listen(TContext context, TNode node)
    {
        BeforeListenToNode(context, node);
        
        ListenToNode(context, node);

        foreach (var child in node.Children)
        {
            Listen(context, child);
        }

        AfterListenToNode(context, node);
    }

    protected internal virtual void BeforeListenToNode(TContext context, TNode node)
    {
    }
    
    protected internal virtual void ListenToNode(TContext context, TNode node)
    {
    }
    
    protected internal virtual void AfterListenToNode(TContext context, TNode node)
    {
    }
}

public abstract class ListenerWithContext<TContext, TBaseNode, TNode> : ListenerWithContext<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    protected internal sealed override void BeforeListenToNode(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            BeforeListenToNode(context, typedNode);
        }
    }

    protected internal virtual void BeforeListenToNode(TContext context, TNode node)
    {
    }
    
    protected internal sealed override void ListenToNode(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            ListenToNode(context, typedNode);
        }
    }
    
    protected internal virtual void ListenToNode(TContext context, TNode node)
    {
    }
    
    protected internal sealed override void AfterListenToNode(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            AfterListenToNode(context, typedNode);
        }
    }
    
    protected internal virtual void AfterListenToNode(TContext context, TNode node)
    {
    }
}