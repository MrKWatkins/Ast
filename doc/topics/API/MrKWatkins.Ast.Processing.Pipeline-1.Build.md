# Pipeline&lt;TNode&gt;.Build Method
## Definition

Fluent interface to build a [Pipeline&lt;TNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md).

```c#
public static Pipeline<TNode> Build(Action<PipelineBuilder<TNode>> build);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| build | [Action&lt;PipelineBuilder&lt;TNode&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Action-1) | An action to perform on a [PipelineBuilder&lt;TNode&gt;](MrKWatkins.Ast.Processing.PipelineBuilder-1.md) to build the pipeline. |

## Returns

[Pipeline&lt;TNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md)

The [Pipeline&lt;TNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md).
