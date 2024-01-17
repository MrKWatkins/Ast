# ListenerWithContext&lt;TContext, TNode&gt;.BeforeListenToNode Method
## Definition

Called before a node *and its descendents* are listened to.

```c#
protected virtual void BeforeListenToNode(TContext context, TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node about to be listened to. |

