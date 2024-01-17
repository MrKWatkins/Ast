# ListenerWithContext&lt;TContext, TNode&gt;.ShouldListenToChildren Method
## Definition

Return a value indicating whether child nodes should be listened to or not. Defaults to `true`.

```c#
protected virtual bool ShouldListenToChildren(TContext context, TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node who&#39;s children should be listened to or not. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
