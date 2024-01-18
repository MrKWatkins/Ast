# ICompositeListenerWithContextBuilder&lt;TContext, TBaseNode&gt;.With Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [With](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with(mrkwatkins-ast-listening-listenerwithcontext((-0-1)))) | Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered. |
| [With](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with-1(mrkwatkins-ast-listening-listenerwithcontext((-0-1-0)))) | Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered. |

## With(ListenerWithContext&lt;TContext, TBaseNode&gt;) {id="mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with(mrkwatkins-ast-listening-listenerwithcontext((-0-1)))"}

Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered.

```c#
public abstract ICompositeListenerWithContextBuilder<TContext, TBaseNode> With(ListenerWithContext<TContext, TBaseNode> listener);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with(mrkwatkins-ast-listening-listenerwithcontext((-0-1)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| listener | [ListenerWithContext&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.ListenerWithContext-2.md) | The listener. |

## Returns {id="returns-mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with(mrkwatkins-ast-listening-listenerwithcontext((-0-1)))"}

[ICompositeListenerWithContextBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.md)

The fluent builder.
## With&lt;TNode&gt;(ListenerWithContext&lt;TContext, TBaseNode, TNode&gt;) {id="mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with-1(mrkwatkins-ast-listening-listenerwithcontext((-0-1-0)))"}

Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered.

```c#
public abstract ICompositeListenerWithContextBuilder<TContext, TBaseNode> With<TNode>(ListenerWithContext<TContext, TBaseNode, TNode> listener)
   where TNode : TBaseNode;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with-1(mrkwatkins-ast-listening-listenerwithcontext((-0-1-0)))"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node `listener` listens to. |

## Parameters {id="parameters-mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with-1(mrkwatkins-ast-listening-listenerwithcontext((-0-1-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| listener | [ListenerWithContext&lt;TContext, TBaseNode, TNode&gt;](MrKWatkins.Ast.Listening.ListenerWithContext-3.md) | The listener. |

## Returns {id="returns-mrkwatkins-ast-listening-icompositelistenerwithcontextbuilder-2-with-1(mrkwatkins-ast-listening-listenerwithcontext((-0-1-0)))"}

[ICompositeListenerWithContextBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.ICompositeListenerWithContextBuilder-2.md)

The fluent builder.
