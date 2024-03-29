# Message.Info Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Info(String, String)](MrKWatkins.Ast.Message.Info.md#mrkwatkins-ast-message-info(system-string-system-string)) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Info(String)](MrKWatkins.Ast.Message.Info.md#mrkwatkins-ast-message-info(system-string)) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Text](MrKWatkins.Ast.Message.Text.md). |

## Info(String, String) {id="mrkwatkins-ast-message-info(system-string-system-string)"}

Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md).

```c#
public static Message Info(string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-message-info(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) of the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## Returns {id="returns-mrkwatkins-ast-message-info(system-string-system-string)"}

[Message](MrKWatkins.Ast.Message.md)

The info message.
## Info(String) {id="mrkwatkins-ast-message-info(system-string)"}

Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Text](MrKWatkins.Ast.Message.Text.md).

```c#
public static Message Info(string text);
```

## Parameters {id="parameters-mrkwatkins-ast-message-info(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## Returns {id="returns-mrkwatkins-ast-message-info(system-string)"}

[Message](MrKWatkins.Ast.Message.md)

The info message.
