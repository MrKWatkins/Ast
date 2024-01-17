# ListenerWithContext&lt;TContext, TBaseNode, TNode&gt;.BeforeListenToNode Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [BeforeListenToNode(TContext, TBaseNode)](MrKWatkins.Ast.Listening.ListenerWithContext-3.BeforeListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-1)) |  |
| [BeforeListenToNode(TContext, TNode)](MrKWatkins.Ast.Listening.ListenerWithContext-3.BeforeListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-2)) | Called before a node *and its descendents* are listened to. |

## BeforeListenToNode(TContext, TBaseNode) {id="mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-1)"}

```c#
protected sealed override void BeforeListenToNode(TContext context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## BeforeListenToNode(TContext, TNode) {id="mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-2)"}

Called before a node *and its descendents* are listened to.

```c#
protected new virtual void BeforeListenToNode(TContext context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node about to be listened to. |

