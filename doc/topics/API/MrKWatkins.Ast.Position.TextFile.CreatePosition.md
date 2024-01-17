# TextFile.CreatePosition Method
## Definition

Creates a [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md) from this [TextFile](MrKWatkins.Ast.Position.TextFile.md).

```c#
public TextFilePosition CreatePosition(int startIndex, int length, int startLineIndex, int startColumnIndex);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| startIndex | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The start index of the position in the file. |
| length | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The length of the file. |
| startLineIndex | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | Zero based index of the start line of the position in the text file. |
| startColumnIndex | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | Zero based index of the start column of the position in the text file. |

## Returns

[TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md)

A new [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md) instance.
