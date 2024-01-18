# Listener&lt;TBaseNode, TNode&gt;.AfterListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode](MrKWatkins.Ast.Listening.Listener-2.AfterListenToNode.md#mrkwatkins-ast-listening-listener-2-afterlistentonode(-0)) |  |
| [AfterListenToNode](MrKWatkins.Ast.Listening.Listener-2.AfterListenToNode.md#mrkwatkins-ast-listening-listener-2-afterlistentonode(-1)) | Called after a node *and its descendents* have been listened to. |

## AfterListenToNode(TBaseNode) {id="mrkwatkins-ast-listening-listener-2-afterlistentonode(-0)"}

```c#
protected sealed override void AfterListenToNode(TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-2-afterlistentonode(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode |  |

## AfterListenToNode(TNode) {id="mrkwatkins-ast-listening-listener-2-afterlistentonode(-1)"}

Called after a node *and its descendents* have been listened to.

```c#
protected new virtual void AfterListenToNode(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-2-afterlistentonode(-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node that has been listened to. |

