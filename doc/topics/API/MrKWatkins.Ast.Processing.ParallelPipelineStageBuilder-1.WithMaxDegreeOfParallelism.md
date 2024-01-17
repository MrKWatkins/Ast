# ParallelPipelineStageBuilder&lt;TNode&gt;.WithMaxDegreeOfParallelism Method
## Definition

Sets the maximum degree of parallelism for parallel processing. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread will be used to walk the tree and the other threads will be used to process the nodes. Defaults to the number of processors in the machine.

```c#
public ParallelPipelineStageBuilder<TNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. |

## Returns

[ParallelPipelineStageBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.md)

The fluent builder.
