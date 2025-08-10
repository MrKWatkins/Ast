# PipelineStage&lt;TContext, TBaseNode&gt;.Run Method
## Definition

Runs the stage.

```c#
public bool Run(TContext? context, TBaseNode root);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run processing on. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the pipeline should proceed to the next stage, `false` otherwise.
