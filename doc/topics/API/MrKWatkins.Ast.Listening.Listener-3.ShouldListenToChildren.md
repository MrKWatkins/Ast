# Listener&lt;TContext, TBaseNode, TNode&gt;.ShouldListenToChildren Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [ShouldListenToChildren(TContext, TBaseNode)](MrKWatkins.Ast.Listening.Listener-3.ShouldListenToChildren.md#mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-1)) |  |
| [ShouldListenToChildren(TContext, TNode)](MrKWatkins.Ast.Listening.Listener-3.ShouldListenToChildren.md#mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-2)) | Return a value indicating whether child nodes should be listened to or not. Defaults to `true`. |

## ShouldListenToChildren(TContext, TBaseNode) {id="mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-1)"}

```c#
protected sealed override bool ShouldListenToChildren(TContext? context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## Returns {id="returns-mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-1)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
## ShouldListenToChildren(TContext, TNode) {id="mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-2)"}

Return a value indicating whether child nodes should be listened to or not. Defaults to `true`.

```c#
protected new virtual bool ShouldListenToChildren(TContext? context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node whose children should be listened to or not. |

## Returns {id="returns-mrkwatkins-ast-listening-listener-3-shouldlistentochildren(-0-2)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if child nodes should be listened to, `false` otherwise.
