# ICompositeListenerBuilder&lt;TContext, TBaseNode&gt; Interface
## Definition

Fluent interface to build a [CompositeListener&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListener-2.md).

```c#
public abstract interface ICompositeListenerBuilder<TContext, TBaseNode>
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
| [ToListener()](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.ToListener.md) | Builds the [CompositeListener&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListener-2.md). |
| [With(Listener&lt;TContext, TBaseNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-2-with(mrkwatkins-ast-listening-listener((-0-1)))) | Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered. |
| [With&lt;TNode&gt;(Listener&lt;TContext, TBaseNode, TNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-2-with-1(mrkwatkins-ast-listening-listener((-0-1-0)))) | Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered. |

