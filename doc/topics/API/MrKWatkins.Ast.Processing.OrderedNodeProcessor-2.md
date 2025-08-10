# OrderedNodeProcessor&lt;TBaseNode, TNode&gt; Class
## Definition

Performs some processing on a given node in a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md). The processor can specify the order the pipeline should traverse the tree and whether to process descendents or not.

```c#
public abstract class OrderedNodeProcessor<TBaseNode, TNode> : OrderedProcessor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The type of nodes in the tree. |
| TNode | The type of node to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedNodeProcessor()](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-2-process(-0)) |  |
| [Process(TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-2-process(-1)) | Performs processing on the specified `node`. Does not process any descendents. |
| [ShouldProcessDescendents(TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-0)) |  |
| [ShouldProcessDescendents(TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-1)) | Whether descendents of this node should be processed by the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) or not. Defaults to `true`. |

