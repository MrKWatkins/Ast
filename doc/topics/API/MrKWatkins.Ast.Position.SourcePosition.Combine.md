# SourcePosition.Combine Method
## Definition

Combines two [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md)s to give a new SourcePosition that includes both this position and the `other` along any source in-between the two.

```c#
public abstract SourcePosition Combine(SourcePosition other);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md) |  |

## Returns

[SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md)
