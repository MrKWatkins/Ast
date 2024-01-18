# ICompositeListenerBuilder&lt;TBaseNode&gt; Interface
## Definition

Fluent interface to build a [CompositeListener&lt;TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListener-1.md).

```c#
public abstract interface ICompositeListenerBuilder<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of all nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [ToListener()](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.ToListener.md) | Builds the [CompositeListener&lt;TBaseNode&gt;](MrKWatkins.Ast.Listening.CompositeListener-1.md). |
| [With(Listener&lt;TBaseNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-1-with(mrkwatkins-ast-listening-listener((-0)))) | Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered. |
| [With&lt;TNode&gt;(Listener&lt;TBaseNode, TNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-1-with-1(mrkwatkins-ast-listening-listener((-0-0)))) | Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered. |

