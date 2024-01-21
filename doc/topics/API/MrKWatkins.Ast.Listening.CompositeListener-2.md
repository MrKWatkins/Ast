# CompositeListener&lt;TContext, TBaseNode&gt; Class
## Definition

A [Listener&lt;TContext, TNode&gt;](MrKWatkins.Ast.Listening.Listener-2.md) built from multiple other listeners that listen to specific node types. When a node is reached the listener with the most specific type for the node will be used. Only a single listener will ever listen to a node. If no suitable listener is found the node will be ignored but it&#39;s descendents will still be listened to.

```c#
public sealed class CompositeListener<TContext, TBaseNode> : Listener<TContext, TBaseNode>, ICompositeListenerBuilder<TContext, TBaseNode>
   where TContext
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TBaseNode | The base type of all nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode(TContext, TBaseNode)](MrKWatkins.Ast.Listening.CompositeListener-2.AfterListenToNode.md) |  |
| [BeforeListenToNode(TContext, TBaseNode)](MrKWatkins.Ast.Listening.CompositeListener-2.BeforeListenToNode.md) |  |
| [Build()](MrKWatkins.Ast.Listening.CompositeListener-2.Build.md) | Fluent interface to build a [CompositeListener&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListener-2.md). |
| [ListenToNode(TContext, TBaseNode)](MrKWatkins.Ast.Listening.CompositeListener-2.ListenToNode.md) |  |

