# SerialPipelineStage&lt;TContext, TBaseNode&gt; Class
## Definition

A [PipelineStage&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineStage-1.md) that runs processors one after the other.

```c#
public sealed class SerialPipelineStage<TContext, TBaseNode> : PipelineStage<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The type of nodes in the tree. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Processors](MrKWatkins.Ast.Processing.SerialPipelineStage-2.Processors.md) | The processors in this stage. |

