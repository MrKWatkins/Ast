# ParallelPipelineStageBuilder&lt;TBaseNode&gt;.WithMaxDegreeOfParallelism Method
## Definition

Sets the maximum degree of parallelism for parallel processing. Defaults to the number of processors in the machine.

```c#
public ParallelPipelineStageBuilder<TBaseNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. |

## Returns

[ParallelPipelineStageBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.md)

The fluent builder.
