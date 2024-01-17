# Message Record
## Definition

An error, warning or informational message for a node.

```c#
public sealed record Message : IEquatable<Message>
```

## Constructors

| Name | Description |
| ---- | ----------- |
| [Message(MessageLevel, String)](MrKWatkins.Ast.Message.-ctor.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the specified [Level](MrKWatkins.Ast.Message.Level.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Message(MessageLevel, String, String)](MrKWatkins.Ast.Message.-ctor.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the specified [Level](MrKWatkins.Ast.Message.Level.md), [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |

## Properties

| Name | Description |
| ---- | ----------- |
| [Code](MrKWatkins.Ast.Message.Code.md) | Optional code for the message. |
| [Level](MrKWatkins.Ast.Message.Level.md) | The [level](MrKWatkins.Ast.MessageLevel.md) of the message. |
| [Text](MrKWatkins.Ast.Message.Text.md) | The text of the message. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Error(String, String)](MrKWatkins.Ast.Message.Error.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Error(String)](MrKWatkins.Ast.Message.Error.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and specified [Text](MrKWatkins.Ast.Message.Text.md). |
| [Info(String, String)](MrKWatkins.Ast.Message.Info.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.Info.md) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Info(String)](MrKWatkins.Ast.Message.Info.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.Info.md) and specified [Text](MrKWatkins.Ast.Message.Text.md). |
| [ToString()](MrKWatkins.Ast.Message.ToString.md) | Returns a string representation of this message. |
| [Warning(String, String)](MrKWatkins.Ast.Message.Warning.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and specified [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md). |
| [Warning(String)](MrKWatkins.Ast.Message.Warning.md) | Initializes a new instance of the [Message](MrKWatkins.Ast.Message.md) class with the [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and specified [Text](MrKWatkins.Ast.Message.Text.md). |

