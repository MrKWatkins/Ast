# Node&lt;TNode&gt;.AddWarning Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddWarning(String)](MrKWatkins.Ast.Node-1.AddWarning.md#mrkwatkins-ast-node-1-addwarning(system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and the specified text to this node. |
| [AddWarning(String, String)](MrKWatkins.Ast.Node-1.AddWarning.md#mrkwatkins-ast-node-1-addwarning(system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and the specified text to this node. |

## AddWarning(string) {id="mrkwatkins-ast-node-1-addwarning(system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and the specified text to this node.

```c#
public void AddWarning(string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addwarning(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## AddWarning(string, string) {id="mrkwatkins-ast-node-1-addwarning(system-string-system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.Warning.md) and the specified text to this node.

```c#
public void AddWarning(string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-addwarning(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) for the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

