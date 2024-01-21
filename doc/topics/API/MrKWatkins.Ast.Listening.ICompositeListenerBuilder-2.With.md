# ICompositeListenerBuilder&lt;TContext, TBaseNode&gt;.With Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [With(Listener&lt;TContext, TBaseNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-2-with(mrkwatkins-ast-listening-listener((-0-1)))) | Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered. |
| [With&lt;TNode&gt;(Listener&lt;TContext, TBaseNode, TNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-2-with-1(mrkwatkins-ast-listening-listener((-0-1-0)))) | Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered. |

## With(Listener&lt;TContext, TBaseNode&gt;) {id="mrkwatkins-ast-listening-icompositelistenerbuilder-2-with(mrkwatkins-ast-listening-listener((-0-1)))"}

Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered.

```c#
public abstract ICompositeListenerBuilder<TContext, TBaseNode> With(Listener<TContext, TBaseNode> listener);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-icompositelistenerbuilder-2-with(mrkwatkins-ast-listening-listener((-0-1)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| listener | [Listener&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.Listener-2.md) | The listener. |

## Returns {id="returns-mrkwatkins-ast-listening-icompositelistenerbuilder-2-with(mrkwatkins-ast-listening-listener((-0-1)))"}

[ICompositeListenerBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.md)

The fluent builder.
## With&lt;TNode&gt;(Listener&lt;TContext, TBaseNode, TNode&gt;) {id="mrkwatkins-ast-listening-icompositelistenerbuilder-2-with-1(mrkwatkins-ast-listening-listener((-0-1-0)))"}

Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered.

```c#
public abstract ICompositeListenerBuilder<TContext, TBaseNode> With<TNode>(Listener<TContext, TBaseNode, TNode> listener)
   where TNode : TBaseNode;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-listening-icompositelistenerbuilder-2-with-1(mrkwatkins-ast-listening-listener((-0-1-0)))"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node `listener` listens to. |

## Parameters {id="parameters-mrkwatkins-ast-listening-icompositelistenerbuilder-2-with-1(mrkwatkins-ast-listening-listener((-0-1-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| listener | [Listener&lt;TContext, TBaseNode, TNode&gt;](MrKWatkins.Ast.Listening.Listener-3.md) | The listener. |

## Returns {id="returns-mrkwatkins-ast-listening-icompositelistenerbuilder-2-with-1(mrkwatkins-ast-listening-listener((-0-1-0)))"}

[ICompositeListenerBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-2.md)

The fluent builder.
