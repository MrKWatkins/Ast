# MessageFormatter Class
## Definition

Utility methods to format [Messages](MrKWatkins.Ast.Message.md) as strings. Formatting optionally includes the original source code.

```c#
public abstract sealed class MessageFormatter
```

## Methods

| Name | Description |
| ---- | ----------- |
| [Format(Node&lt;TNode&gt;, MessageLevel, Boolean)](MrKWatkins.Ast.MessageFormatter.Format.md) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) of the specified [MessageLevel](MrKWatkins.Ast.MessageLevel.md) in the specified node. |
| [Format(Node&lt;TNode&gt;, Boolean)](MrKWatkins.Ast.MessageFormatter.Format.md) | Lazily enumerates over all [Messages](MrKWatkins.Ast.Message.md) in the specified node, grouping by [Level](MrKWatkins.Ast.Message.Level.md) in descending order. I.e. [Error](MrKWatkins.Ast.MessageLevel.Error.md) then [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and then [Info](MrKWatkins.Ast.MessageLevel.Info.md). |
| [FormatErrors(Node&lt;TNode&gt;, Boolean)](MrKWatkins.Ast.MessageFormatter.FormatErrors.md) | Lazily enumerates over all [Errors](MrKWatkins.Ast.MessageLevel.Error.md) in the specified node. |

