# PipelineBuilder&lt;TNode&gt;.AddParallelStage Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [UnorderedProcessors](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) in parallel. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. |

## AddParallelStage(Action&lt;ParallelPipelineStageBuilder&lt;TNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))"}

Adds a stage to the pipeline that runs [UnorderedProcessors](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) in parallel. Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddParallelStage(Action<ParallelPipelineStageBuilder<TNode>> build);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;ParallelPipelineStageBuilder&lt;TNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [ParallelPipelineStageBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.ParallelPipelineStageBuilder-1.md) to build the pipeline. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;\[\]) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddParallelStage(UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processor1 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The first processor to add. |
| processor2 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The second processor to add. |
| others | [UnorderedProcessor&lt;TNode&gt;\[\]](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(string, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;\[\]) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddParallelStage(string name, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| processor1 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The first processor to add. |
| processor2 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The second processor to add. |
| others | [UnorderedProcessor&lt;TNode&gt;\[\]](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(int, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;\[\]) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddParallelStage(int maxDegreeOfParallelism, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread will be used to walk the tree and the other threads will be used to process the nodes. |
| processor1 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The first processor to add. |
| processor2 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The second processor to add. |
| others | [UnorderedProcessor&lt;TNode&gt;\[\]](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddParallelStage(string, int, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;, UnorderedProcessor&lt;TNode&gt;\[\]) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism.

```c#
public PipelineBuilder<TNode> AddParallelStage(string name, int maxDegreeOfParallelism, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| maxDegreeOfParallelism | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The maximum degree of parallelism. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread will be used to walk the tree and the other threads will be used to process the nodes. |
| processor1 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The first processor to add. |
| processor2 | [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | The second processor to add. |
| others | [UnorderedProcessor&lt;TNode&gt;\[\]](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
