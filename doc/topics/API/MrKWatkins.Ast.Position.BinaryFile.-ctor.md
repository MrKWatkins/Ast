# BinaryFile Constructors
## Overloads

| Name | Description |
| ---- | ----------- |
| [BinaryFile(FileInfo)](MrKWatkins.Ast.Position.BinaryFile.-ctor.md#mrkwatkins-ast-position-binaryfile-ctor(system-io-fileinfo)) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a file on disk. |
| [BinaryFile(String, Stream)](MrKWatkins.Ast.Position.BinaryFile.-ctor.md#mrkwatkins-ast-position-binaryfile-ctor(system-string-system-io-stream)) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file. |
| [BinaryFile(String, IReadOnlyList&lt;Byte&gt;)](MrKWatkins.Ast.Position.BinaryFile.-ctor.md#mrkwatkins-ast-position-binaryfile-ctor(system-string-system-collections-generic-ireadonlylist((system-byte)))) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a list of bytes containing the file. |

## BinaryFile(FileInfo) {id="mrkwatkins-ast-position-binaryfile-ctor(system-io-fileinfo)"}

Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a file on disk.

```c#
public BinaryFile(FileInfo file);
```

## Parameters {id="parameters-mrkwatkins-ast-position-binaryfile-ctor(system-io-fileinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| file | [FileInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.FileInfo) | A [FileInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.FileInfo) with details a file on disk to load. The [FullName](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.FileSystemInfo.FullName) will be used for [Name](MrKWatkins.Ast.Position.SourceFile.Name.md). |

## BinaryFile(String, Stream) {id="mrkwatkins-ast-position-binaryfile-ctor(system-string-system-io-stream)"}

Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file.

```c#
public BinaryFile(string name, Stream file);
```

## Parameters {id="parameters-mrkwatkins-ast-position-binaryfile-ctor(system-string-system-io-stream)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the file. |
| file | [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) | A [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file. The stream will be read to the end and left open. |

## BinaryFile(String, IReadOnlyList&lt;Byte&gt;) {id="mrkwatkins-ast-position-binaryfile-ctor(system-string-system-collections-generic-ireadonlylist((system-byte)))"}

Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a list of bytes containing the file.

```c#
public BinaryFile(string name, IReadOnlyList<Byte> bytes);
```

## Parameters {id="parameters-mrkwatkins-ast-position-binaryfile-ctor(system-string-system-collections-generic-ireadonlylist((system-byte)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the file. |
| bytes | [IReadOnlyList&lt;Byte&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IReadOnlyList-1) | The contents of the file. |

