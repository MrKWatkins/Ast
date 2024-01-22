# OrderedProcessorWithContext&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes of a specific type in a tree in a specified order and gives access to a context object during processing.

```c#
public abstract class OrderedProcessorWithContext<TContext, TBaseNode, TNode> : Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedProcessorWithContext()](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [Traversal](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.Traversal.md) | Override this property to specify the [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to traverse the tree. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md). |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateContext(TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.CreateContext.md) | Override to create the context object. |
| [ProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.ProcessNode.md) | Process the specified node. |
| [ShouldProcessChildren(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.ShouldProcessChildren.md) | Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes. |
| [ShouldProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

