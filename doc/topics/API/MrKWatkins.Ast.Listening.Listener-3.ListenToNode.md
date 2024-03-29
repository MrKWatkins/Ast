# Listener&lt;TContext, TBaseNode, TNode&gt;.ListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [ListenToNode(TContext, TBaseNode)](MrKWatkins.Ast.Listening.Listener-3.ListenToNode.md#mrkwatkins-ast-listening-listener-3-listentonode(-0-1)) |  |
| [ListenToNode(TContext, TNode)](MrKWatkins.Ast.Listening.Listener-3.ListenToNode.md#mrkwatkins-ast-listening-listener-3-listentonode(-0-2)) | Called when the node is listened to. |

## ListenToNode(TContext, TBaseNode) {id="mrkwatkins-ast-listening-listener-3-listentonode(-0-1)"}

```c#
protected sealed override void ListenToNode(TContext? context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-3-listentonode(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## ListenToNode(TContext, TNode) {id="mrkwatkins-ast-listening-listener-3-listentonode(-0-2)"}

Called when the node is listened to.

```c#
protected new virtual void ListenToNode(TContext? context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listener-3-listentonode(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node being listened to. |

