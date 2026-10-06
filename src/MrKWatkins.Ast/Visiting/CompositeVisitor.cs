namespace MrKWatkins.Ast.Visiting;

/// <summary>
/// A <see cref="Visitor{TContext, TNode, TResult}" /> built from multiple other visitors that visit specific node types. When a node is visited the visitor
/// with the most specific type for the node will be used. If no suitable visitor is found an <see cref="InvalidOperationException" /> is thrown; register a
/// visitor for the base node type to provide a fallback instead.
/// </summary>
/// <remarks>
/// Visitors registered with a composite have their <see cref="Visitor{TContext, TNode, TResult}.Visit" /> method dispatch through the composite, so a visitor for
/// one node type can visit children of other node types. A visitor can therefore only be registered with a single composite. A composite can itself be
/// registered with another composite, in which case dispatch goes through the outermost one.
/// </remarks>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public sealed class CompositeVisitor<TContext, TBaseNode, TResult> : Visitor<TContext, TBaseNode, TResult>, ICompositeVisitorBuilder<TContext, TBaseNode, TResult>
    where TBaseNode : Node<TBaseNode>
{
    private readonly NodeTypeLookup<TBaseNode, Visitor<TContext, TBaseNode, TResult>> visitors = new("visitor");

    /// <summary>
    /// Fluent interface to build a <see cref="CompositeVisitor{TContext, TBaseNode, TResult}"/>.
    /// </summary>
    /// <returns>A fluent builder.</returns>
    [Pure]
    public static ICompositeVisitorBuilder<TContext, TBaseNode, TResult> Build() => new CompositeVisitor<TContext, TBaseNode, TResult>();

    private CompositeVisitor()
    {
    }

    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">If no visitor has been registered for the type of <paramref name="node" /> or any of its base types.</exception>
    protected internal override TResult VisitNode(TContext context, TBaseNode node)
    {
        var visitor = visitors.Get(node) ?? throw new InvalidOperationException($"No visitor has been registered for {node.GetType().SimpleName()}.");

        return visitor.VisitNode(context, node);
    }

    [MustUseReturnValue]
    ICompositeVisitorBuilder<TContext, TBaseNode, TResult> ICompositeVisitorBuilder<TContext, TBaseNode, TResult>.With(Visitor<TContext, TBaseNode, TResult> visitor) =>
        Add<TBaseNode>(visitor);

    [MustUseReturnValue]
    ICompositeVisitorBuilder<TContext, TBaseNode, TResult> ICompositeVisitorBuilder<TContext, TBaseNode, TResult>.With<TNode>(NodeVisitor<TContext, TBaseNode, TNode, TResult> visitor) =>
        Add<TNode>(visitor);

    private CompositeVisitor<TContext, TBaseNode, TResult> Add<TNode>(Visitor<TContext, TBaseNode, TResult> visitor)
        where TNode : TBaseNode
    {
        if (visitor.HasOwner)
        {
            throw new InvalidOperationException($"The visitor {visitor.GetType().SimpleName()} has already been registered with a composite visitor.");
        }

        visitors.Add<TNode>(visitor);
        visitor.SetOwner(this);

        return this;
    }

    [Pure]
    CompositeVisitor<TContext, TBaseNode, TResult> ICompositeVisitorBuilder<TContext, TBaseNode, TResult>.ToVisitor()
    {
        if (visitors.Count == 0)
        {
            throw new InvalidOperationException("No visitors have been registered.");
        }

        return this;
    }
}

/// <summary>
/// A <see cref="Visitor{TNode, TResult}" /> built from multiple other visitors that visit specific node types and do not take a context object. When a node is
/// visited the visitor with the most specific type for the node will be used. If no suitable visitor is found an <see cref="InvalidOperationException" /> is
/// thrown; register a visitor for the base node type to provide a fallback instead.
/// </summary>
/// <remarks>
/// Visitors registered with a composite have their <see cref="Visitor{TNode, TResult}.Visit(TNode)" /> method dispatch through the composite, so a visitor for
/// one node type can visit children of other node types. A visitor can therefore only be registered with a single composite. A composite can itself be
/// registered with another composite, in which case dispatch goes through the outermost one.
/// </remarks>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public sealed class CompositeVisitor<TBaseNode, TResult> : Visitor<TBaseNode, TResult>, ICompositeVisitorBuilder<TBaseNode, TResult>
    where TBaseNode : Node<TBaseNode>
{
    private readonly NodeTypeLookup<TBaseNode, Visitor<TBaseNode, TResult>> visitors = new("visitor");

    /// <summary>
    /// Fluent interface to build a <see cref="CompositeVisitor{TBaseNode, TResult}"/>.
    /// </summary>
    /// <returns>A fluent builder.</returns>
    [Pure]
    public static ICompositeVisitorBuilder<TBaseNode, TResult> Build() => new CompositeVisitor<TBaseNode, TResult>();

    private CompositeVisitor()
    {
    }

    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">If no visitor has been registered for the type of <paramref name="node" /> or any of its base types.</exception>
    protected internal override TResult VisitNode(TBaseNode node)
    {
        var visitor = visitors.Get(node) ?? throw new InvalidOperationException($"No visitor has been registered for {node.GetType().SimpleName()}.");

        return visitor.VisitNode(node);
    }

    [MustUseReturnValue]
    ICompositeVisitorBuilder<TBaseNode, TResult> ICompositeVisitorBuilder<TBaseNode, TResult>.With(Visitor<TBaseNode, TResult> visitor) =>
        Add<TBaseNode>(visitor);

    [MustUseReturnValue]
    ICompositeVisitorBuilder<TBaseNode, TResult> ICompositeVisitorBuilder<TBaseNode, TResult>.With<TNode>(NodeVisitor<TBaseNode, TNode, TResult> visitor) =>
        Add<TNode>(visitor);

    private CompositeVisitor<TBaseNode, TResult> Add<TNode>(Visitor<TBaseNode, TResult> visitor)
        where TNode : TBaseNode
    {
        if (visitor.HasOwner)
        {
            throw new InvalidOperationException($"The visitor {visitor.GetType().SimpleName()} has already been registered with a composite visitor.");
        }

        visitors.Add<TNode>(visitor);
        visitor.SetOwner(this);

        return this;
    }

    [Pure]
    CompositeVisitor<TBaseNode, TResult> ICompositeVisitorBuilder<TBaseNode, TResult>.ToVisitor()
    {
        if (visitors.Count == 0)
        {
            throw new InvalidOperationException("No visitors have been registered.");
        }

        return this;
    }
}