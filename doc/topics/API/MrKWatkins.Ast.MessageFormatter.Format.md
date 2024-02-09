# MessageFormatter.Format Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageLevel, Boolean)](MrKWatkins.Ast.MessageFormatter.Format.md#mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-system-boolean)) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) of the specified [MessageLevel](MrKWatkins.Ast.MessageLevel.md) in the specified node. |
| [Format&lt;TNode&gt;(Node&lt;TNode&gt;, Boolean)](MrKWatkins.Ast.MessageFormatter.Format.md#mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-system-boolean)) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) in the specified node, grouping by [Level](MrKWatkins.Ast.Message.Level.md) in descending order. I.e. [Error](MrKWatkins.Ast.MessageLevel.md#fields) then [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and then [Info](MrKWatkins.Ast.MessageLevel.md#fields). |

## Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageLevel, Boolean) {id="mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-system-boolean)"}

Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) of the specified [MessageLevel](MrKWatkins.Ast.MessageLevel.md) in the specified node.

```c#
public static IEnumerable<string> Format<TNode>(Node<TNode> node, MessageLevel level, bool includeSource = true)
   where TNode : Node<TNode>;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-system-boolean)"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node. |

## Parameters {id="parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-system-boolean)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) | The node. |
| level | [MessageLevel](MrKWatkins.Ast.MessageLevel.md) | The [MessageLevel](MrKWatkins.Ast.MessageLevel.md). |
| includeSource | [Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean) | Whether to include the source code in the output or not. Defaults to `true`. |

## Returns {id="returns-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-system-boolean)"}

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of formatted [Messages](MrKWatkins.Ast.Message.md).
## Format&lt;TNode&gt;(Node&lt;TNode&gt;, Boolean) {id="mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-system-boolean)"}

Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) in the specified node, grouping by [Level](MrKWatkins.Ast.Message.Level.md) in descending order. I.e. [Error](MrKWatkins.Ast.MessageLevel.md#fields) then [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and then [Info](MrKWatkins.Ast.MessageLevel.md#fields).

```c#
public static IEnumerable<IGrouping<MessageLevel, string>> Format<TNode>(Node<TNode> node, bool includeSource = true)
   where TNode : Node<TNode>;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-system-boolean)"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node. |

## Parameters {id="parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-system-boolean)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) | The node. |
| includeSource | [Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean) | Whether to include the source code in the output or not. Defaults to `true`. |

## Returns {id="returns-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-system-boolean)"}

[IEnumerable&lt;IGrouping&lt;MessageLevel, String&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of formatted [Messages](MrKWatkins.Ast.Message.md) grouped by [Level](MrKWatkins.Ast.Message.Level.md).
