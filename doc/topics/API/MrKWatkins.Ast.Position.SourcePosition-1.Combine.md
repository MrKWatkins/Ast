# SourcePosition&lt;TSelf&gt;.Combine Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Combine(SourcePosition)](MrKWatkins.Ast.Position.SourcePosition-1.Combine.md#mrkwatkins-ast-position-sourceposition-1-combine(mrkwatkins-ast-position-sourceposition)) |  |
| [Combine(TSelf)](MrKWatkins.Ast.Position.SourcePosition-1.Combine.md#mrkwatkins-ast-position-sourceposition-1-combine(-0)) | Combines two [SourcePosition&lt;TSelf&gt;](MrKWatkins.Ast.Position.SourcePosition-1.md)s to give a new SourcePosition that includes both this position and the `other` along any source in-between the two. |

## Combine(SourcePosition) {id="mrkwatkins-ast-position-sourceposition-1-combine(mrkwatkins-ast-position-sourceposition)"}

```c#
public new TSelf Combine(SourcePosition other);
```

## Parameters {id="parameters-mrkwatkins-ast-position-sourceposition-1-combine(mrkwatkins-ast-position-sourceposition)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md) |  |

## Returns {id="returns-mrkwatkins-ast-position-sourceposition-1-combine(mrkwatkins-ast-position-sourceposition)"}

TSelf
## Combine(TSelf) {id="mrkwatkins-ast-position-sourceposition-1-combine(-0)"}

Combines two [SourcePosition&lt;TSelf&gt;](MrKWatkins.Ast.Position.SourcePosition-1.md)s to give a new SourcePosition that includes both this position and the `other` along any source in-between the two.

```c#
protected new abstract TSelf Combine(TSelf other);
```

## Parameters {id="parameters-mrkwatkins-ast-position-sourceposition-1-combine(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | TSelf |  |

## Returns {id="returns-mrkwatkins-ast-position-sourceposition-1-combine(-0)"}

TSelf

The combined position.
