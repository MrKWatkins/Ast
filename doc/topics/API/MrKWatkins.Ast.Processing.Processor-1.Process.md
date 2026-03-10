# Processor&lt;TBaseNode&gt;.Process Method
## Definition

Performs processing on the specified `node`. Does not process any descendents.

```c#
public abstract TBaseNode Process(TBaseNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode | The node to process. |

## Returns

TBaseNode

The root node of the tree, which may have been replaced.

