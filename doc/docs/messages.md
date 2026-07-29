# Messages

Compilers rarely stop at the first problem. Rather than throwing, errors, warnings and informational messages are attached to the node they apply to and collected up later, so a single pass can report everything it found and processing can carry on for as long as it is useful to do so.

## Adding Messages

Every node has [`AddError`](API/MrKWatkins.Ast/Node-TNode/AddError.md), [`AddWarning`](API/MrKWatkins.Ast/Node-TNode/AddWarning.md) and [`AddInfo`](API/MrKWatkins.Ast/Node-TNode/AddInfo.md) methods, each with an overload that takes a code as well as the text:

```c#
sixty.AddError("Value must be less than 55.");
sixty.AddError("E017", "Value must be less than 55.");
```

The three levels come from [`MessageLevel`](API/MrKWatkins.Ast/MessageLevel/index.md). [`AddMessage`](API/MrKWatkins.Ast/Node-TNode/AddMessage.md) takes a level, or a prebuilt [`Message`](API/MrKWatkins.Ast/Message/index.md), which is useful when a validator builds its messages up front. [`Message`](API/MrKWatkins.Ast/Message/index.md) is an immutable record with static [`Error`](API/MrKWatkins.Ast/Message/Error.md), [`Warning`](API/MrKWatkins.Ast/Message/Warning.md) and [`Info`](API/MrKWatkins.Ast/Message/Info.md) factory methods.

Adding a message is thread safe, so processors in a [parallel pipeline stage](processing.md#pipelines) can raise messages against the nodes they are visiting without any locking of your own.

## Finding Messages

Messages can be read back from a single node or from a whole subtree. Each level has the same set of members, with [`Messages`](API/MrKWatkins.Ast/Node-TNode/Messages.md) covering all levels at once:

| Member | Description |
| ------ | ----------- |
| [`Errors`](API/MrKWatkins.Ast/Node-TNode/Errors.md) | The error messages on this node. |
| [`HasErrors`](API/MrKWatkins.Ast/Node-TNode/HasErrors.md) | Whether this node has any errors. |
| [`ThisAndDescendentsHaveErrors`](API/MrKWatkins.Ast/Node-TNode/ThisAndDescendentsHaveErrors.md) | Whether this node or anything beneath it has errors. |
| [`ThisAndDescendentsWithErrors`](API/MrKWatkins.Ast/Node-TNode/ThisAndDescendentsWithErrors.md) | The nodes at or beneath this one that have errors. |

`Warnings`, `Infos` and `Messages` have the equivalent members.

```c#
sixty.AddError("Value must be less than 55.");

var expressionHasErrors = expression.ThisAndDescendentsHaveErrors;  // true.
var badNodes = expression.ThisAndDescendentsWithErrors;
```

Note the difference between [`HasErrors`](API/MrKWatkins.Ast/Node-TNode/HasErrors.md), which looks only at the node itself, and [`ThisAndDescendentsHaveErrors`](API/MrKWatkins.Ast/Node-TNode/ThisAndDescendentsHaveErrors.md), which walks the subtree. The latter is what a [pipeline](processing.md#pipelines) uses by default to decide whether to continue to the next stage.

## Formatting Messages

[`MessageFormatter`](API/MrKWatkins.Ast/MessageFormatter/index.md) turns the messages in a tree into strings ready for output. [`FormatErrors`](API/MrKWatkins.Ast/MessageFormatter/FormatErrors.md) returns just the errors; [`Format`](API/MrKWatkins.Ast/MessageFormatter/Format.md) takes a [`MessageLevel`](API/MrKWatkins.Ast/MessageLevel/index.md) for a single level, or no level at all to return every message grouped by level, errors first.

```c#
foreach (var error in MessageFormatter.FormatErrors(expression))
{
    Console.Error.WriteLine(error);
}
```

By default only the level, code and text are written:

```
Error: Value must be less than 55.
```

Pass [`MessageFormatterOptions`](API/MrKWatkins.Ast/MessageFormatterOptions/index.md) to include the [source position](source-positions.md) of the node the message came from. Four combinations are available as static instances — [`Default`](API/MrKWatkins.Ast/MessageFormatterOptions/Default.md), [`PrefixOnly`](API/MrKWatkins.Ast/MessageFormatterOptions/PrefixOnly.md), [`HighlightOnly`](API/MrKWatkins.Ast/MessageFormatterOptions/HighlightOnly.md) and [`PrefixAndHighlight`](API/MrKWatkins.Ast/MessageFormatterOptions/PrefixAndHighlight.md):

```c#
var errors = MessageFormatter.FormatErrors(expression, MessageFormatterOptions.PrefixAndHighlight);
```

```
MySource.code (1, 6): Error: Value must be less than 55.
50 + 60
     --
```

The prefix is the position's `ToString`, which for a text file is the file name plus one based line and column numbers, matching the format the C# compiler uses. The highlight repeats the source line with the position underlined, and preserves any tabs in the leading part of the line so that the underline stays aligned.

Nodes with no source position — those built programmatically rather than parsed — format as just the message. Highlighting is only available for positions in text files; a [`BinaryFilePosition`](API/MrKWatkins.Ast.Position/BinaryFilePosition/index.md) will still be used as a prefix but has no source line to show.
