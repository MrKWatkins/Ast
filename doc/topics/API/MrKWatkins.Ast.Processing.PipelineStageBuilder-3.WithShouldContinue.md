# PipelineStageBuilder&lt;TSelf, TProcessor, TNode&gt;.WithShouldContinue Method
## Definition

Sets a function to determine whether processing should continue after this stage or not. Any previously registered function will be replaced. By default, processing will not continue if there are any errors in the tree.

```c#
public TSelf WithShouldContinue(Func<TNode, bool> shouldContinue);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| shouldContinue | [Func&lt;TNode, Boolean&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) | A function that takes the root node and returns `true` if processing should continue, `false` otherwise. |

## Returns

TSelf

The fluent builder.
