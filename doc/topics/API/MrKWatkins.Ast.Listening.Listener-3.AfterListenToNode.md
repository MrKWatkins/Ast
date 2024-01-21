# Listener&lt;TContext, TBaseNode, TNode&gt;.AfterListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode(TContext, TBaseNode)](MrKWatkins.Ast.Listening.Listener-3.AfterListenToNode.md#mrkwatkins-ast-listening-listener-3-afterlistentonode(-0-1)) |  |
| [AfterListenToNode(TContext, TNode)](MrKWatkins.Ast.Listening.Listener-3.AfterListenToNode.md#mrkwatkins-ast-listening-listener-3-afterlistentonode(-0-2)) | Called after a node *and its descendents* have been listened to. |

## AfterListenToNode(TContext, TBaseNode) {id="mrkwatkins-ast-listening-listener-3-afterlistentonode(-0-1)"}

```c#
protected sealed override void AfterListenToNode(TContext context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-3-afterlistentonode(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## AfterListenToNode(TContext, TNode) {id="mrkwatkins-ast-listening-listener-3-afterlistentonode(-0-2)"}

Called after a node *and its descendents* have been listened to.

```c#
protected new virtual void AfterListenToNode(TContext context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-3-afterlistentonode(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node that has been listened to. |

