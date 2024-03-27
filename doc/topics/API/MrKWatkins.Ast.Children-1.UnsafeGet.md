# Children&lt;TNode&gt;.UnsafeGet Method
## Definition

Gets the child at the specified index in the collection without array bounds checks. For high performance scenarios. WARNING: Do not use unless you are certain of the number of children!

```c#
public TNode UnsafeGet(int index);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The index of the child to get. |

## Returns

TNode

The node at `index`.
