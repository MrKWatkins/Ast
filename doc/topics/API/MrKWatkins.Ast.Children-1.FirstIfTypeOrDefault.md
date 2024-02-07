# Children&lt;TNode&gt;.FirstIfTypeOrDefault Method
## Definition

Returns the first node in the collection if it is of the specified type or a specified default if the collection is empty or the first node is a different type.

```c#
public TChild FirstIfTypeOrDefault<TChild>(TChild @default = null)
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the node to return. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| default | TChild | The default value to return if the collection is empty or the first node is not of type `TChild`. |

## Returns

TChild

The first node if it is of the specified type or `default` otherwise.
## Remarks

Slightly quicker than [FirstOfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.FirstOfTypeOrDefault.md) if you only care about the first node.
