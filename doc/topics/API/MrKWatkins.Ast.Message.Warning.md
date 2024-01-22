# Message.Warning Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Warning(string, string)](MrKWatkins.Ast.Message.Warning.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Warning(string)](MrKWatkins.Ast.Message.Warning.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Text](MrKWatkins.Ast.Message.Text.md). |

## Warning(string, string) {id="mrkwatkins-ast-message-warning(system-string-system-string)"}

Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md).

```c#
public static Message Warning(string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-message-warning(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) of the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## Returns {id="returns-mrkwatkins-ast-message-warning(system-string-system-string)"}

[Message](MrKWatkins.Ast.Message.md)
## Warning(string) {id="mrkwatkins-ast-message-warning(system-string)"}

Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Text](MrKWatkins.Ast.Message.Text.md).

```c#
public static Message Warning(string text);
```

## Parameters {id="parameters-mrkwatkins-ast-message-warning(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## Returns {id="returns-mrkwatkins-ast-message-warning(system-string)"}

[Message](MrKWatkins.Ast.Message.md)
