# Pipeline&lt;TBaseNode&gt;.Build Method
## Definition

Fluent interface to build a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md).

```c#
public static Pipeline<TBaseNode> Build(Action<PipelineBuilder<TBaseNode>> build);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;PipelineBuilder&lt;TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [PipelineBuilder&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md) to build the pipeline. |

## Returns

[Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md)

The [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md).
