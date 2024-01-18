# SourcePosition Class
## Definition

A position in source code.

```c#
public abstract class SourcePosition : IEquatable<SourcePosition>, IEqualityOperators<SourcePosition, SourcePosition, Boolean>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [SourcePosition()](MrKWatkins.Ast.Position.SourcePosition.-ctor.md) |  |

## Fields

| Name | Description |
| ---- | ----------- |
| [None](MrKWatkins.Ast.Position.SourcePosition.None.md) | Represents no source position, e.g. the parent node was generated programmatically. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Combine](MrKWatkins.Ast.Position.SourcePosition.Combine.md) | Combines two [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md)s to give a new SourcePosition that includes both this position and the `other` along any source in-between the two. |
| [CreateZeroWidthPrefix](MrKWatkins.Ast.Position.SourcePosition.CreateZeroWidthPrefix.md) | Create a new [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md) with zero width at the start of this [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md). |
| [Equals](MrKWatkins.Ast.Position.SourcePosition.Equals.md#mrkwatkins-ast-position-sourceposition-equals(mrkwatkins-ast-position-sourceposition)) |  |
| [Equals](MrKWatkins.Ast.Position.SourcePosition.Equals.md#mrkwatkins-ast-position-sourceposition-equals(system-object)) |  |
| [GetHashCode](MrKWatkins.Ast.Position.SourcePosition.GetHashCode.md) |  |

## Operators

| Name | Description |
| ---- | ----------- |
| [Addition](MrKWatkins.Ast.Position.SourcePosition.op_Addition.md) | Combines two [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md)s to give a new SourcePosition that includes both `x` and `y` along with any source in-between the two. |
| [Equality](MrKWatkins.Ast.Position.SourcePosition.op_Equality.md) | Determines whether two specified [SourceFile](MrKWatkins.Ast.Position.SourceFile.md)s have the same value. |
| [Inequality](MrKWatkins.Ast.Position.SourcePosition.op_Inequality.md) | Determines whether two specified [SourceFile](MrKWatkins.Ast.Position.SourceFile.md)s have different values. |

