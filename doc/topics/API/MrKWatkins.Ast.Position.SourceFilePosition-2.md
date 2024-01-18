# SourceFilePosition&lt;TSelf, TFile&gt; Class
## Definition

A [SourcePosition](MrKWatkins.Ast.Position.SourcePosition.md) in a source code file.

```c#
public abstract class SourceFilePosition<TSelf, TFile> : SourcePosition<TSelf>, IEquatable<SourcePosition>, IEqualityOperators<SourcePosition, SourcePosition, Boolean>
   where TSelf : SourceFilePosition<TSelf, TFile>
   where TFile : SourceFile
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TSelf |  |
| TFile |  |

## Constructors

| Name | Description |
| ---- | ----------- |
| [SourceFilePosition(TFile, int, int)](MrKWatkins.Ast.Position.SourceFilePosition-2.-ctor.md) | Initialises a new instance of the [SourceFilePosition&lt;TSelf, TFile&gt;](MrKWatkins.Ast.Position.SourceFilePosition-2.md) class. |

## Properties

| Name | Description |
| ---- | ----------- |
| [EndIndex](MrKWatkins.Ast.Position.SourceFilePosition-2.EndIndex.md) | The exclusive end index of the position in the source file. |
| [File](MrKWatkins.Ast.Position.SourceFilePosition-2.File.md) | The [SourceFile](MrKWatkins.Ast.Position.SourceFile.md). |
| [Length](MrKWatkins.Ast.Position.SourceFilePosition-2.Length.md) | The length of the position in the source file. |
| [StartIndex](MrKWatkins.Ast.Position.SourceFilePosition-2.StartIndex.md) | The inclusive start index of the position in the source file. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Combine](MrKWatkins.Ast.Position.SourceFilePosition-2.Combine.md) |  |
| [CreateCombination](MrKWatkins.Ast.Position.SourceFilePosition-2.CreateCombination.md) | Creates a combination of this [SourceFilePosition&lt;TSelf, TFile&gt;](MrKWatkins.Ast.Position.SourceFilePosition-2.md) and another. Used to create a combination for [Combine(TSelf)](MrKWatkins.Ast.Position.SourceFilePosition-2.Combine.md). |
| [Equals](MrKWatkins.Ast.Position.SourceFilePosition-2.Equals.md) |  |
| [GetHashCode](MrKWatkins.Ast.Position.SourceFilePosition-2.GetHashCode.md) |  |
| [Overlaps](MrKWatkins.Ast.Position.SourceFilePosition-2.Overlaps.md) | Returns `true` if two positions for the same file overlap. Zero length positions will overlap only if they are inside the other position. If they are at the start or end index of the other position they will not overlap. Two zero length positions never overlap. |
| [ToString](MrKWatkins.Ast.Position.SourceFilePosition-2.ToString.md) |  |

