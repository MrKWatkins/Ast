# ListenerWithContext&lt;TContext, TBaseNode, TNode&gt;.AfterListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.AfterListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-1)) |  |
| [AfterListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.AfterListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-2)) | Called after a node *and its descendents* have been listened to. |

## AfterListenToNode(TContext, TBaseNode) {id="mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-1)"}

```c#
protected sealed override void AfterListenToNode(TContext context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## AfterListenToNode(TContext, TNode) {id="mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-2)"}

Called after a node *and its descendents* have been listened to.

```c#
protected new virtual void AfterListenToNode(TContext context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node that has been listened to. |

