# MessageFormatter.FormatErrors Method
## Definition

Lazily enumerates over all [Errors](MrKWatkins.Ast.MessageLevel.md#fields) in the specified node.

```c#
public static IEnumerable<string> FormatErrors<TNode>(Node<TNode> node, bool includeSource = true)
   where TNode : Node<TNode>;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) | The node. |
| includeSource | [Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean) | Whether to include the source code in the output or not. Defaults to `true`. |

## Returns

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of formatted errors.
