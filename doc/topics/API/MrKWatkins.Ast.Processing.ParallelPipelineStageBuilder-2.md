# ParallelPipelineStageBuilder&lt;TContext, TBaseNode&gt; Class
## Definition

Fluent builder for a parallel pipeline stage.

```c#
public sealed class ParallelPipelineStageBuilder<TContext, TBaseNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TContext, TBaseNode>, ParallelPipelineStage<TContext, TBaseNode>, TBaseNode, Processor<TContext, TBaseNode>, Func<TContext, TBaseNode, Boolean>>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context for the processing. |
| TBaseNode | The base type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [WithAlwaysContinue()](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-2.WithAlwaysContinue.md) | Always continue to the next stage after this one, irrespective of errors in the tree. |
| [WithMaxDegreeOfParallelism(Int32)](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-2.WithMaxDegreeOfParallelism.md) | Sets the maximum degree of parallelism for parallel processing. Defaults to the number of processors in the machine. |

