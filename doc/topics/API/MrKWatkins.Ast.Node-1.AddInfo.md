# Node&lt;TNode&gt;.AddInfo Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddInfo(String)](MrKWatkins.Ast.Node-1.AddInfo.md#mrkwatkins-ast-node-1-addinfo(system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AddInfo(String, String)](MrKWatkins.Ast.Node-1.AddInfo.md#mrkwatkins-ast-node-1-addinfo(system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |

## AddInfo(String) {id="mrkwatkins-ast-node-1-addinfo(system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node.

```c#
public void AddInfo(string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addinfo(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## AddInfo(String, String) {id="mrkwatkins-ast-node-1-addinfo(system-string-system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node.

```c#
public void AddInfo(string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addinfo(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) for the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

