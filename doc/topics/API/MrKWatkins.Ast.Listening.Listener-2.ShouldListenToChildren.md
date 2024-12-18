# Listener&lt;TContext, TNode&gt;.ShouldListenToChildren Method
## Definition

Return a value indicating whether child nodes should be listened to or not. Defaults to `true`.

```c#
protected virtual bool ShouldListenToChildren(TContext? context, TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node whose children should be listened to or not. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if child nodes should be listened to, `false` otherwise.
