# Message Record
## Definition

An error, warning or informational message for a node.

```c#
public sealed record Message : IEquatable<Message>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [Message(MessageLevel, string)](MrKWatkins.Ast.Message.-ctor.md#mrkwatkins-ast-message-ctor(mrkwatkins-ast-messagelevel-system-string)) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the specified [Level](MrKWatkins.Ast.Message.Level.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Message(MessageLevel, string, string)](MrKWatkins.Ast.Message.-ctor.md#mrkwatkins-ast-message-ctor(mrkwatkins-ast-messagelevel-system-string-system-string)) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the specified [Level](MrKWatkins.Ast.Message.Level.md), [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |

## Properties

| Name | Description |
| ---- | ----------- |
| [Code](MrKWatkins.Ast.Message.Code.md) | Optional code for the message. |
| [Level](MrKWatkins.Ast.Message.Level.md) | The [level](MrKWatkins.Ast.MessageLevel.md) of the message. |
| [Text](MrKWatkins.Ast.Message.Text.md) | The text of the message. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Error](MrKWatkins.Ast.Message.Error.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Error](MrKWatkins.Ast.Message.Error.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and specified [Text](MrKWatkins.Ast.Message.Text.md). |
| [Info](MrKWatkins.Ast.Message.Info.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.Info.md) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Info](MrKWatkins.Ast.Message.Info.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.Info.md) and specified [Text](MrKWatkins.Ast.Message.Text.md). |
| [ToString](MrKWatkins.Ast.Message.ToString.md) | Returns a string representation of this message. |
| [Warning](MrKWatkins.Ast.Message.Warning.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Warning](MrKWatkins.Ast.Message.Warning.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and specified [Text](MrKWatkins.Ast.Message.Text.md). |

