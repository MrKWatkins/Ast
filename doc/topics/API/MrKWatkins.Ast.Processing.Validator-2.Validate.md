# Validator&lt;TContext, TBaseNode&gt;.Validate Method
## Definition

Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues.

```c#
protected abstract IEnumerable<Message> Validate(TContext? context, TBaseNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TBaseNode | The node to validate. |

## Returns

[IEnumerable&lt;Message&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

[Messages](MrKWatkins.Ast.Message.md) to attach to the node.
