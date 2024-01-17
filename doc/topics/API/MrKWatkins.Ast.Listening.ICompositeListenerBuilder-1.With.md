# ICompositeListenerBuilder&lt;TBaseNode&gt;.With Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [With(Listener&lt;TBaseNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-1-with(mrkwatkins-ast-listening-listener((-0)))) | Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered. |
| [With(Listener&lt;TBaseNode, TNode&gt;)](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.With.md#mrkwatkins-ast-listening-icompositelistenerbuilder-1-with-1(mrkwatkins-ast-listening-listener((-0-0)))) | Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered. |

## With(Listener&lt;TBaseNode&gt;) {id="mrkwatkins-ast-listening-icompositelistenerbuilder-1-with(mrkwatkins-ast-listening-listener((-0)))"}

Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the specific node type registered.

```c#
public abstract ICompositeListenerBuilder<TBaseNode> With(Listener<TBaseNode> listener);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-icompositelistenerbuilder-1-with(mrkwatkins-ast-listening-listener((-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| listener | [Listener&lt;TBaseNode&gt;](MrKWatkins.Ast.Listening.Listener-1.md) | The listener. |

## Returns {id="returns-mrkwatkins-ast-listening-icompositelistenerbuilder-1-with(mrkwatkins-ast-listening-listener((-0)))"}

[ICompositeListenerBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.md)

The fluent builder.
## With&lt;TNode&gt;(Listener&lt;TBaseNode, TNode&gt;) {id="mrkwatkins-ast-listening-icompositelistenerbuilder-1-with-1(mrkwatkins-ast-listening-listener((-0-0)))"}

Add a listener for the specific node type `TNode`. This can be a base node type which will be used if there is no listener for the specific node type registered.

```c#
public abstract ICompositeListenerBuilder<TBaseNode> With<TNode>(Listener<TBaseNode, TNode> listener)
   where TNode : TBaseNode;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-listening-icompositelistenerbuilder-1-with-1(mrkwatkins-ast-listening-listener((-0-0)))"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node `listener` listens to. |

## Parameters {id="parameters-mrkwatkins-ast-listening-icompositelistenerbuilder-1-with-1(mrkwatkins-ast-listening-listener((-0-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| listener | [Listener&lt;TBaseNode, TNode&gt;](MrKWatkins.Ast.Listening.Listener-2.md) | The listener. |

## Returns {id="returns-mrkwatkins-ast-listening-icompositelistenerbuilder-1-with-1(mrkwatkins-ast-listening-listener((-0-0)))"}

[ICompositeListenerBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Listening.ICompositeListenerBuilder-1.md)

The fluent builder.
