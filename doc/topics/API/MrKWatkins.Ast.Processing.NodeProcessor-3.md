# NodeProcessor&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

Performs some processing on a given node of a specific type using a processing context in a [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md).

```c#
public abstract class NodeProcessor<TContext, TBaseNode, TNode> : Processor<TContext, TBaseNode>
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
| [NodeProcessor()](MrKWatkins.Ast.Processing.NodeProcessor-3.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.NodeProcessor-3.Process.md#mrkwatkins-ast-processing-nodeprocessor-3-process(-0-1)) | Performs processing on the specified `node` if it is of type `TNode`. Does not process any descendents. |
| [Process(TContext, TNode)](MrKWatkins.Ast.Processing.NodeProcessor-3.Process.md#mrkwatkins-ast-processing-nodeprocessor-3-process(-0-2)) | Performs processing on the specified `node`. Does not process any descendents. |

