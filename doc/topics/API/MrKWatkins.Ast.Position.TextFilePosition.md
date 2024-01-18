# TextFilePosition Class
## Definition

A [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md) in a text source code file.

```c#
public sealed class TextFilePosition : SourceFilePosition<TextFilePosition, TextFile>, IEquatable<SourcePosition>, IEqualityOperators<SourcePosition, SourcePosition, Boolean>, ITextSourcePosition
```

## Properties

| Name | Description |
| ---- | ----------- |
| [StartColumnIndex](MrKWatkins.Ast.Position.TextFilePosition.StartColumnIndex.md) | Zero based index of the start column of the position in the text file. |
| [StartColumnNumber](MrKWatkins.Ast.Position.TextFilePosition.StartColumnNumber.md) | Number, i.e. 1 based index, of the start column of the position in the text file. |
| [StartLine](MrKWatkins.Ast.Position.TextFilePosition.StartLine.md) | The start line of the text source. |
| [StartLineIndex](MrKWatkins.Ast.Position.TextFilePosition.StartLineIndex.md) | Zero based index of the start line of the position in the text file. |
| [StartLineNumber](MrKWatkins.Ast.Position.TextFilePosition.StartLineNumber.md) | Number, i.e. 1 based index, of the start line of the position in the text file. |
| [Text](MrKWatkins.Ast.Position.TextFilePosition.Text.md) | The text source. |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateCombination(TextFilePosition)](MrKWatkins.Ast.Position.TextFilePosition.CreateCombination.md) |  |
| [CreateZeroWidthPrefix()](MrKWatkins.Ast.Position.TextFilePosition.CreateZeroWidthPrefix.md) | Create a new [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md) with zero width at the start of this [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md). |
| [ToString()](MrKWatkins.Ast.Position.TextFilePosition.ToString.md) |  |
| [WriteSourceForMessage(StringBuilder)](MrKWatkins.Ast.Position.TextFilePosition.WriteSourceForMessage.md) |  |

## Operators

| Name | Description |
| ---- | ----------- |
| [op_Addition(TextFilePosition, TextFilePosition)](MrKWatkins.Ast.Position.TextFilePosition.op_Addition.md) | Combines two [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md)s to give a new SourcePosition that includes both `x` and `y` along with any source in-between the two. |

