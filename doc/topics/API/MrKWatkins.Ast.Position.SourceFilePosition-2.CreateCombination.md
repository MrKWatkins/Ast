# SourceFilePosition&lt;TSelf, TFile&gt;.CreateCombination Method
## Definition

Creates a combination of this [SourceFilePosition&lt;TSelf, TFile&gt;](MrKWatkins.Ast.Position.SourceFilePosition-2.md) and another. Used to create a combination for [Combine(TSelf)](MrKWatkins.Ast.Position.SourceFilePosition-2.Combine.md).

```c#
protected abstract TSelf CreateCombination(TSelf other);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | TSelf | The other [SourceFilePosition&lt;TSelf, TFile&gt;](MrKWatkins.Ast.Position.SourceFilePosition-2.md) to combine with. |

## Returns

TSelf

The combination.
