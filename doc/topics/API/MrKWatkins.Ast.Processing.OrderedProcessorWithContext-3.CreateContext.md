# OrderedProcessorWithContext&lt;TContext, TBaseNode, TNode&gt;.CreateContext Method
## Definition

Override to create the context object.

```c#
protected abstract TContext? CreateContext(TBaseNode root);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root node for the processing. |

## Returns

TContext

The context object.
