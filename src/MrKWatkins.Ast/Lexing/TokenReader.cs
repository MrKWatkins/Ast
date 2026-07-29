using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Lexing;

/// <summary>
/// A reader over the <see cref="Token{TKind}">Tokens</see> lexed from a file, for consumption by a parser. The last token should be
/// an end of file token; the reader never advances beyond it, so a parser can always safely examine <see cref="Current" />.
/// </summary>
/// <typeparam name="TKind">The type of the enum that identifies the different kinds of token.</typeparam>
public sealed class TokenReader<TKind>
    where TKind : struct, Enum
{
    private readonly Token<TKind>[] tokens;
    private int position;

    /// <summary>
    /// Initialises a new instance of the <see cref="TokenReader{TKind}" /> class with the specified tokens.
    /// </summary>
    /// <param name="tokens">The tokens.</param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="tokens" /> is empty; a lexer should always produce an end of file token.
    /// </exception>
    public TokenReader([InstantHandle] IEnumerable<Token<TKind>> tokens)
    {
        this.tokens = tokens.ToArray();
        if (this.tokens.Length == 0)
        {
            throw new ArgumentException("Value must contain at least one token; a lexer should always produce an end of file token.", nameof(tokens));
        }
    }

    /// <summary>
    /// The number of tokens in the reader.
    /// </summary>
    /// <returns>The number of tokens.</returns>
    public int Count => tokens.Length;

    /// <summary>
    /// The index of the current position in the reader. Can be set to a previously retrieved value to rewind the reader, e.g. after
    /// speculative lookahead.
    /// </summary>
    /// <returns>The index of the current position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">On set if the value is less than 0 or not less than <see cref="Count" />.</exception>
    public int Position
    {
        get => position;
        set
        {
            if (value < 0 || value >= tokens.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be in the range 0 to {tokens.Length - 1}.");
            }

            position = value;
        }
    }

    /// <summary>
    /// Returns <c>true</c> if the reader is positioned at the last token, i.e. the end of file token, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether the reader is positioned at the last token or not.</returns>
    public bool AtEnd => position == tokens.Length - 1;

    /// <summary>
    /// The token at the current position.
    /// </summary>
    /// <returns>The current token.</returns>
    public Token<TKind> Current => tokens[position];

    /// <summary>
    /// Returns the token at the specified offset from the current position, clamped to the ends of the reader, i.e. offsets before
    /// the first token return the first token and offsets after the last token return the last token.
    /// </summary>
    /// <param name="offset">The offset from the current position; can be negative to peek at preceding tokens.</param>
    /// <returns>The token at the offset.</returns>
    [Pure]
    public Token<TKind> Peek(int offset)
    {
        var index = position + offset;
        if (index < 0)
        {
            return tokens[0];
        }

        return index < tokens.Length ? tokens[index] : tokens[^1];
    }

    /// <summary>
    /// Advances the reader one token. Does nothing if the reader is positioned at the last token.
    /// </summary>
    public void Advance()
    {
        if (position < tokens.Length - 1)
        {
            position++;
        }
    }

    /// <summary>
    /// If the token at the current position is of the specified kind then it is returned and the reader advanced, otherwise the
    /// stream does not move.
    /// </summary>
    /// <param name="kind">The kind of token to consume.</param>
    /// <param name="token">The consumed token if it was of the specified kind, the default token value otherwise.</param>
    /// <returns><c>true</c> if a token was consumed, <c>false</c> otherwise.</returns>
    public bool TryConsume(TKind kind, out Token<TKind> token)
    {
        if (EqualityComparer<TKind>.Default.Equals(tokens[position].Kind, kind))
        {
            token = tokens[position];
            Advance();
            return true;
        }

        token = default;
        return false;
    }

    /// <summary>
    /// If the token at the current position is of the specified kind then the reader advances, otherwise the reader does not move.
    /// </summary>
    /// <param name="kind">The kind of token to consume.</param>
    /// <returns><c>true</c> if a token was consumed, <c>false</c> otherwise.</returns>
    public bool TryConsume(TKind kind) => TryConsume(kind, out _);

    /// <summary>
    /// Creates a <see cref="TextFilePosition" /> spanning from the specified token up to and including the last token consumed.
    /// Typically used to set the position of a node from the token that started it once parsing of the node has finished.
    /// </summary>
    /// <param name="start">The token the position starts at.</param>
    /// <returns>A <see cref="TextFilePosition" /> spanning from <paramref name="start" /> to the last token consumed.</returns>
    [Pure]
    public TextFilePosition PositionFrom(Token<TKind> start) => start.Position + Peek(-1).Position;

    /// <summary>
    /// Advances the reader until the current token is one of the specified kinds, stopping at the last token if none is found. Always
    /// advances at least one token unless already positioned at the last, making it suitable for panic mode error recovery: skipped
    /// tokens can never cause an infinite loop, and the synchronisation token is left as <see cref="Current" /> for the parser to
    /// decide whether to consume.
    /// </summary>
    /// <param name="kinds">The kinds of token to stop at.</param>
    public void SkipUntil(params ReadOnlySpan<TKind> kinds)
    {
        do
        {
            Advance();
        }
        while (!AtEnd && !Contains(kinds, tokens[position].Kind));
    }

    [Pure]
    private static bool Contains(ReadOnlySpan<TKind> kinds, TKind kind)
    {
        foreach (var candidate in kinds)
        {
            if (EqualityComparer<TKind>.Default.Equals(candidate, kind))
            {
                return true;
            }
        }

        return false;
    }
}