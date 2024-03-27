# SourceFilePosition&lt;TSelf, TFile&gt;.EndIndex Property
## Definition

The exclusive end index of the position in the source file.

```c#
public int EndIndex { get; }
```

## Property Value

[Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32)

The exclusive end index.
## Remarks

As the end index is exclusive it will be the index of the first character *after* the position. If the position is zero length then it will equal [StartIndex](MrKWatkins.Ast.Position.SourceFilePosition-2.StartIndex.md).
