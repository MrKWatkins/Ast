# ParallelPipelineStage&lt;TBaseNode&gt; Class
## Definition

A [PipelineStage&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineStage-1.md) that runs processors in parallel.

```c#
public sealed class ParallelPipelineStage<TBaseNode> : PipelineStage<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The type of nodes in the tree. |

## Properties

| Name | Description |
| ---- | ----------- |
| [MaxDegreeOfParallelism](MrKWatkins.Ast.Processing.ParallelPipelineStage-1.MaxDegreeOfParallelism.md) | The maximum degree of parallelism to use when processing nodes. |
| [Processors](MrKWatkins.Ast.Processing.ParallelPipelineStage-1.Processors.md) | The processors in the stage. |

