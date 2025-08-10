# PipelineStage&lt;TBaseNode&gt;.Run Method
## Definition

Runs the stage.

```c#
public bool Run(TBaseNode root);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root node to run processing on. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the pipeline should proceed to the next stage, `false` otherwise.
