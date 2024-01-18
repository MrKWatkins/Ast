# Listener&lt;TBaseNode, TNode&gt; Class
## Definition

A [Listener&lt;TNode&gt;](MrKWatkins.Ast.Listening.Listener-1.md) that only listens to nodes of a specific type. All other nodes will be ignored. The listener will still proceed to descendents of nodes that aren&#39;t listened too, i.e. the entire tree will be walked.

```c#
public abstract class Listener<TBaseNode, TNode> : Listener<TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of all nodes in the tree. |
| TNode | The type of the nodes to listen to. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Listener()](MrKWatkins.Ast.Listening.Listener-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode](MrKWatkins.Ast.Listening.Listener-2.AfterListenToNode.md#mrkwatkins-ast-listening-listener-2-afterlistentonode(-0)) |  |
| [AfterListenToNode](MrKWatkins.Ast.Listening.Listener-2.AfterListenToNode.md#mrkwatkins-ast-listening-listener-2-afterlistentonode(-1)) | Called after a node *and its descendents* have been listened to. |
| [BeforeListenToNode](MrKWatkins.Ast.Listening.Listener-2.BeforeListenToNode.md#mrkwatkins-ast-listening-listener-2-beforelistentonode(-0)) |  |
| [BeforeListenToNode](MrKWatkins.Ast.Listening.Listener-2.BeforeListenToNode.md#mrkwatkins-ast-listening-listener-2-beforelistentonode(-1)) | Called before a node *and its descendents* are listened to. |
| [ListenToNode](MrKWatkins.Ast.Listening.Listener-2.ListenToNode.md#mrkwatkins-ast-listening-listener-2-listentonode(-0)) |  |
| [ListenToNode](MrKWatkins.Ast.Listening.Listener-2.ListenToNode.md#mrkwatkins-ast-listening-listener-2-listentonode(-1)) | Called when the node is listened to. |

