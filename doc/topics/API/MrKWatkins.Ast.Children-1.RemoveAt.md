# Children&lt;TNode&gt;.RemoveAt Method
## Definition

Removes the node at the specified position from the collection and reset its [Parent](MrKWatkins.Ast.Node-1.Parent.md) property to `null`.

```c#
public TNode RemoveAt(int index);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The position of the node to remove. |

## Returns

TNode

The node that was removed.
