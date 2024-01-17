# OrderedProcessor&lt;TBaseNode, TNode&gt;.ShouldProcessNode Method
## Definition

Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes.

```c#
protected virtual bool ShouldProcessNode(TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `node` should be processed, `false` otherwise.
