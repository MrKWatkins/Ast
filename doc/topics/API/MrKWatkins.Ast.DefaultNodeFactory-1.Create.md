# DefaultNodeFactory&lt;TNode&gt;.Create Method
## Definition

Creates a node of the specified type. The node must inherit from `TNode`.

```c#
public TNode Create(Type nodeType);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nodeType | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type of node to create. |

## Returns

TNode

The new node.
