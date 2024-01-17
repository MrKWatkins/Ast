# BinaryFile.CreatePosition Method
## Definition

Creates a [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md) from this [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md).

```c#
public BinaryFilePosition CreatePosition(int startIndex, int length);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| startIndex | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The start index of the position in the file. |
| length | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The length of the file. |

## Returns

[BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md)

A new [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md) instance.
