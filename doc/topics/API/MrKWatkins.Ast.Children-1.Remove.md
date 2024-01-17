# Children&lt;TNode&gt;.Remove Method
## Definition

Tries to remove a node from the collection and reset its [Parent](MrKWatkins.Ast.Node-1.Parent.md) property to `null`.

```c#
public bool Remove(TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node to remove. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `node` was in the collection and was removed, `false` otherwise.
