# Children&lt;TNode&gt;.OfType Method
## Definition

Lazily enumerates over all nodes in this collection of the specified type.

```c#
public IEnumerable<TChild> OfType<TChild>()
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the nodes to return. |

## Returns

[IEnumerable&lt;TChild&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of all nodes in this collection of type `TChild`.
