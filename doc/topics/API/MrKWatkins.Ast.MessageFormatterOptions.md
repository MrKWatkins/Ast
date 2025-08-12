# MessageFormatterOptions Class
## Definition

Options for the [MessageFormatter](MrKWatkins.Ast.MessageFormatter.md).

```c#
public sealed class MessageFormatterOptions
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [MessageFormatterOptions()](MrKWatkins.Ast.MessageFormatterOptions.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [Default](MrKWatkins.Ast.MessageFormatterOptions.Default.md) | [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) with [PrefixWithSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.PrefixWithSourcePosition.md) = `false` and [HighlightSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.HighlightSourcePosition.md) = `false`. |
| [HighlightOnly](MrKWatkins.Ast.MessageFormatterOptions.HighlightOnly.md) | [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) with [PrefixWithSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.PrefixWithSourcePosition.md) = `false` and [HighlightSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.HighlightSourcePosition.md) = `true`. |
| [HighlightSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.HighlightSourcePosition.md) | Whether to suffix the message with a highlight of the message in its line. |
| [PrefixAndHighlight](MrKWatkins.Ast.MessageFormatterOptions.PrefixAndHighlight.md) | [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) with [PrefixWithSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.PrefixWithSourcePosition.md) = `true` and [HighlightSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.HighlightSourcePosition.md) = `true`. |
| [PrefixOnly](MrKWatkins.Ast.MessageFormatterOptions.PrefixOnly.md) | [MessageFormatterOptions](MrKWatkins.Ast.MessageFormatterOptions.md) with [PrefixWithSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.PrefixWithSourcePosition.md) = `true` and [HighlightSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.HighlightSourcePosition.md) = `false`. |
| [PrefixWithSourcePosition](MrKWatkins.Ast.MessageFormatterOptions.PrefixWithSourcePosition.md) | Whether to prefix the message with the source position or not. |

