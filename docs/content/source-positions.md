# Source Positions

Every node has a [`SourcePosition`](api/MrKWatkins.Ast/Node-TNode/SourcePosition.md) recording where in the source it came from. Set it during parsing and every [message](messages.md) raised against the node afterwards can point back at the original source, however many passes later it happens.

Nodes default to [`SourcePosition.None`](api/MrKWatkins.Ast.Position/SourcePosition/None.md), which is the right value for a node that was generated programmatically rather than parsed from anything.

## Source Files

A [`SourceFile`](api/MrKWatkins.Ast.Position/SourceFile/index.md) is a named piece of source with a length. Two implementations are provided:

- [`TextFile`](api/MrKWatkins.Ast.Position/TextFile/index.md) holds the source as a `string`, split into [`Lines`](api/MrKWatkins.Ast.Position/TextFile/Lines.md) for reporting.
- [`BinaryFile`](api/MrKWatkins.Ast.Position/BinaryFile/index.md) holds the source as bytes, for assemblers and tools that work over binary input.

Both can be constructed from a `FileInfo`, from a `Stream`, or from the content directly. Streams are read to the end and left open.

```c#
var source = new TextFile(new FileInfo("MySource.code"));  // Contains "50 + 60".
var image = new BinaryFile("rom.bin", romBytes);
```

Files are compared by type and name, not by content, so two `TextFile`s for the same path are equal.

## Positions

Ask the file for the positions within it. [`TextFile.CreatePosition`](api/MrKWatkins.Ast.Position/TextFile/CreatePosition.md) takes the start index and length of the position along with zero based line and column indices; [`BinaryFile.CreatePosition`](api/MrKWatkins.Ast.Position/BinaryFile/CreatePosition.md) just takes the start index and length. Both also have `CreateEntireFilePosition` for a position covering the whole file, which is handy for messages about the file as a whole.

```c#
expression.SourcePosition = source.CreatePosition(0, 7, 0, 0);  // startIndex, length, startLineIndex, startColumnIndex.
fifty.SourcePosition = source.CreatePosition(0, 2, 0, 0);
sixty.SourcePosition = source.CreatePosition(5, 2, 0, 5);
```

Indices are validated on construction, so a position can never point outside its file.

[`SourceFilePosition<TSelf, TFile>`](api/MrKWatkins.Ast.Position/SourceFilePosition-TSelf-TFile/index.md) is the base for both position types and provides [`File`](api/MrKWatkins.Ast.Position/SourceFilePosition-TSelf-TFile/File.md), [`StartIndex`](api/MrKWatkins.Ast.Position/SourceFilePosition-TSelf-TFile/StartIndex.md), [`Length`](api/MrKWatkins.Ast.Position/SourceFilePosition-TSelf-TFile/Length.md) and the exclusive [`EndIndex`](api/MrKWatkins.Ast.Position/SourceFilePosition-TSelf-TFile/EndIndex.md). [`TextFilePosition`](api/MrKWatkins.Ast.Position/TextFilePosition/index.md) adds line and column information, both zero based as `Index` and one based as `Number` for display, along with [`StartLine`](api/MrKWatkins.Ast.Position/TextFilePosition/StartLine.md) and the [`Text`](api/MrKWatkins.Ast.Position/TextFilePosition/Text.md) of the position itself.

## Combining Positions

A node built from several tokens usually wants a position covering all of them. [`Combine`](api/MrKWatkins.Ast.Position/SourcePosition/Combine.md), or the `+` operator, produces a position spanning both operands and everything between them:

```c#
var whole = left.SourcePosition + operatorPosition + right.SourcePosition;
```

Both positions must be from the same file, and must be the same kind of position; combining across files or mixing text and binary positions throws an `ArgumentException`.

[`CreateZeroWidthPrefix`](api/MrKWatkins.Ast.Position/SourcePosition/CreateZeroWidthPrefix.md) gives a zero length position at the start of an existing one. That is what you want to point at "just before this token" — a missing semicolon or an unclosed bracket belongs at the position where the expected thing should have been, not on top of whatever was found instead.

[`Overlaps`](api/MrKWatkins.Ast.Position/SourceFilePosition-TSelf-TFile/Overlaps.md) reports whether two positions in the same file intersect. Zero length positions overlap only when strictly inside another position, so a zero width position sitting at the start or end index of another does not count as overlapping, and two zero length positions never overlap.

## Text Positions in Messages

[`ITextSourcePosition`](api/MrKWatkins.Ast.Position/ITextSourcePosition/index.md) marks the positions that can render their own source for a message, which currently means [`TextFilePosition`](api/MrKWatkins.Ast.Position/TextFilePosition/index.md). The [`MessageFormatter`](api/MrKWatkins.Ast/MessageFormatter/index.md) tests for this interface when highlighting is enabled, so binary positions are still used as a message prefix but produce no highlighted line. See [Messages](messages.md#formatting-messages) for the formatting options.
