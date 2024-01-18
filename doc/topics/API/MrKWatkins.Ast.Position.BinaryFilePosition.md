# BinaryFilePosition Class
## Definition

A [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md) in a binary source code file.

```c#
public sealed class BinaryFilePosition : SourceFilePosition<BinaryFilePosition, BinaryFile>, IEquatable<SourcePosition>, IEqualityOperators<SourcePosition, SourcePosition, Boolean>
```

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateCombination](MrKWatkins.Ast.Position.BinaryFilePosition.CreateCombination.md) |  |
| [CreateZeroWidthPrefix](MrKWatkins.Ast.Position.BinaryFilePosition.CreateZeroWidthPrefix.md) | Create a new [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md) with zero width at the start of this [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md). |

## Operators

| Name | Description |
| ---- | ----------- |
| [Addition](MrKWatkins.Ast.Position.BinaryFilePosition.op_Addition.md) | Combines two [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md)s to give a new SourcePosition that includes both `x` and `y` along with any source in-between the two. |

