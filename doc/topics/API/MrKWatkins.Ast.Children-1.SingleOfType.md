# Children&lt;TNode&gt;.SingleOfType Method
## Definition

Returns the only node in the collection of the specified type. Throws if there is not exactly one node in the collection of the specified type.

```c#
public TChild SingleOfType<TChild>()
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the node to return. |

## Returns

TChild

The single node in the collection of type `TChild`.
