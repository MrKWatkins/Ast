# DepthFirstPreOrderTraversal&lt;TNode&gt;.Enumerate Method
## Definition

```c#
public IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TNode |  |
| includeRoot | [Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean) |  |
| shouldEnumerateDescendents | [Func&lt;TNode, Boolean&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) |  |

## Returns

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
