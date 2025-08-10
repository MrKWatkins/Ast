# PipelineStageBuilder&lt;TSelf, TStage, TBaseNode, TProcessor, TShouldContinue&gt;.Add Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Add&lt;TConstructableProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-5-add-1) | Adds a [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage. |
| [Add(IEnumerable&lt;TProcessor&gt;)](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-5-add(system-collections-generic-ienumerable((-3)))) | Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage. |

## Add&lt;TConstructableProcessor&gt;() {id="mrkwatkins-ast-processing-pipelinestagebuilder-5-add-1"}

Adds a [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage.

```c#
public TSelf Add<TConstructableProcessor>()
   where TConstructableProcessor : TProcessor, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinestagebuilder-5-add-1"}

| Name | Description |
| ---- | ----------- |
| TConstructableProcessor | The type of the processor to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinestagebuilder-5-add-1"}

TSelf

The fluent builder.
## Add(IEnumerable&lt;TProcessor&gt;) {id="mrkwatkins-ast-processing-pipelinestagebuilder-5-add(system-collections-generic-ienumerable((-3)))"}

Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage.

```c#
public TSelf Add(params IEnumerable<TProcessor> processors);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinestagebuilder-5-add(system-collections-generic-ienumerable((-3)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processors | [IEnumerable&lt;TProcessor&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinestagebuilder-5-add(system-collections-generic-ienumerable((-3)))"}

TSelf

The fluent builder.
