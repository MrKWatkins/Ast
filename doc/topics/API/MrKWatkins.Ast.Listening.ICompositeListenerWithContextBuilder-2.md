# ICompositeListenerWithContextBuilder&lt;TContext, TBaseNode&gt; Interface
## Definition

Fluent interface to build a [CompositeListenerWithContext&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListenerWithContext-2.md).

```c#
public abstract interface ICompositeListenerWithContextBuilder<TContext, TBaseNode>
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
| [ToListener()](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.ToListener.md) | Builds the [CompositeListenerWithContext&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListenerWithContext-2.md). |
| [With(ListenerWithContext&lt;TContext, TBaseNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with(mrkwatkins-ast-listening-listenerwithcontext((-0-1)))) | Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered. |
| [With(ListenerWithContext&lt;TContext, TBaseNode, TNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with-1(mrkwatkins-ast-listening-listenerwithcontext((-0-1-0)))) | Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered. |

