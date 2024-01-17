# Listener&lt;TNode&gt; Class
## Definition

A listener for a syntax tree. A listener walks the tree and gets notified when nodes are reached. An alternative to processing. Useful to build something completely new from the tree. Processing is more useful to mutate the tree.

```c#
public abstract class Listener<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of the nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Listener()](MrKWatkins.Ast.Listening.Listener-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode(TNode)](MrKWatkins.Ast.Listening.Listener-1.AfterListenToNode.md) | Called after a node *and its descendents* have been listened to. |
| [BeforeListenToNode(TNode)](MrKWatkins.Ast.Listening.Listener-1.BeforeListenToNode.md) | Called before a node *and its descendents* are listened to. |
| [Listen(TNode)](MrKWatkins.Ast.Listening.Listener-1.Listen.md) | Listen to the specified node and its descendents. |
| [ListenToNode(TNode)](MrKWatkins.Ast.Listening.Listener-1.ListenToNode.md) | Called when the node is listened to. |
| [ShouldListenToChildren(TNode)](MrKWatkins.Ast.Listening.Listener-1.ShouldListenToChildren.md) | Return a value indicating whether child nodes should be listened to or not. Defaults to `true`. |

