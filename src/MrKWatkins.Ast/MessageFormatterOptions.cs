namespace MrKWatkins.Ast;

/// <summary>
/// Options for the <see cref="MessageFormatter" />.
/// </summary>
public sealed class MessageFormatterOptions
{
    /// <summary>
    /// <see cref="MessageFormatterOptions" /> with <see cref="PrefixWithSourcePosition" /> = <c>false</c> and <see cref="HighlightSourcePosition" /> = <c>false</c>.
    /// </summary>
    public static MessageFormatterOptions Default { get; } = new();

    /// <summary>
    /// <see cref="MessageFormatterOptions" /> with <see cref="PrefixWithSourcePosition" /> = <c>true</c> and <see cref="HighlightSourcePosition" /> = <c>false</c>.
    /// </summary>
    public static MessageFormatterOptions PrefixOnly { get; } = new() { PrefixWithSourcePosition = true };

    /// <summary>
    /// <see cref="MessageFormatterOptions" /> with <see cref="PrefixWithSourcePosition" /> = <c>false</c> and <see cref="HighlightSourcePosition" /> = <c>true</c>.
    /// </summary>
    public static MessageFormatterOptions HighlightOnly { get; } = new() { HighlightSourcePosition = true };

    /// <summary>
    /// <see cref="MessageFormatterOptions" /> with <see cref="PrefixWithSourcePosition" /> = <c>true</c> and <see cref="HighlightSourcePosition" /> = <c>true</c>.
    /// </summary>
    public static MessageFormatterOptions PrefixAndHighlight { get; } = new() { PrefixWithSourcePosition = true, HighlightSourcePosition = true };

    /// <summary>
    /// Whether to prefix the message with the source position or not.
    /// </summary>
    public bool PrefixWithSourcePosition { get; init; }

    /// <summary>
    /// Whether to suffix the message with a highlight of the message in its line.
    /// </summary>
    public bool HighlightSourcePosition { get; init; }
}