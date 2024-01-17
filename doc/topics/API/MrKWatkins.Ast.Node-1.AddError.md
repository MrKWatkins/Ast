# Node&lt;TNode&gt;.AddError Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [AddError(String)](MrKWatkins.Ast.Node-1.AddError.md#mrkwatkins-ast-node-1-adderror(system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and the specified text to this node. |
| [AddError(String, String)](MrKWatkins.Ast.Node-1.AddError.md#mrkwatkins-ast-node-1-adderror(system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and the specified text to this node. |

## AddError(string) {id="mrkwatkins-ast-node-1-adderror(system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and the specified text to this node.

```c#
public void AddError(string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-adderror(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

## AddError(string, string) {id="mrkwatkins-ast-node-1-adderror(system-string-system-string)"}

Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md) and the specified text to this node.

```c#
public void AddError(string code, string text);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-adderror(system-string-system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Code](MrKWatkins.Ast.Message.Code.md) for the message. |
| text | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The [Text](MrKWatkins.Ast.Message.Text.md) of the message. |

