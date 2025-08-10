# ParallelPipelineStageBuilder&lt;TContext, TBaseNode&gt;.WithMaxDegreeOfParallelism Method
## Definition

Sets the maximum degree of parallelism for parallel processing. Defaults to the number of processors in the machine.

```c#
public ParallelPipelineStageBuilder<TContext, TBaseNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. |

## Returns

[ParallelPipelineStageBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-2.md)

The fluent builder.
