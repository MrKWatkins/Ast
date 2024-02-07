# ITraversal&lt;TNode&gt;.Enumerate Method
## Definition

Enumerates over a node and its descendents.

```c#
public abstract IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, Boolean> shouldEnumerateDescendents = null);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TNode | The root node to enumerate over. |
| includeRoot | [Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean) | Whether to include `root` in the results or not. Defaults to `true`. |
| shouldEnumerateDescendents | [Func&lt;TNode, Boolean&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) | Optional function to specify whether the descendents of a given node should be included or not. If not provided then all descendents will be included. |

## Returns

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy [IEnumerable&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) of the descendents.
