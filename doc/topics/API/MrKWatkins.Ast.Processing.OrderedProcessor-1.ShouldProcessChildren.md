# OrderedProcessor&lt;TNode&gt;.ShouldProcessChildren Method
## Definition

Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes.

```c#
protected virtual bool ShouldProcessChildren(TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if children should be processed, `false` otherwise.
