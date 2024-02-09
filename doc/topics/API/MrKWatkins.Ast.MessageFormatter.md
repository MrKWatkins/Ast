# MessageFormatter Class
## Definition

Utility methods to format [Messages](MrKWatkins.Ast.Message.md) as strings. Formatting optionally includes the original source code.

```c#
public abstract sealed class MessageFormatter
```

## Methods

| Name | Description |
| ---- | ----------- |
| [Format&lt;TNode&gt;(Node&lt;TNode&gt;, MessageLevel, Boolean)](MrKWatkins.Ast.MessageFormatter.Format.md#mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-mrkwatkins-ast-messagelevel-system-boolean)) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) of the specified [MessageLevel](MrKWatkins.Ast.MessageLevel.md) in the specified node. |
| [Format&lt;TNode&gt;(Node&lt;TNode&gt;, Boolean)](MrKWatkins.Ast.MessageFormatter.Format.md#mrkwatkins-ast-messageformatter-format-1(mrkwatkins-ast-node((-0))-system-boolean)) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) in the specified node, grouping by [Level](MrKWatkins.Ast.Message.Level.md) in descending order. I.e. [Error](MrKWatkins.Ast.MessageLevel.md#fields) then [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and then [Info](MrKWatkins.Ast.MessageLevel.md#fields). |
| [FormatErrors&lt;TNode&gt;(Node&lt;TNode&gt;, Boolean)](MrKWatkins.Ast.MessageFormatter.FormatErrors.md) | Lazily enumerates over all [Errors](MrKWatkins.Ast.MessageLevel.md#fields) in the specified node. |

