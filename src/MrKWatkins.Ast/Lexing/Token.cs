using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Lexing;

/// <summary>
/// A token produced by lexing a <see cref="TextFile" />.
/// </summary>
/// <typeparam name="TKind">The type of the enum that identifies the different kinds of token.</typeparam>
public readonly record struct Token<TKind>
    where TKind : struct, Enum
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Token{TKind}" /> struct. No validation is performed for performance; use
    /// <see cref="SourceReader.CreateToken{TKind}" /> to create tokens with guaranteed consistent values.
    /// </summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="file">The <see cref="TextFile" /> the token comes from.</param>
    /// <param name="startIndex">The index of the start of the token in <paramref name="file" />.</param>
    /// <param name="length">The length of the token.</param>
    /// <param name="startLineIndex">Zero based index of the start line of the token in <paramref name="file" />.</param>
    /// <param name="startColumnIndex">Zero based index of the start column of the token in <paramref name="file" />.</param>
    public Token(TKind kind, TextFile file, int startIndex, int length, int startLineIndex, int startColumnIndex)
    {
        Kind = kind;
        File = file;
        StartIndex = startIndex;
        Length = length;
        StartLineIndex = startLineIndex;
        StartColumnIndex = startColumnIndex;
    }

    /// <summary>
    /// The kind of the token.
    /// </summary>
    /// <returns>The kind.</returns>
    public TKind Kind { get; }

    /// <summary>
    /// The <see cref="TextFile" /> the token comes from.
    /// </summary>
    /// <returns>The file.</returns>
    public TextFile File { get; }

    /// <summary>
    /// The index of the start of the token in <see cref="File" />.
    /// </summary>
    /// <returns>The start index.</returns>
    public int StartIndex { get; }

    /// <summary>
    /// The length of the token.
    /// </summary>
    /// <returns>The length.</returns>
    public int Length { get; }

    /// <summary>
    /// Zero based index of the start line of the token in <see cref="File" />.
    /// </summary>
    /// <returns>The index of the start line.</returns>
    public int StartLineIndex { get; }

    /// <summary>
    /// Zero based index of the start column of the token in <see cref="File" />.
    /// </summary>
    /// <returns>The index of the start column.</returns>
    public int StartColumnIndex { get; }

    /// <summary>
    /// The text of the token.
    /// </summary>
    /// <returns>The text.</returns>
    public ReadOnlySpan<char> Text => File.Text.AsSpan(StartIndex, Length);

    /// <summary>
    /// The <see cref="TextFilePosition" /> of the token in <see cref="File" />. The start index, line and column are clamped to valid
    /// positions in the file for tokens at positions a <see cref="TextFilePosition" /> cannot represent, i.e. a token starting on a
    /// line's terminator or a zero length token at the end of the file.
    /// </summary>
    /// <returns>The position.</returns>
    /// <exception cref="InvalidOperationException">If <see cref="File" /> is empty; empty files have no positions.</exception>
    public TextFilePosition Position
    {
        get
        {
            var lines = File.Lines;
            if (lines.Count == 0)
            {
                throw new InvalidOperationException("The file is empty so the token does not have a position.");
            }

            var startIndex = StartIndex < File.Length ? StartIndex : File.Length - 1;

            var lineIndex = StartLineIndex;
            var columnIndex = StartColumnIndex;
            if (lineIndex >= lines.Count)
            {
                lineIndex = lines.Count - 1;
                columnIndex = lines[lineIndex].Length;
            }

            var lineLength = lines[lineIndex].Length;
            if (columnIndex >= lineLength)
            {
                columnIndex = lineLength > 0 ? lineLength - 1 : 0;
            }

            return File.CreatePosition(startIndex, Length, lineIndex, columnIndex);
        }
    }

    /// <summary>
    /// Returns <c>true</c> if this token ends exactly where <paramref name="other" /> starts, i.e. the tokens are directly adjacent
    /// in the file with nothing between them. Both tokens are assumed to come from the same file.
    /// </summary>
    /// <param name="other">The other token.</param>
    /// <returns><c>true</c> if this token is directly before <paramref name="other" />, <c>false</c> otherwise.</returns>
    [Pure]
    public bool IsDirectlyBefore(in Token<TKind> other) => StartIndex + Length == other.StartIndex;

    /// <summary>
    /// Returns a string representation of this token, i.e. the <see cref="Kind" /> followed by the <see cref="Text" /> if the token
    /// has any.
    /// </summary>
    /// <returns>A string representation of this token.</returns>
    public override string ToString() =>
        Length > 0
            ? $"{Kind} \"{File.Text.Substring(StartIndex, Length)}\""
            : $"{Kind}";
}