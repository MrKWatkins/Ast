# OrderedProcessorWithContext&lt;TContext, TNode&gt;.CreateContext Method
## Definition

Override to create the context object.

```c#
protected abstract TContext? CreateContext(TNode root);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TNode | The root node for the processing. |

## Returns

TContext

The context object.
