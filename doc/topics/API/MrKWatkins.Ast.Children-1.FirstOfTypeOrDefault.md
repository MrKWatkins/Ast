# Children&lt;TNode&gt;.FirstOfTypeOrDefault Method
## Definition

Returns the first node in the collection of the specified type or a specified default if it doesn&#39;t contain any nodes of the specified type.

```c#
public TChild FirstOfTypeOrDefault<TChild>(TChild @default = null)
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

The first node if it is of the specified type or `default` if it doesn&#39;t contain any nodes of the specified type.
