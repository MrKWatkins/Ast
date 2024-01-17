# Children&lt;TNode&gt;.SingleOfTypeOrDefault Method
## Definition

Returns the only node in the collection of the specified type. Returns the specified default if there are no nodes in the collection of the specified type. Throws if there are multiple nodes in the collection of the specified type.

```c#
public TChild SingleOfTypeOrDefault<TChild>(TChild @default = null)
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the node to return. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| default | TChild | The default value to return if the collection does not contain any nodes of type `TChild`. |

## Returns

TChild

The single node if it is of type `TChild` or `default` if there are no nodes of type `TChild`.
