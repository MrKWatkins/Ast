using System.Collections.Concurrent;

namespace MrKWatkins.Ast.Processing;

internal sealed class Exceptions
{
    private readonly ConcurrentStack<ProcessingException> exceptions = new();

    internal void Add(Exceptions other)
    {
        foreach (var exception in other.exceptions)
        {
            exceptions.Push(exception);
        }
    }

    [MustUseReturnValue]
    internal bool Trap<TNode>(TNode node, string method, [InstantHandle] Func<TNode, bool> function)
        where TNode : Node<TNode> =>
        Trap<TNode, TNode>(node, method, function);

    [MustUseReturnValue]
    internal bool Trap<TBaseNode, TNode>(TNode node, string method, [InstantHandle] Func<TNode, bool> function)
        where TBaseNode : Node<TBaseNode>
        where TNode : TBaseNode
    {
        try
        {
            return function(node);
        }
        catch (Exception exception)
        {
            exceptions.Push(new ProcessingException<TBaseNode>($"Exception during {method}.", exception, node));
            return false;
        }
    }

    [MustUseReturnValue]
    internal bool Trap<TContext, TNode>(TContext context, TNode node, string method, [InstantHandle] Func<TContext, TNode, bool> function)
        where TNode : Node<TNode> =>
        Trap<TContext, TNode, TNode>(context, node, method, function);

    [MustUseReturnValue]
    internal bool Trap<TContext, TBaseNode, TNode>(TContext context, TNode node, string method, [InstantHandle] Func<TContext, TNode, bool> function)
        where TBaseNode : Node<TBaseNode>
        where TNode : TBaseNode
    {
        try
        {
            return function(context, node);
        }
        catch (Exception exception)
        {
            exceptions.Push(new ProcessingException<TBaseNode>($"Exception during {method}.", exception, node));
            return false;
        }
    }

    internal void Trap<TNode>(TNode node, string method, [InstantHandle] Action<TNode> action)
        where TNode : Node<TNode> =>
        Trap<TNode, TNode>(node, method, action);

    internal void Trap<TBaseNode, TNode>(TNode node, string method, [InstantHandle] Action<TNode> action)
        where TBaseNode : Node<TBaseNode>
        where TNode : TBaseNode
    {
        try
        {
            action(node);
        }
        catch (Exception exception)
        {
            exceptions.Push(new ProcessingException<TBaseNode>($"Exception during {method}.", exception, node));
        }
    }

    internal void Trap<TContext, TNode>(TContext context, TNode node, string method, [InstantHandle] Action<TContext, TNode> action)
        where TNode : Node<TNode> =>
        Trap<TContext, TNode, TNode>(context, node, method, action);

    internal void Trap<TContext, TBaseNode, TNode>(TContext context, TNode node, string method, [InstantHandle] Action<TContext, TNode> action)
        where TBaseNode : Node<TBaseNode>
        where TNode : TBaseNode
    {
        try
        {
            action(context, node);
        }
        catch (Exception exception)
        {
            exceptions.Push(new ProcessingException<TBaseNode>($"Exception during {method}.", exception, node));
        }
    }

    public void ThrowIfContainsExceptions(string message)
    {
        if (exceptions.Any())
        {
            throw new AggregateException(message, exceptions);
        }
    }
}