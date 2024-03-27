# Message.Error Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Error(String, String)](MrKWatkins.Ast.Message.Error.md#mrkwatkins-ast-message-error(system-string-system-string)) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Error(String)](MrKWatkins.Ast.Message.Error.md#mrkwatkins-ast-message-error(system-string)) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Text](MrKWatkins.Ast.Message.Text.md). |

## Error(String, String) {id="mrkwatkins-ast-message-error(system-string-system-string)"}

Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md).

```c#
public static Message Error(string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-message-error(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) of the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## Returns {id="returns-mrkwatkins-ast-message-error(system-string-system-string)"}

[Message](MrKWatkins.Ast.Message.md)

The error message.
## Error(String) {id="mrkwatkins-ast-message-error(system-string)"}

Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) and specified [Text](MrKWatkins.Ast.Message.Text.md).

```c#
public static Message Error(string text);
```

## Parameters {id="parameters-mrkwatkins-ast-message-error(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## Returns {id="returns-mrkwatkins-ast-message-error(system-string)"}

[Message](MrKWatkins.Ast.Message.md)

The error message.
