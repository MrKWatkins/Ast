# Node&lt;TNode&gt;.AddMessage Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddMessage(Message)](MrKWatkins.Ast.Node-1.AddMessage.md#mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-message)) | Adds a [Message](MrKWatkins.Ast.Message.md) to this node. |
| [AddMessage(MessageLevel, String)](MrKWatkins.Ast.Node-1.AddMessage.md#mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with the specified [Level](MrKWatkins.Ast.Message.Level.md) and [Text](MrKWatkins.Ast.Message.Text.md) to this node. |
| [AddMessage(MessageLevel, String, String)](MrKWatkins.Ast.Node-1.AddMessage.md#mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with the specified [Level](MrKWatkins.Ast.Message.Level.md), [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md) to this node. |

## AddMessage(Message) {id="mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-message)"}

Adds a [Message](MrKWatkins.Ast.Message.md) to this node.

```c#
public void AddMessage(Message message);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-message)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [Message](MrKWatkins.Ast.Message.md) | The [Message](MrKWatkins.Ast.Message.md) to add. |

## AddMessage(MessageLevel, String) {id="mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with the specified [Level](MrKWatkins.Ast.Message.Level.md) and [Text](MrKWatkins.Ast.Message.Text.md) to this node.

```c#
public void AddMessage(MessageLevel level, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| level | [MessageLevel](MrKWatkins.Ast.MessageLevel.md) | The [Level](MrKWatkins.Ast.Message.Level.md) of the [Message](MrKWatkins.Ast.Message.md). |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## AddMessage(MessageLevel, String, String) {id="mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string-system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with the specified [Level](MrKWatkins.Ast.Message.Level.md), [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md) to this node.

```c#
public void AddMessage(MessageLevel level, string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| level | [MessageLevel](MrKWatkins.Ast.MessageLevel.md) | The [Level](MrKWatkins.Ast.Message.Level.md) of the [Message](MrKWatkins.Ast.Message.md). |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) for the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

