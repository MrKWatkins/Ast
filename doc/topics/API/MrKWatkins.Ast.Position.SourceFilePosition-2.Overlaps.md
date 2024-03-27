# SourceFilePosition&lt;TSelf, TFile&gt;.Overlaps Method
## Definition

Returns `true` if two positions for the same file overlap. Zero length positions will overlap only if they are inside the other position. If they are at the start or end index of the other position they will not overlap. Two zero length positions never overlap.

```c#
public bool Overlaps(SourceFilePosition<TSelf, TFile> other);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [SourceFilePosition&lt;TSelf, TFile&gt;](MrKWatkins.Ast.Position.SourceFilePosition-2.md) |  |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the two positions overlap, `false` otherwise.
