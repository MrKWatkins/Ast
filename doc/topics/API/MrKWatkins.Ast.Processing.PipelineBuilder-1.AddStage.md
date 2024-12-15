# PipelineBuilder&lt;TNode&gt;.AddStage Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddStage(Action&lt;SerialPipelineStageBuilder&lt;TNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) serially. Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1) | Adds a stage to the pipeline with a single [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;(String)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)) | Adds a stage with the specified name to the pipeline with a single [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |
| [AddStage(Processor&lt;TNode&gt;, IEnumerable&lt;Processor&lt;TNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. Its name will be the number of the stage. |
| [AddStage(String, Processor&lt;TNode&gt;, IEnumerable&lt;Processor&lt;TNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. |

## AddStage(Action&lt;SerialPipelineStageBuilder&lt;TNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))"}

Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) serially. Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddStage(Action<SerialPipelineStageBuilder<TNode>> build);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;SerialPipelineStageBuilder&lt;TNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [SerialPipelineStageBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.SerialPipelineStageBuilder-1.md) to build the pipeline. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage&lt;TProcessor&gt;() {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1"}

Adds a stage to the pipeline with a single [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddStage<TProcessor>()
   where TProcessor : Processor<TNode>, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1"}

| Name | Description |
| ---- | ----------- |
| TProcessor | The type of the [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage&lt;TProcessor&gt;(String) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

Adds a stage with the specified name to the pipeline with a single [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md).

```c#
public PipelineBuilder<TNode> AddStage<TProcessor>(string name)
   where TProcessor : Processor<TNode>, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

| Name | Description |
| ---- | ----------- |
| TProcessor | The type of the [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage(Processor&lt;TNode&gt;, IEnumerable&lt;Processor&lt;TNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage(mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. Its name will be the number of the stage.

```c#
public PipelineBuilder<TNode> AddStage(Processor<TNode> processor, params IEnumerable<Processor<TNode>> others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processor | [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) | The first processor to add. |
| others | [IEnumerable&lt;Processor&lt;TNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
## AddStage(String, Processor&lt;TNode&gt;, IEnumerable&lt;Processor&lt;TNode&gt;&gt;) {id="mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially.

```c#
public PipelineBuilder<TNode> AddStage(string name, Processor<TNode> processor, params IEnumerable<Processor<TNode>> others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the stage. |
| processor | [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) | The first processor to add. |
| others | [IEnumerable&lt;Processor&lt;TNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-mrkwatkins-ast-processing-processor((-0))-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))"}

[PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md)

The fluent builder.
