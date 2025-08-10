# PipelineBuilder&lt;TBaseNode&gt;.AddStage Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddStage(Action&lt;SerialPipelineStageBuilder&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) serially. Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1) | Adds a stage to the pipeline with a single [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;(String)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)) | Adds a stage with the specified name to the pipeline with a single [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |
| [AddStage(IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. Its name will be the number of the stage. |
| [AddStage(String, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. |

## AddStage(Action&lt;SerialPipelineStageBuilder&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))"}

Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) serially. Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddStage(Action<SerialPipelineStageBuilder<TBaseNode>> build);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;SerialPipelineStageBuilder&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [SerialPipelineStageBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.SerialPipelineStageBuilder-1.md) to build the pipeline. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage&lt;TProcessor&gt;() {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1"}

Adds a stage to the pipeline with a single [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddStage<TProcessor>()
   where TProcessor : Processor<TBaseNode>, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1"}

| Name | Description |
| ---- | ----------- |
| TProcessor | The type of the [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage&lt;TProcessor&gt;(String) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

Adds a stage with the specified name to the pipeline with a single [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md).

```c#
public PipelineBuilder<TBaseNode> AddStage<TProcessor>(string name)
   where TProcessor : Processor<TBaseNode>, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

| Name | Description |
| ---- | ----------- |
| TProcessor | The type of the [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage(IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. Its name will be the number of the stage.

```c#
public PipelineBuilder<TBaseNode> AddStage(params IEnumerable<Processor<TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processors | [IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage(String, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially.

```c#
public PipelineBuilder<TBaseNode> AddStage(string name, params IEnumerable<Processor<TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| processors | [IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
