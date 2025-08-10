# PipelineBuilder&lt;TContext, TBaseNode&gt;.AddStage Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddStage(Action&lt;SerialPipelineStageBuilder&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0-1)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-2.md) serially. Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1) | Adds a stage to the pipeline with a single [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;(String)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1(system-string)) | Adds a stage with the specified name to the pipeline with a single [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). |
| [AddStage(IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run serially. Its name will be the number of the stage. |
| [AddStage(String, IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run serially. |

## AddStage(Action&lt;SerialPipelineStageBuilder&lt;TContext, TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0-1)))))"}

Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-2.md) serially. Its name will be the number of the stage.

```c#
public PipelineBuilder<TContext, TBaseNode> AddStage(Action<SerialPipelineStageBuilder<TContext, TBaseNode>> build);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0-1)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;SerialPipelineStageBuilder&lt;TContext, TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [SerialPipelineStageBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.SerialPipelineStageBuilder-2.md) to build the pipeline. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0-1)))))"}

[PipelineBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-2.md)

The fluent builder.
## AddStage&lt;TProcessor&gt;() {id="mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1"}

Adds a stage to the pipeline with a single [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). Its name will be the number of the stage.

```c#
public PipelineBuilder<TContext, TBaseNode> AddStage<TProcessor>()
   where TProcessor : Processor<TContext, TBaseNode>, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1"}

| Name | Description |
| ---- | ----------- |
| TProcessor | The type of the [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1"}

[PipelineBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-2.md)

The fluent builder.
## AddStage&lt;TProcessor&gt;(String) {id="mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1(system-string)"}

Adds a stage with the specified name to the pipeline with a single [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md).

```c#
public PipelineBuilder<TContext, TBaseNode> AddStage<TProcessor>(string name)
   where TProcessor : Processor<TContext, TBaseNode>, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1(system-string)"}

| Name | Description |
| ---- | ----------- |
| TProcessor | The type of the [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). |

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1(system-string)"}

[PipelineBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-2.md)

The fluent builder.
## AddStage(IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run serially. Its name will be the number of the stage.

```c#
public PipelineBuilder<TContext, TBaseNode> AddStage(params IEnumerable<Processor<TContext, TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processors | [IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))"}

[PipelineBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-2.md)

The fluent builder.
## AddStage(String, IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run serially.

```c#
public PipelineBuilder<TContext, TBaseNode> AddStage(string name, params IEnumerable<Processor<TContext, TBaseNode>> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| processors | [IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))"}

[PipelineBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-2.md)

The fluent builder.
