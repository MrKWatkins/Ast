# NodeProcessor&lt;TBaseNode, TNode&gt; Class
## Definition

Performs some processing on a given node of a specific type in a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md).

```c#
public abstract class NodeProcessor<TBaseNode, TNode> : Processor<TBaseNode>
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
| [NodeProcessor()](MrKWatkins.Ast.Processing.NodeProcessor-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.NodeProcessor-2.Process.md#mrkwatkins-ast-processing-nodeprocessor-2-process(-0)) | Performs processing on the specified `node` if it is of type `TNode`. Does not process any descendents. |
| [Process(TNode)](MrKWatkins.Ast.Processing.NodeProcessor-2.Process.md#mrkwatkins-ast-processing-nodeprocessor-2-process(-1)) | Performs processing on the specified `node`. Does not process any descendents. |

