# BinaryFile Class
## Definition

A binary [SourceFile](MrKWatkins.Ast.Position.SourceFile.md).

```c#
public sealed class BinaryFile : SourceFile, IEquatable<SourceFile>, IEqualityOperators<SourceFile, SourceFile, Boolean>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [BinaryFile(FileInfo)](MrKWatkins.Ast.Position.BinaryFile.-ctor.md#mrkwatkins-ast-position-binaryfile-ctor(system-io-fileinfo)) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a file on disk. |
| [BinaryFile(string, Stream)](MrKWatkins.Ast.Position.BinaryFile.-ctor.md#mrkwatkins-ast-position-binaryfile-ctor(system-string-system-io-stream)) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file. |
| [BinaryFile(string, IReadOnlyList&lt;Byte&gt;)](MrKWatkins.Ast.Position.BinaryFile.-ctor.md#mrkwatkins-ast-position-binaryfile-ctor(system-string-system-collections-generic-ireadonlylist((system-byte)))) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a list of bytes containing the file. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Bytes](MrKWatkins.Ast.Position.BinaryFile.Bytes.md) | The raw bytes of the file. |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateEntireFilePosition()](MrKWatkins.Ast.Position.BinaryFile.CreateEntireFilePosition.md) | Creates a [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md) from this [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) that represents the whole file. |
| [CreatePosition(int, int)](MrKWatkins.Ast.Position.BinaryFile.CreatePosition.md) | Creates a [BinaryFilePosition](MrKWatkins.Ast.Position.BinaryFilePosition.md) from this [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md). |

