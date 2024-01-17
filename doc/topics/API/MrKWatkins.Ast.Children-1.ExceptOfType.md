# Children&lt;TNode&gt;.ExceptOfType Method
## Definition

Lazily enumerates over all nodes in this collection not of the specified type.

```c#
public IEnumerable<TNode> ExceptOfType<TChild>()
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the nodes not to return. |

## Returns

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of all nodes in this collection not of type `TChild`.
