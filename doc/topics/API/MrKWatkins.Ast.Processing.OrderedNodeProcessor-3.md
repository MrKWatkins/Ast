# OrderedNodeProcessor&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

Performs some processing on a given node using a processing context in a [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md). The processor can specify the order the pipeline should traverse the tree and whether to process descendents or not.

```c#
public abstract class OrderedNodeProcessor<TContext, TBaseNode, TNode> : OrderedProcessor<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The type of nodes in the tree. |
| TNode | The type of node to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedNodeProcessor()](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-1)) |  |
| [Process(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-2)) | Performs processing on the specified `node`. Does not process any descendents. |
| [ShouldProcessDescendents(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-1)) |  |
| [ShouldProcessDescendents(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-2)) | Whether descendents of this node should be processed by the [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md) or not. Defaults to `true`. |

