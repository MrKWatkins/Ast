# Processor&lt;TContext, TBaseNode&gt;.Process Method
## Definition

Performs processing on the specified `node`. Does not process any descendents.

```c#
public abstract TBaseNode Process(TContext context, TBaseNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TBaseNode | The node to process. |

## Returns

TBaseNode

The root node of the tree, which may have been replaced.
