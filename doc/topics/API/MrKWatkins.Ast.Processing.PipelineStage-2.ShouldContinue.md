# PipelineStage&lt;TContext, TBaseNode&gt;.ShouldContinue Property
## Definition

Function to run after the stage to determine if the pipeline should move on to the next stage or not.

```c#
public Func<TContext, TBaseNode, bool> ShouldContinue { get; }
```

## Property Value

[Func&lt;TContext, TBaseNode, Boolean&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-3)
