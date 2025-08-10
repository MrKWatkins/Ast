# ParallelPipelineStageBuilder&lt;TBaseNode&gt; Class
## Definition

Fluent builder for a parallel pipeline stage.

```c#
public sealed class ParallelPipelineStageBuilder<TBaseNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TBaseNode>, ParallelPipelineStage<TBaseNode>, TBaseNode, Processor<TBaseNode>, Func<TBaseNode, Boolean>>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [WithAlwaysContinue()](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.WithAlwaysContinue.md) | Always continue to the next stage after this one, irrespective of errors in the tree. |
| [WithMaxDegreeOfParallelism(Int32)](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.WithMaxDegreeOfParallelism.md) | Sets the maximum degree of parallelism for parallel processing. Defaults to the number of processors in the machine. |

