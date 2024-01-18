# ListenerWithContext&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

A [Listener&lt;TBaseNode, TNode&gt;](MrKWatkins.Ast.Listening.Listener-2.md) that only listens to nodes of a specific type. All other nodes will be ignored. The listener will still proceed to descendents of nodes that aren&#39;t listened too, i.e. the entire tree will be walked.

```c#
public abstract class ListenerWithContext<TContext, TBaseNode, TNode> : ListenerWithContext<TContext, TBaseNode>
   where TContext
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TBaseNode | The base type of all nodes in the tree. |
| TNode | The type of the nodes to listen to. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [ListenerWithContext()](MrKWatkins.Ast.Listening.ListenerWithContext-3.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.AfterListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-1)) |  |
| [AfterListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.AfterListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-afterlistentonode(-0-2)) | Called after a node *and its descendents* have been listened to. |
| [BeforeListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.BeforeListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-1)) |  |
| [BeforeListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.BeforeListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-beforelistentonode(-0-2)) | Called before a node *and its descendents* are listened to. |
| [ListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.ListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-listentonode(-0-1)) |  |
| [ListenToNode](MrKWatkins.Ast.Listening.ListenerWithContext-3.ListenToNode.md#mrkwatkins-ast-listening-listenerwithcontext-3-listentonode(-0-2)) | Called when the node is listened to. |

