# Listeners

Listeners walk a tree and are notified as nodes are reached. They are the lightweight alternative to [processing](processing.md): a listener has access to a context object it can accumulate results in, which makes it a good fit for building something new out of a tree — a string representation, IL, an evaluated value. Reach for [processing](processing.md) instead when the job is to mutate the tree.

## Creating a Listener

There are two base classes to inherit from:

- [`Listener<TContext, TNode>`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/index.md) listens to every node in the tree.
- [`Listener<TContext, TBaseNode, TNode>`](API/MrKWatkins.Ast.Listening/Listener-TContext-TBaseNode-TNode/index.md) listens only to nodes of a specific type. Other nodes are ignored, but their descendents are still walked, so the whole tree is visited either way.

Three methods can be overridden to get at the nodes:

| Method | Called |
| ------ | ------ |
| [`BeforeListenToNode`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/BeforeListenToNode.md) | Immediately before a node *and its descendents* are visited. |
| [`ListenToNode`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/ListenToNode.md) | When the node itself is visited. |
| [`AfterListenToNode`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/AfterListenToNode.md) | Immediately after a node *and its descendents* have been visited. |

Between them, the before and after methods bracket a whole subtree, which is what you want for anything that nests — opening and closing brackets, pushing and popping a scope, indenting output.

[`ShouldListenToChildren`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/ShouldListenToChildren.md) can be overridden to skip a node's descendents entirely.

Start the walk by calling [`Listen`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/Listen.md) with the context and the root node:

```c#
var context = new FormattingContext();

listener.Listen(context, expression);

return context.Output.ToString();
```

The context is passed in to [`Listen`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/Listen.md) rather than created by the listener. That means listeners hold no state of their own between runs and a single instance can be used to walk many trees, including concurrently.

Exceptions are not handled. If a listener throws, the exception escapes from [`Listen`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/Listen.md) and no further nodes are visited.

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

Exactly one listener will ever be used for a node — the one registered for the most specific type that node matches. Registering a listener for a base type therefore gives fallback behaviour for anything more specific that has no listener of its own, and the [`With`](API/MrKWatkins.Ast.Listening/ICompositeListenerBuilder-TContext-TBaseNode/With.md) overload taking a two parameter listener registers a catch-all for the base node type itself. If no listener matches at all the node is skipped, though its descendents are still visited.

Only one listener can be registered per type, and [`ToListener`](API/MrKWatkins.Ast.Listening/ICompositeListenerBuilder-TContext-TBaseNode/ToListener.md) throws if no listeners were registered at all. Listeners can share implementation through their own base classes in the usual way.

## Example

The [Listeners example](https://github.com/MrKWatkins/Ast/tree/main/examples/Listeners) uses composite listeners to produce a string representation of a tree. The [Maths example](https://github.com/MrKWatkins/Ast/tree/main/examples/Maths) uses them twice over, to evaluate an expression tree and to compile it.
