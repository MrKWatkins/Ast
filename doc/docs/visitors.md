# Visitors

Visitors visit a tree and return a value for each node. They are the answer to the question "how do I get a result out of a tree?" that [listeners](listeners.md) only answer indirectly: a listener has to accumulate its results in the context object, typically with a stack, whereas a visitor for a node simply visits the node's children and combines their values into its own.

The other difference is who drives the walk. A listener is notified as the library walks the whole tree. A visitor controls the walk itself: it visits whichever children it needs, in whatever order it needs, and can skip children altogether. That makes visitors the natural fit for evaluation, especially when not every child should be evaluated, such as the short-circuiting of `&&` or only evaluating one branch of an `if`. Reach for [listeners](listeners.md) when you just need to be notified of every node, and for [processing](processing.md) when the job is to mutate the tree.

## Creating a Visitor

There are two base classes to inherit from:

- [`Visitor<TContext, TNode, TResult>`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/index.md) visits any node in the tree.
- [`NodeVisitor<TContext, TBaseNode, TNode, TResult>`](API/MrKWatkins.Ast.Visiting/NodeVisitor-TContext-TBaseNode-TNode-TResult/index.md) visits only nodes of a specific type. These are the building blocks for [composite visitors](#composite-visitors).

Override [`VisitNode`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/VisitNode.md) to return the value for a node. Inside it, call [`Visit`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/Visit.md) to visit a child and get its value, or [`VisitChildren`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/VisitChildren.md) to visit all the children in order:

```c#
internal sealed class AndVisitor : NodeVisitor<EvaluationContext, Expression, And, bool>
{
    protected override bool VisitNode(EvaluationContext context, And and) =>
        Visit(context, and.Left) && Visit(context, and.Right);
}
```

Because `&&` short-circuits, the right operand is never visited when the left is `false`.

Start the visit by calling [`Visit`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/Visit.md) with the context and the root node; the return value is the result:

```c#
var context = new EvaluationContext(variables);

return visitor.Visit(context, expression);
```

As with listeners, the context is passed in rather than held by the visitor. Visitors hold no state of their own between runs, so a single instance can be used to visit many trees, including concurrently.

A typed visitor given a node of a type it does not handle calls [`VisitUnhandledNode`](API/MrKWatkins.Ast.Visiting/NodeVisitor-TContext-TBaseNode-TNode-TResult/VisitUnhandledNode.md). By default that throws an `InvalidOperationException`; override it to return a fallback value instead. A visitor cannot silently ignore a node the way a listener can, because it has to return something for it.

Exceptions are not handled. If a visitor throws, the exception escapes from [`Visit`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/Visit.md) and no further nodes are visited.

## Visitors Without a Context

Every visitor type has a form that takes no context object: [`Visitor<TNode, TResult>`](API/MrKWatkins.Ast.Visiting/Visitor-TNode-TResult/index.md), [`NodeVisitor<TBaseNode, TNode, TResult>`](API/MrKWatkins.Ast.Visiting/NodeVisitor-TBaseNode-TNode-TResult/index.md) and [`CompositeVisitor<TBaseNode, TResult>`](API/MrKWatkins.Ast.Visiting/CompositeVisitor-TBaseNode-TResult/index.md). They have the same methods minus the context parameter, which suits visitors whose result depends only on the tree:

```c#
internal sealed class NodeCounter : Visitor<Expression, int>
{
    protected internal override int VisitNode(Expression node) => 1 + VisitChildren(node).Sum();
}
```

Under the covers the context-free forms share the implementation of the context-taking ones using the empty [`NoContext`](API/MrKWatkins.Ast/NoContext/index.md) type; that only matters if you see `NoContext` in a base class chain.

## Composite Visitors

Visiting a tree usually means doing something different for each kind of node. Rather than one visitor with a `switch` over node types, build a [`CompositeVisitor<TContext, TBaseNode, TResult>`](API/MrKWatkins.Ast.Visiting/CompositeVisitor-TContext-TBaseNode-TResult/index.md) from typed visitors that each handle one type, using the fluent interface from [`Build`](API/MrKWatkins.Ast.Visiting/CompositeVisitor-TContext-TBaseNode-TResult/Build.md):

```c#
private static readonly CompositeVisitor<EvaluationContext, Expression, bool> Visitor =
    CompositeVisitor<EvaluationContext, Expression, bool>
        .Build()
        .With(new ConstantVisitor())
        .With(new VariableVisitor())
        .With(new AndVisitor())
        .With(new OrVisitor())
        .With(new NotVisitor())
        .ToVisitor();
```

Exactly one visitor is used for a node: the one registered for the most specific type that node matches. Registering a visitor for a base type therefore gives fallback behaviour for anything more specific that has no visitor of its own, and the [`With`](API/MrKWatkins.Ast.Visiting/ICompositeVisitorBuilder-TContext-TBaseNode-TResult/With.md) overload taking a three parameter visitor registers a catch-all for the base node type itself. If no visitor matches at all an `InvalidOperationException` is thrown.

Once a visitor has been registered with a composite, its [`Visit`](API/MrKWatkins.Ast.Visiting/Visitor-TContext-TNode-TResult/Visit.md) method dispatches through the composite. That is what lets the `AndVisitor` above visit its operands without knowing what types they are: the composite picks the right visitor for each. It also means a visitor instance can only belong to one composite. Composites can be nested by registering one with another, in which case dispatch goes through the outermost composite.

Only one visitor can be registered per type, and [`ToVisitor`](API/MrKWatkins.Ast.Visiting/ICompositeVisitorBuilder-TContext-TBaseNode-TResult/ToVisitor.md) throws if no visitors were registered at all. [`CompositeVisitor<TBaseNode, TResult>`](API/MrKWatkins.Ast.Visiting/CompositeVisitor-TBaseNode-TResult/index.md) does the same for visitors without a context.

## Example

The [Visitors example](https://github.com/MrKWatkins/Ast/tree/main/examples/Visitors) uses a composite visitor to evaluate boolean expressions, with short-circuiting `and` and `or`. The [Maths example](https://github.com/MrKWatkins/Ast/tree/main/examples/Maths) uses one to evaluate an arithmetic expression tree, alongside a listener that compiles the same tree.