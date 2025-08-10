# OrderedProcessor&lt;TContext, TBaseNode&gt; Class
## Definition

Performs some processing on a given node using a processing context in a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md). The processor can specify the order the pipeline should traverse the tree and whether to process descendents or not.

```c#
public abstract class OrderedProcessor<TContext, TBaseNode> : Processor<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [OrderedProcessor()](MrKWatkins.Ast.Processing.OrderedProcessor-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [GetTraversal(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessor-2.GetTraversal.md) | Gets the traversal that the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) should use for this processor. |
| [ShouldProcessDescendents(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedProcessor-2.ShouldProcessDescendents.md) | Whether descendents of this node should be processed by the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) or not. Defaults to `true`. |

