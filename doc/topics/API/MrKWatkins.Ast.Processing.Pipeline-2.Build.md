# Pipeline&lt;TContext, TBaseNode&gt;.Build Method
## Definition

Fluent interface to build a [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md).

```c#
public static Pipeline<TContext, TBaseNode> Build(Action<PipelineBuilder<TContext, TBaseNode>> build);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;PipelineBuilder&lt;TContext, TBaseNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [PipelineBuilder&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-2.md) to build the pipeline. |

## Returns

[Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md)

The [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md).
