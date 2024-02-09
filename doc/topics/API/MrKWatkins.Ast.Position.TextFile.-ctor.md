# TextFile Constructors
## Overloads

| Name | Description |
| ---- | ----------- |
| [TextFile(FileInfo)](MrKWatkins.Ast.Position.TextFile.-ctor.md#mrkwatkins-ast-position-textfile-ctor(system-io-fileinfo)) | Initialises a new instance of the [TextFile](MrKWatkins.Ast.Position.TextFile.md) class from a file on disk. |
| [TextFile(String, Stream)](MrKWatkins.Ast.Position.TextFile.-ctor.md#mrkwatkins-ast-position-textfile-ctor(system-string-system-io-stream)) | Initialises a new instance of the [TextFile](MrKWatkins.Ast.Position.TextFile.md) class from a [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file. |
| [TextFile(String, String)](MrKWatkins.Ast.Position.TextFile.-ctor.md#mrkwatkins-ast-position-textfile-ctor(system-string-system-string)) | Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a string containing the file. |

## TextFile(FileInfo) {id="mrkwatkins-ast-position-textfile-ctor(system-io-fileinfo)"}

Initialises a new instance of the [TextFile](MrKWatkins.Ast.Position.TextFile.md) class from a file on disk.

```c#
public TextFile(FileInfo file);
```

## Parameters {id="parameters-mrkwatkins-ast-position-textfile-ctor(system-io-fileinfo)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| file | [FileInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.FileInfo) | A [FileInfo](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.FileInfo) with details a file on disk to load. The [FullName](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.FileSystemInfo.FullName) will be used for [Name](MrKWatkins.Ast.Position.SourceFile.Name.md). |

## TextFile(String, Stream) {id="mrkwatkins-ast-position-textfile-ctor(system-string-system-io-stream)"}

Initialises a new instance of the [TextFile](MrKWatkins.Ast.Position.TextFile.md) class from a [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file.

```c#
public TextFile(string name, Stream text);
```

## Parameters {id="parameters-mrkwatkins-ast-position-textfile-ctor(system-string-system-io-stream)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the file. |
| text | [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) | A [Stream](https://learn.microsoft.com/en-gb/dotnet/api/System.IO.Stream) containing the file. The stream will be read to the end and left open. |

## TextFile(String, String) {id="mrkwatkins-ast-position-textfile-ctor(system-string-system-string)"}

Initialises a new instance of the [BinaryFile](MrKWatkins.Ast.Position.BinaryFile.md) class from a string containing the file.

```c#
public TextFile(string name, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-position-textfile-ctor(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the file. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The contents of the file. |

