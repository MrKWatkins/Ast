# ListenerWithContext&lt;TContext, TNode&gt; Class
## Definition

A listener for a syntax tree that passes a context object to the listen methods. A listener walks the tree and gets notified when nodes are reached. An alternative to processing. Useful to build something completely new from the tree. Processing is more useful to mutate the tree.

```c#
public abstract class ListenerWithContext<TContext, TNode>
   where TContext
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TNode | The type of the nodes to listen to. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [ListenerWithContext()](MrKWatkins.Ast.Listening.ListenerWithContext-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AfterListenToNode(TContext, TNode)](MrKWatkins.Ast.Listening.ListenerWithContext-2.AfterListenToNode.md) | Called after a node *and its descendents* have been listened to. |
| [BeforeListenToNode(TContext, TNode)](MrKWatkins.Ast.Listening.ListenerWithContext-2.BeforeListenToNode.md) | Called before a node *and its descendents* are listened to. |
| [Listen(TContext, TNode)](MrKWatkins.Ast.Listening.ListenerWithContext-2.Listen.md) | Listen to the specified node and its descendents. |
| [ListenToNode(TContext, TNode)](MrKWatkins.Ast.Listening.ListenerWithContext-2.ListenToNode.md) | Called when the node is listened to. |
| [ShouldListenToChildren(TContext, TNode)](MrKWatkins.Ast.Listening.ListenerWithContext-2.ShouldListenToChildren.md) | Return a value indicating whether child nodes should be listened to or not. Defaults to `true`. |

