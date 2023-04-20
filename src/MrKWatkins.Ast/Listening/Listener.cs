namespace MrKWatkins.Ast.Listening;

public abstract class Listener<TNode>
    where TNode : Node<TNode>
{
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

    protected internal virtual void BeforeListenToNode(TNode node)
    {
    }
    
    protected internal virtual void ListenToNode(TNode node)
    {
    }
    
    protected internal virtual void AfterListenToNode(TNode node)
    {
    }
}

public abstract class Listener<TBaseNode, TNode> : Listener<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    protected internal sealed override void BeforeListenToNode(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            BeforeListenToNode(typedNode);
        }
    }

    protected internal virtual void BeforeListenToNode(TNode node)
    {
    }
    
    protected internal sealed override void ListenToNode(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            ListenToNode(typedNode);
        }
    }
    
    protected internal virtual void ListenToNode(TNode node)
    {
    }
    
    protected internal sealed override void AfterListenToNode(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            AfterListenToNode(typedNode);
        }
    }
    
    protected internal virtual void AfterListenToNode(TNode node)
    {
    }
}