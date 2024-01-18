# SourcePosition&lt;TSelf&gt; Class
## Definition

Self generic extension of [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md).

```c#
public abstract class SourcePosition<TSelf> : SourcePosition, IEquatable<SourcePosition>, IEqualityOperators<SourcePosition, SourcePosition, Boolean>
   where TSelf : SourcePosition<TSelf>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TSelf |  |

## Constructors

| Name | Description |
| ---- | ----------- |
| [SourcePosition()](MrKWatkins.Ast.Position.SourcePosition-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Combine(SourcePosition)](MrKWatkins.Ast.Position.SourcePosition-1.Combine.md#mrkwatkins-ast-position-sourceposition-1-combine(mrkwatkins-ast-position-sourceposition)) |  |
| [Combine(TSelf)](MrKWatkins.Ast.Position.SourcePosition-1.Combine.md#mrkwatkins-ast-position-sourceposition-1-combine(-0)) | Combines two [SourcePosition&lt;TSelf&gt;](MrKWatkins.Ast.Position.SourcePosition-1.md)s to give a new SourcePosition that includes both this position and the `other` along any source in-between the two. |

