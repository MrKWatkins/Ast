# PipelineStageBuilder&lt;TSelf, TStage, TBaseNode, TProcessor, TShouldContinue&gt;.WithShouldContinue Method
## Definition

Sets a function to determine whether processing should continue after this stage or not. Any previously registered function will be replaced. By default, processing will not continue if there are any errors in the tree.

```c#
public TSelf WithShouldContinue(TShouldContinue? shouldContinue);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| shouldContinue | TShouldContinue | A function that takes the root node and returns `true` if processing should continue, `false` otherwise. |

## Returns

TSelf

The fluent builder.
