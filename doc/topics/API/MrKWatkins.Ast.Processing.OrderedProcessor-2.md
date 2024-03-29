# OrderedProcessor&lt;TBaseNode, TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes of a specific type in a tree in a specified order.

```c#
public abstract class OrderedProcessor<TBaseNode, TNode> : Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedProcessor()](MrKWatkins.Ast.Processing.OrderedProcessor-2.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [Traversal](MrKWatkins.Ast.Processing.OrderedProcessor-2.Traversal.md) | Override this property to specify the [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to traverse the tree. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md). |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode(TNode)](MrKWatkins.Ast.Processing.OrderedProcessor-2.ProcessNode.md) | Process the specified node. |
| [ShouldProcessChildren(TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessor-2.ShouldProcessChildren.md) | Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes. |
| [ShouldProcessNode(TNode)](MrKWatkins.Ast.Processing.OrderedProcessor-2.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

