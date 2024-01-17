# Node&lt;TNode&gt;.ThisAnd Method
## Definition

Lazily enumerates over this node and then the specified enumeration of nodes.

```c#
protected IEnumerable<TNode> ThisAnd(IEnumerable<TNode> and);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| and | [IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The nodes to enumerate over after this. |

## Returns

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of this node and `and`.
