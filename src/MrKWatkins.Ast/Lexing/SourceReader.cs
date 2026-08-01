using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Lexing;

/// <summary>
/// A forward-only reader over the characters of a <see cref="TextFile" /> that tracks the current line and column, for use when
/// lexing the file into <see cref="Token{TKind}">Tokens</see>. Lines are terminated by <c>"\n"</c>, <c>"\r\n"</c> or a lone
/// <c>"\r"</c>, matching <see cref="TextFile.Lines" />.
/// </summary>
public sealed class SourceReader
{
    private readonly string text;

    /// <summary>
    /// Initialises a new instance of the <see cref="SourceReader" /> class to read the specified <see cref="TextFile" />.
    /// </summary>
    /// <param name="file">The file to read.</param>
    public SourceReader(TextFile file)
    {
        File = file;
        text = file.Text;
    }

    /// <summary>
    /// The <see cref="TextFile" /> being read.
    /// </summary>
    /// <returns>The file.</returns>
    public TextFile File { get; }

    /// <summary>
    /// The index of the current position in the file.
    /// </summary>
    /// <returns>The index.</returns>
    public int Index { get; private set; }

    /// <summary>
    /// Zero based index of the line of the current position in the file.
    /// </summary>
    /// <returns>The index of the line.</returns>
    public int LineIndex { get; private set; }

    /// <summary>
    /// Zero based index of the column of the current position in the file.
    /// </summary>
    /// <returns>The index of the column.</returns>
    public int ColumnIndex { get; private set; }

    /// <summary>
    /// Returns <c>true</c> if the reader is at the end of the file, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether the reader is at the end of the file or not.</returns>
    public bool AtEnd => Index == text.Length;

    /// <summary>
    /// The character at the current position, or the NUL character <c>'\0'</c> if the reader is at the end of the file.
    /// </summary>
    /// <returns>The current character.</returns>
    public char Current => Index < text.Length ? text[Index] : '\0';

    /// <summary>
    /// Returns the character at the specified offset from the current position, or the NUL character <c>'\0'</c> if the offset is
    /// outside the file.
    /// </summary>
    /// <param name="offset">The offset from the current position; can be negative to peek at preceding characters.</param>
    /// <returns>The character at the offset.</returns>
    [Pure]
    public char Peek(int offset)
    {
        var index = Index + offset;
        return index >= 0 && index < text.Length ? text[index] : '\0';
    }

    /// <summary>
    /// Advances the reader one character. Does nothing if the reader is at the end of the file.
    /// </summary>
    public void Advance()
    {
        if (Index == text.Length)
        {
            return;
        }

        var current = text[Index];
        Index++;
        if (current == '\n' || current == '\r' && (Index == text.Length || text[Index] != '\n'))
        {
            LineIndex++;
            ColumnIndex = 0;
        }
        else
        {
            ColumnIndex++;
        }
    }

    /// <summary>
    /// Advances the reader the specified number of characters, stopping at the end of the file if it is reached first.
    /// </summary>
    /// <param name="count">The number of characters to advance.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count" /> is less than 0.</exception>
    public void Advance(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Value must be greater than or equal to 0.");
        }

        for (var f = 0; f < count; f++)
        {
            Advance();
        }
    }

    /// <summary>
    /// Advances the reader while the specified predicate returns <c>true</c> for the current character, stopping at the end of the
    /// file if it is reached first.
    /// </summary>
    /// <param name="predicate">The predicate to test characters with.</param>
    /// <returns>The number of characters advanced.</returns>
    public int AdvanceWhile([InstantHandle] Func<char, bool> predicate)
    {
        var start = Index;
        while (Index < text.Length && predicate(text[Index]))
        {
            Advance();
        }

        return Index - start;
    }

    /// <summary>
    /// Creates a <see cref="SourceMark" /> for the current position, typically to mark the start of a token before scanning to its
    /// end.
    /// </summary>
    /// <returns>A <see cref="SourceMark" /> for the current position.</returns>
    [Pure]
    public SourceMark Mark() => new(Index, LineIndex, ColumnIndex);

    /// <summary>
    /// Creates a <see cref="Token{TKind}" /> of the specified kind from the specified <see cref="SourceMark" /> up to the current
    /// position.
    /// </summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="start">The start of the token.</param>
    /// <typeparam name="TKind">The type of the enum that identifies the different kinds of token.</typeparam>
    /// <returns>A new <see cref="Token{TKind}" /> instance.</returns>
    [Pure]
    public Token<TKind> CreateToken<TKind>(TKind kind, SourceMark start)
        where TKind : struct, Enum =>
        new(kind, File, start.Index, Index - start.Index, start.LineIndex, start.ColumnIndex);

    /// <summary>
    /// Creates a <see cref="Token{TKind}" /> of the specified kind and length starting at the current position, advancing the
    /// reader over it. Stops at the end of the file if it is reached first, shortening the token accordingly.
    /// </summary>
    /// <param name="kind">The kind of the token.</param>
    /// <param name="length">The length of the token.</param>
    /// <typeparam name="TKind">The type of the enum that identifies the different kinds of token.</typeparam>
    /// <returns>A new <see cref="Token{TKind}" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length" /> is less than 0.</exception>
    [MustUseReturnValue]
    public Token<TKind> ReadToken<TKind>(TKind kind, int length)
        where TKind : struct, Enum
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Value must be greater than or equal to 0.");
        }

        var start = Mark();
        Advance(length);
        return CreateToken(kind, start);
    }
}