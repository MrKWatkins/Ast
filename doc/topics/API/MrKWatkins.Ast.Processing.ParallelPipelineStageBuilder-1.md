# ParallelPipelineStageBuilder&lt;TNode&gt; Class
## Definition

Fluent builder for a parallel pipeline stage.

```c#
public sealed class ParallelPipelineStageBuilder<TNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TNode>, UnorderedProcessor<TNode>, TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [WithMaxDegreeOfParallelism](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.WithMaxDegreeOfParallelism.md) | Sets the maximum degree of parallelism for parallel processing. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread will be used to walk the tree and the other threads will be used to process the nodes. Defaults to the number of processors in the machine. |

