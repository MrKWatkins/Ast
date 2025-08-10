# OrderedProcessor&lt;TBaseNode&gt; Class
## Definition

Performs some processing on a given node in a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md). The processor can specify the order the pipeline should traverse the tree and whether to process descendents or not.

```c#
public abstract class OrderedProcessor<TBaseNode> : Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedProcessor()](MrKWatkins.Ast.Processing.OrderedProcessor-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [GetTraversal(TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessor-1.GetTraversal.md) | Gets the traversal that the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) should use for this processor. |
| [ShouldProcessDescendents(TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessor-1.ShouldProcessDescendents.md) | Whether descendents of this node should be processed by the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) or not. Defaults to `true`. |

