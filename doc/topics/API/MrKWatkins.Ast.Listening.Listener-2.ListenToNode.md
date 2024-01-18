# Listener&lt;TBaseNode, TNode&gt;.ListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [ListenToNode(TBaseNode)](MrKWatkins.Ast.Listening.Listener-2.ListenToNode.md#mrkwatkins-ast-listening-listener-2-listentonode(-0)) |  |
| [ListenToNode(TNode)](MrKWatkins.Ast.Listening.Listener-2.ListenToNode.md#mrkwatkins-ast-listening-listener-2-listentonode(-1)) | Called when the node is listened to. |

## ListenToNode(TBaseNode) {id="mrkwatkins-ast-listening-listener-2-listentonode(-0)"}

```c#
protected sealed override void ListenToNode(TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-2-listentonode(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode |  |

## ListenToNode(TNode) {id="mrkwatkins-ast-listening-listener-2-listentonode(-1)"}

Called when the node is listened to.

```c#
protected new virtual void ListenToNode(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-2-listentonode(-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node being listened to. |

