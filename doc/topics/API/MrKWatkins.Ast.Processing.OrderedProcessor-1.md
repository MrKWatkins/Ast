# OrderedProcessor&lt;TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes in a tree in a specified order.

```c#
public abstract class OrderedProcessor<TNode> : Processor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedProcessor()](MrKWatkins.Ast.Processing.OrderedProcessor-1.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [Traversal](MrKWatkins.Ast.Processing.OrderedProcessor-1.Traversal.md) | Override this property to specify the [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to traverse the tree. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md). |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode(TNode)](MrKWatkins.Ast.Processing.OrderedProcessor-1.ProcessNode.md) | Process the specified node. |
| [ShouldProcessChildren(TNode)](MrKWatkins.Ast.Processing.OrderedProcessor-1.ShouldProcessChildren.md) | Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes. |
| [ShouldProcessNode(TNode)](MrKWatkins.Ast.Processing.OrderedProcessor-1.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

