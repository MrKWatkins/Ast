# Listeners

Listeners walk a tree and are notified as nodes are reached. They are the lightweight alternative to [processing](processing.md): a listener has access to a context object it can accumulate results in, which makes it a good fit for building something new out of a tree — a string representation, IL, a report of the tree. Reach for [processing](processing.md) instead when the job is to mutate the tree, or for [visitors](visitors.md) when each node should produce a value, such as when evaluating a tree.

## Creating a Listener

There are two base classes to inherit from:

- [`Listener<TContext, TNode>`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/index.md) listens to every node in the tree.
- [`NodeListener<TContext, TBaseNode, TNode>`](API/MrKWatkins.Ast.Listening/NodeListener-TContext-TBaseNode-TNode/index.md) listens only to nodes of a specific type. Other nodes are ignored, but their descendants are still walked, so the whole tree is visited either way.

Three methods can be overridden to get at the nodes:

| Method | Called |
| ------ | ------ |
| [`BeforeListenToNode`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/BeforeListenToNode.md) | Immediately before a node *and its descendants* are visited. |
| [`ListenToNode`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/ListenToNode.md) | When the node itself is visited. |
| [`AfterListenToNode`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/AfterListenToNode.md) | Immediately after a node *and its descendants* have been visited. |

Between them, the before and after methods bracket a whole subtree, which is what you want for anything that nests — opening and closing brackets, pushing and popping a scope, indenting output.

[`ShouldListenToDescendants`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/ShouldListenToDescendants.md) can be overridden to skip a node's descendants entirely.

Start the walk by calling [`Listen`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/Listen.md) with the context and the root node:

```c#
var context = new FormattingContext();

listener.Listen(context, expression);

return context.Output.ToString();
```

The context is passed in to [`Listen`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/Listen.md) rather than created by the listener. That means listeners hold no state of their own between runs and a single instance can be used to walk many trees, including concurrently.

Exceptions are not handled. If a listener throws, the exception escapes from [`Listen`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/Listen.md) and no further nodes are visited.

## Listeners Without a Context

Every listener type has a form that takes no context object: [`Listener<TNode>`](API/MrKWatkins.Ast.Listening/Listener-TNode/index.md), [`NodeListener<TBaseNode, TNode>`](API/MrKWatkins.Ast.Listening/NodeListener-TBaseNode-TNode/index.md) and [`CompositeListener<TBaseNode>`](API/MrKWatkins.Ast.Listening/CompositeListener-TBaseNode/index.md). They have the same methods minus the context parameter, so a listener that accumulates into its own fields, or that only has side effects, need not invent a context type:

```c#
internal sealed class NodeCounter : Listener<Expression>
{
    public int Count { get; private set; }

    protected internal override void ListenToNode(Expression node) => Count++;
}
```

A listener without a context holds its state itself, so unlike one with a context a single instance should not be used to walk several trees at once. Under the covers the context-free forms share the implementation of the context-taking ones using the empty [`NoContext`](API/MrKWatkins.Ast/NoContext/index.md) type; that only matters if you see `NoContext` in a base class chain.

## Composite Listeners

Walking a tree usually means doing something different for each kind of node. Rather than one listener with a `switch` over node types, build a [`CompositeListener<TContext, TBaseNode>`](API/MrKWatkins.Ast.Listening/CompositeListener-TContext-TBaseNode/index.md) from listeners that each handle one type, using the fluent interface from [`Build`](API/MrKWatkins.Ast.Listening/CompositeListener-TContext-TBaseNode/Build.md):

```c#
private static readonly CompositeListener<FormattingContext, Expression> Listener =
    CompositeListener<FormattingContext, Expression>
        .Build()
        .With(new ConstantListener())
        .With(new ArrayListener())
        .ToListener();
```

Exactly one listener will ever be used for a node — the one registered for the most specific type that node matches. Registering a listener for a base type therefore gives fallback behaviour for anything more specific that has no listener of its own, and the [`With`](API/MrKWatkins.Ast.Listening/ICompositeListenerBuilder-TContext-TBaseNode/With.md) overload taking a two parameter listener registers a catch-all for the base node type itself. If no listener matches at all the node is skipped, though its descendants are still visited.

Only one listener can be registered per type, and [`ToListener`](API/MrKWatkins.Ast.Listening/ICompositeListenerBuilder-TContext-TBaseNode/ToListener.md) throws if no listeners were registered at all. Listeners can share implementation through their own base classes in the usual way. [`CompositeListener<TBaseNode>`](API/MrKWatkins.Ast.Listening/CompositeListener-TBaseNode/index.md) does the same for listeners without a context.

## Example

The [Listeners example](https://github.com/MrKWatkins/Ast/tree/main/examples/Listeners) uses composite listeners to produce a string representation of a tree. The [Maths example](https://github.com/MrKWatkins/Ast/tree/main/examples/Maths) uses them to compile an expression tree, alongside a [visitor](visitors.md) that evaluates it.
