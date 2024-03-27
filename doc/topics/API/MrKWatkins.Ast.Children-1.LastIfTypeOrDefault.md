# Children&lt;TNode&gt;.LastIfTypeOrDefault Method
## Definition

Returns the last node in the collection if it is of the specified type or a specified default if the collection is empty or the last node is a different type.

```c#
public TChild? LastIfTypeOrDefault<TChild>(TChild? @default = null)
   where TChild : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TChild | The type of the node to return. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| default | TChild | The default value to return if the collection is empty or the last node is not of type `TChild`. |

## Returns

TChild

The last node if it is of the specified type or `default` otherwise.
## Remarks

Slightly quicker than [LastOfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.LastOfTypeOrDefault.md) if you only care about the last node.
