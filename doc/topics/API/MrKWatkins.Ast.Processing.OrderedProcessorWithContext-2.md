# OrderedProcessorWithContext&lt;TContext, TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes in a tree in a specified order and gives access to a context object during processing.

```c#
public abstract class OrderedProcessorWithContext<TContext, TNode> : Processor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedProcessorWithContext()](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [Traversal](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.Traversal.md) | Override this property to specify the [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to traverse the tree. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md). |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateContext(TNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.CreateContext.md) | Override to create the context object. |
| [ProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.ProcessNode.md) | Process the specified node. |
| [ShouldProcessChildren(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.ShouldProcessChildren.md) | Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes. |
| [ShouldProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

