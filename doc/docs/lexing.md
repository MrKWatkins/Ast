# Lexing

The `MrKWatkins.Ast.Lexing` namespace provides the plumbing that sits between a source file and a parser: a reader over the characters of a file, a token type that remembers where it came from, and a reader over the resulting tokens. It does not attempt to be a lexer generator — the rules for what makes a token are yours to write — but it takes care of the position tracking and lookahead that every hand written lexer and parser needs.

## Reading Source

[`SourceReader`](API/MrKWatkins.Ast.Lexing/SourceReader/index.md) is a forward-only reader over a [`TextFile`](API/MrKWatkins.Ast.Position/TextFile/index.md) that tracks the current [`Index`](API/MrKWatkins.Ast.Lexing/SourceReader/Index.md), [`LineIndex`](API/MrKWatkins.Ast.Lexing/SourceReader/LineIndex.md) and [`ColumnIndex`](API/MrKWatkins.Ast.Lexing/SourceReader/ColumnIndex.md) as it goes. Lines are terminated by `"\n"`, `"\r\n"` or a lone `"\r"`, matching how [`TextFile`](API/MrKWatkins.Ast.Position/TextFile/index.md) splits its [`Lines`](API/MrKWatkins.Ast.Position/TextFile/Lines.md).

| Member | Description |
| ------ | ----------- |
| [`Current`](API/MrKWatkins.Ast.Lexing/SourceReader/Current.md) | The character at the current position, or `'\0'` at the end of the file. |
| [`Peek`](API/MrKWatkins.Ast.Lexing/SourceReader/Peek.md) | The character at an offset from the current position, forwards or backwards, or `'\0'` outside the file. |
| [`AtEnd`](API/MrKWatkins.Ast.Lexing/SourceReader/AtEnd.md) | Whether the reader has reached the end of the file. |
| [`Advance`](API/MrKWatkins.Ast.Lexing/SourceReader/Advance.md) | Moves on one character, or a given number of them, stopping at the end of the file. |
| [`AdvanceWhile`](API/MrKWatkins.Ast.Lexing/SourceReader/AdvanceWhile.md) | Moves on while a predicate holds, returning the number of characters consumed. |

Because the end of file reads as `'\0'` rather than throwing, a lexer can be written without an `AtEnd` check on every branch.

## Creating Tokens

Mark the start of a token, scan to its end, then create the token from the mark:

```c#
public enum TokenKind { Number, Identifier, Operator, EndOfFile }

private static Token<TokenKind> ReadNumber(SourceReader reader)
{
    var start = reader.Mark();
    reader.AdvanceWhile(char.IsAsciiDigit);
    return reader.CreateToken(TokenKind.Number, start);
}
```

[`Mark`](API/MrKWatkins.Ast.Lexing/SourceReader/Mark.md) captures the current index, line and column as a [`SourceMark`](API/MrKWatkins.Ast.Lexing/SourceMark/index.md). [`CreateToken`](API/MrKWatkins.Ast.Lexing/SourceReader/CreateToken.md) then builds a [`Token<TKind>`](API/MrKWatkins.Ast.Lexing/Token-TKind/index.md) running from that mark up to wherever the reader has got to.

`TKind` is an enum of your own describing the kinds of token in your language. Tokens are readonly record structs holding the kind, the file and the start and length of the token, so lexing a file does not allocate a token object per token. [`Text`](API/MrKWatkins.Ast.Lexing/Token-TKind/Text.md) returns the token's characters as a `ReadOnlySpan<char>` over the file's text rather than as a new string.

Tokens can be constructed directly, but the constructor performs no validation for performance; going through [`CreateToken`](API/MrKWatkins.Ast.Lexing/SourceReader/CreateToken.md) guarantees the index, line and column are consistent with each other.

## Reading Tokens

[`TokenReader<TKind>`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/index.md) hands the tokens to a parser. The last token is expected to be an end of file token, and the reader never advances past it, so [`Current`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/Current.md) is always safe to examine and a parser needs no bounds checks of its own. Constructing a reader with no tokens at all throws, since a lexer should always produce that end of file token.

```c#
var reader = new TokenReader<TokenKind>(tokens);

if (reader.TryConsume(TokenKind.Number, out var number))
{
    // number was consumed and the reader has advanced.
}
```

[`TryConsume`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/TryConsume.md) advances only if the current token is of the requested kind, which is the usual shape of a recursive descent parser. [`Peek`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/Peek.md) looks at a token an offset away, clamped to the ends of the stream, for decisions that need more than one token of lookahead.

For speculative parsing, save [`Position`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/Position.md) before you start and set it back afterwards to rewind:

```c#
var start = reader.Position;
if (!TryParseTypeName(reader, out var type))
{
    reader.Position = start;   // Rewind and try something else.
}
```

## Positions from Tokens

[`Token<TKind>.Position`](API/MrKWatkins.Ast.Lexing/Token-TKind/Position.md) converts a token to a [`TextFilePosition`](API/MrKWatkins.Ast.Position/TextFilePosition/index.md), which can be assigned straight to a node's [`SourcePosition`](API/MrKWatkins.Ast/Node-TNode/SourcePosition.md). For a node built from several tokens, [`PositionFrom`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/PositionFrom.md) spans from a given token up to the last token consumed:

```c#
var start = reader.Current;
var expression = ParseExpression(reader);
expression.SourcePosition = reader.PositionFrom(start);
```

Positions for tokens at places a [`TextFilePosition`](API/MrKWatkins.Ast.Position/TextFilePosition/index.md) cannot represent — a token starting on a line terminator, or a zero length token at the end of the file — are clamped to the nearest valid position rather than throwing, so an end of file token can still carry a usable position for error reporting. Empty files have no positions at all, and asking a token from one for its position throws an `InvalidOperationException`.

## Error Recovery

[`SkipUntil`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/SkipUntil.md) advances until the current token is one of the kinds given, stopping at the end of file token if none is found. It always advances at least one token unless it is already at the end, which makes it safe for panic mode recovery: skipped tokens can never leave the parser looping on the same position. The synchronisation token is left as [`Current`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/Current.md) so the parser decides whether to consume it.

```c#
node.AddError("Expected an expression.");
reader.SkipUntil(TokenKind.Semicolon, TokenKind.CloseBrace);
```

Recording the failure as a [message](messages.md) on a node rather than throwing lets the parser carry on and report every syntax error in the file in one pass.
