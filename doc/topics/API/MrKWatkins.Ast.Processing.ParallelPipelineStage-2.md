# ParallelPipelineStage&lt;TContext, TBaseNode&gt; Class
## Definition

A [PipelineStage&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineStage-2.md) that runs processors in parallel.

```c#
public sealed class ParallelPipelineStage<TContext, TBaseNode> : PipelineStage<TContext, TBaseNode>
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
| [MaxDegreeOfParallelism](MrKWatkins.Ast.Processing.ParallelPipelineStage-2.MaxDegreeOfParallelism.md) | The maximum degree of parallelism to use when processing nodes. |
| [Processors](MrKWatkins.Ast.Processing.ParallelPipelineStage-2.Processors.md) | The processors in the stage. |

