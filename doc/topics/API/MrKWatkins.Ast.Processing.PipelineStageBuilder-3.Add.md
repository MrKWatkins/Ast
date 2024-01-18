# PipelineStageBuilder&lt;TSelf, TProcessor, TNode&gt;.Add Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Add&lt;TConstructableProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-3-add-1) | Adds a [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage. |
| [Add(TProcessor, TProcessor\[\])](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-1())) | Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage. |

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
## Add(TProcessor, TProcessor\[\]) {id="mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-1())"}

Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage.

```c#
public TSelf Add(TProcessor processor, params TProcessor[] others);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-1())"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| processor | TProcessor | The first processor to add. |
| others | TProcessor\[\] | Other processors to add. |

## Returns {id="returns-mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-1())"}

TSelf

The fluent builder.
