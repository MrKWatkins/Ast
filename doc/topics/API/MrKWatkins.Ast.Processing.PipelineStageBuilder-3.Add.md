# PipelineStageBuilder&lt;TSelf, TProcessor, TNode&gt;.Add Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Add&lt;TConstructableProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-3-add-1) | Adds a [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage. |
| [Add(TProcessor, IEnumerable&lt;TProcessor&gt;)](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-system-collections-generic-ienumerable((-1)))) | Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage. |

## Add&lt;TConstructableProcessor&gt;() {id="mrkwatkins-ast-processing-pipelinestagebuilder-3-add-1"}

Adds a [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage.

```c#
public TSelf Add<TConstructableProcessor>()
   where TConstructableProcessor : TProcessor, new();
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-processing-pipelinestagebuilder-3-add-1"}

| Name | Description |
| ---- | ----------- |
| TConstructableProcessor | The type of the processor to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinestagebuilder-3-add-1"}

TSelf

The fluent builder.
## Add(TProcessor, IEnumerable&lt;TProcessor&gt;) {id="mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-system-collections-generic-ienumerable((-1)))"}

Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage.

```c#
public TSelf Add(TProcessor processor, params IEnumerable<TProcessor> others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-system-collections-generic-ienumerable((-1)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processor | TProcessor | The first processor to add. |
| others | [IEnumerable&lt;TProcessor&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-system-collections-generic-ienumerable((-1)))"}

TSelf

The fluent builder.
