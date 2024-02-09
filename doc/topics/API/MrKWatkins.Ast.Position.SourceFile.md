# SourceFile Class
## Definition

A file of source code.

```c#
public abstract class SourceFile : IEquatable<SourceFile>, IEqualityOperators<SourceFile, SourceFile, Boolean>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [SourceFile(String, Int32)](MrKWatkins.Ast.Position.SourceFile.-ctor.md) | Initialises a new instance of the [SourceFile](MrKWatkins.Ast.Position.SourceFile.md) class. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Length](MrKWatkins.Ast.Position.SourceFile.Length.md) | The length of the source file. |
| [Name](MrKWatkins.Ast.Position.SourceFile.Name.md) | The name of the source file. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Equals(SourceFile)](MrKWatkins.Ast.Position.SourceFile.Equals.md#mrkwatkins-ast-position-sourcefile-equals(mrkwatkins-ast-position-sourcefile)) |  |
| [Equals(Object)](MrKWatkins.Ast.Position.SourceFile.Equals.md#mrkwatkins-ast-position-sourcefile-equals(system-object)) |  |
| [GetHashCode()](MrKWatkins.Ast.Position.SourceFile.GetHashCode.md) |  |
| [ToString()](MrKWatkins.Ast.Position.SourceFile.ToString.md) |  |

## Operators

| Name | Description |
| ---- | ----------- |
| [op_Equality(SourceFile, SourceFile)](MrKWatkins.Ast.Position.SourceFile.op_Equality.md) | Determines whether two specified [SourceFile](MrKWatkins.Ast.Position.SourceFile.md)s have the same value. |
| [op_Inequality(SourceFile, SourceFile)](MrKWatkins.Ast.Position.SourceFile.op_Inequality.md) | Determines whether two specified [SourceFile](MrKWatkins.Ast.Position.SourceFile.md)s have different values. |

