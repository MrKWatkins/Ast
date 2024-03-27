# Children&lt;TNode&gt;.LastOfTypeOrDefault Method
## Definition

Returns the last node in the collection of the specified type or a specified default if it doesn&#39;t contain any nodes of the specified type.

```c#
public TChild? LastOfTypeOrDefault<TChild>(TChild? @default = null)
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

The last node if it is of the specified type or `default` if it doesn&#39;t contain any nodes of the specified type.
