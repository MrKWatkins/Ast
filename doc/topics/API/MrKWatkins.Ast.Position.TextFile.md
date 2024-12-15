# TextFile Class
## Definition

A text [SourceFile](MrKWatkins.Ast.Position.SourceFile.md).

```c#
public sealed class TextFile : SourceFile, IEquatable<SourceFile>, IEqualityOperators<SourceFile, SourceFile, Boolean>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [TextFile(FileInfo)](MrKWatkins.Ast.Position.TextFile.-ctor.md#mrkwatkins-ast-position-textfile-ctor(system-io-fileinfo)) | Initialises a new instance of the [TextFile](MrKWatkins.Ast.Position.TextFile.md) class from a file on disk. |
| [TextFile(String, Stream)](MrKWatkins.Ast.Position.TextFile.-ctor.md#mrkwatkins-ast-position-textfile-ctor(system-string-system-io-stream)) | Initialises a new instance of the [TextFile](MrKWatkins.Ast.Position.TextFile.md) class from a [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file. |
| [TextFile(String, String)](MrKWatkins.Ast.Position.TextFile.-ctor.md#mrkwatkins-ast-position-textfile-ctor(system-string-system-string)) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a string containing the file. |

## Properties

| Name | Description |
| ---- | ----------- |
| [IsEmpty](MrKWatkins.Ast.Position.TextFile.IsEmpty.md) | Returns `true` if the file is empty. |
| [Lines](MrKWatkins.Ast.Position.TextFile.Lines.md) | The individual lines in the file. |
| [Text](MrKWatkins.Ast.Position.TextFile.Text.md) | The text of the file. |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateEntireFilePosition()](MrKWatkins.Ast.Position.TextFile.CreateEntireFilePosition.md) | Creates a [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md) from this [TextFile](MrKWatkins.Ast.Position.TextFile.md) that represents the whole file. |
| [CreatePosition(Int32, Int32, Int32, Int32)](MrKWatkins.Ast.Position.TextFile.CreatePosition.md) | Creates a [TextFilePosition](MrKWatkins.Ast.Position.TextFilePosition.md) from this [TextFile](MrKWatkins.Ast.Position.TextFile.md). |

