# Children&lt;TNode&gt;.LastOfType Method
## Definition

Returns the last node in the collection of the specified type or throws otherwise.

```c#
public TChild LastOfType<TChild>()
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the node to return. |

## Returns

TChild

The last node if it is of the specified type.
