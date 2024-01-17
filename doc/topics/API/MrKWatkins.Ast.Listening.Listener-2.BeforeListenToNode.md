# Listener&lt;TBaseNode, TNode&gt;.BeforeListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [BeforeListenToNode(TBaseNode)](MrKWatkins.Ast.Listening.Listener-2.BeforeListenToNode.md#mrkwatkins-ast-listening-listener-2-beforelistentonode(-0)) |  |
| [BeforeListenToNode(TNode)](MrKWatkins.Ast.Listening.Listener-2.BeforeListenToNode.md#mrkwatkins-ast-listening-listener-2-beforelistentonode(-1)) | Called before a node *and its descendents* are listened to. |

## BeforeListenToNode(TBaseNode) {id="mrkwatkins-ast-listening-listener-2-beforelistentonode(-0)"}

```c#
protected sealed override void BeforeListenToNode(TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-2-beforelistentonode(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode |  |

## BeforeListenToNode(TNode) {id="mrkwatkins-ast-listening-listener-2-beforelistentonode(-1)"}

Called before a node *and its descendents* are listened to.

```c#
protected new virtual void BeforeListenToNode(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-2-beforelistentonode(-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node about to be listened to. |

