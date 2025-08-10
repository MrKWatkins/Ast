# PipelineBuilder&lt;TBaseNode&gt;.AddParallelStage Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddParallelStage(Action&lt;ParallelPipelineStageBuilder&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) in parallel. Its name will be the number of the stage. |
| [AddParallelStage(IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage(String, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage(Int32, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage. |
| [AddParallelStage(String, Int32, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. |

## AddParallelStage(Action&lt;ParallelPipelineStageBuilder&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))"}

Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) in parallel. Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddParallelStage(Action<ParallelPipelineStageBuilder<TBaseNode>> build);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;ParallelPipelineStageBuilder&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [ParallelPipelineStageBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.md) to build the pipeline. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddParallelStage(params IEnumerable<Processor<TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processors | [IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(String, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddParallelStage(string name, params IEnumerable<Processor<TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| processors | [IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(Int32, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddParallelStage(int maxDegreeOfParallelism, params IEnumerable<Processor<TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. If set to one, then the stage will proceed in serial. If greater than one, then one thread will be used to walk the tree and the other threads will be used to process the nodes. |
| processors | [IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(String, Int32, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism.

```c#
public PipelineBuilder<TBaseNode> AddParallelStage(string name, int maxDegreeOfParallelism, params IEnumerable<Processor<TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. If set to one, then the stage will proceed in serial. If greater than one, then one thread will be used to walk the tree and the other threads will be used to process the nodes. |
| processors | [IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
