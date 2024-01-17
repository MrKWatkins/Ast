# Children&lt;TNode&gt;.FirstOfType Method
## Definition

Returns the first node in the collection of the specified type or throws otherwise.

```c#
public TChild FirstOfType<TChild>()
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the node to return. |

## Returns

TChild

The first node if it is of the specified type.
