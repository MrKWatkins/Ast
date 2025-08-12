# MessageFormatter.Format Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageLevel, MessageFormatterOptions)](MrKWatkins.Ast.MessageFormatter.Format.md#mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-mrkwatkins-ast-messageformatteroptions)) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) of the specified [MessageLevel](MrKWatkins.Ast.MessageLevel.md) in the specified node. |
| [Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageFormatterOptions)](MrKWatkins.Ast.MessageFormatter.Format.md#mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messageformatteroptions)) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) in the specified node, grouping by [Level](MrKWatkins.Ast.Message.Level.md) in descending order. I.e. [Error](MrKWatkins.Ast.MessageLevel.md#fields) then [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and then [Info](MrKWatkins.Ast.MessageLevel.md#fields). |

## Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageLevel, MessageFormatterOptions) {id="mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-mrkwatkins-ast-messageformatteroptions)"}

Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) of the specified [MessageLevel](MrKWatkins.Ast.MessageLevel.md) in the specified node.

```c#
public static IEnumerable<string> Format<TNode>(Node<TNode> node, MessageLevel level, MessageFormatterOptions? options = null)
   where TNode : Node<TNode>;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-mrkwatkins-ast-messageformatteroptions)"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node. |

## Parameters {id="parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-mrkwatkins-ast-messageformatteroptions)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) | The node. |
| level | [MessageLevel](MrKWatkins.Ast.MessageLevel.md) | The [MessageLevel](MrKWatkins.Ast.MessageLevel.md). |
| options | [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) | The [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) to use. If not specified then [Default](MrKWatkins.Ast.MessageFormatterOptions.Default.md) is used. |

## Returns {id="returns-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-mrkwatkins-ast-messageformatteroptions)"}

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of formatted [Messages](MrKWatkins.Ast.Message.md).
## Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageFormatterOptions) {id="mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messageformatteroptions)"}

Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) in the specified node, grouping by [Level](MrKWatkins.Ast.Message.Level.md) in descending order. I.e. [Error](MrKWatkins.Ast.MessageLevel.md#fields) then [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and then [Info](MrKWatkins.Ast.MessageLevel.md#fields).

```c#
public static IEnumerable<IGrouping<MessageLevel, string>> Format<TNode>(Node<TNode> node, MessageFormatterOptions? options = null)
   where TNode : Node<TNode>;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messageformatteroptions)"}

| Name | Description |
| ---- | ----------- |
| TNode | The type of the node. |

## Parameters {id="parameters-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messageformatteroptions)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) | The node. |
| options | [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) | The [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) to use. If not specified then [Default](MrKWatkins.Ast.MessageFormatterOptions.Default.md) is used. |

## Returns {id="returns-mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messageformatteroptions)"}

[IEnumerable&lt;IGrouping&lt;MessageLevel, String&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of formatted [Messages](MrKWatkins.Ast.Message.md) grouped by [Level](MrKWatkins.Ast.Message.Level.md).
