namespace MrKWatkins.Ast;

/// <summary>
/// An error, warning or informational message for a node.
/// </summary>
public sealed record Message
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the specified <see cref="Level"/> and <see cref="Text"/>.
    /// </summary>
    /// <param name="level">The <see cref="Level"/> of the message.</param>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    public Message(MessageLevel level, string text)
    {
        Level = level;
        Text = text;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the specified <see cref="Level"/>, <see cref="Code"/> and <see cref="Text"/>.
    /// </summary>
    /// <param name="level">The <see cref="Level"/> of the message.</param>
    /// <param name="code">The <see cref="Code"/> of the message.</param>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    public Message(MessageLevel level, string code, string text)
    {
        Level = level;
        Text = text;
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the <see cref="Level"/> <see cref="MessageLevel.Error"/>
    /// and specified <see cref="Code"/> and <see cref="Text"/>.
    /// </summary>
    /// <param name="code">The <see cref="Code"/> of the message.</param>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    [Pure]
    public static Message Error(string code, string text) => new(MessageLevel.Error, code, text);

    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the <see cref="Level"/> <see cref="MessageLevel.Error"/>
    /// and specified <see cref="Text"/>.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    [Pure]
    public static Message Error(string text) => new(MessageLevel.Error, text);

    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the <see cref="Level"/> <see cref="MessageLevel.Warning"/>
    /// and specified <see cref="Code"/> and <see cref="Text"/>.
    /// </summary>
    /// <param name="code">The <see cref="Code"/> of the message.</param>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    [Pure]
    public static Message Warning(string code, string text) => new(MessageLevel.Warning, code, text);

    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the <see cref="Level"/> <see cref="MessageLevel.Warning"/>
    /// and specified <see cref="Text"/>.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    [Pure]
    public static Message Warning(string text) => new(MessageLevel.Warning, text);

    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the <see cref="Level"/> <see cref="MessageLevel.Info"/>
    /// and specified <see cref="Code"/> and <see cref="Text"/>.
    /// </summary>
    /// <param name="code">The <see cref="Code"/> of the message.</param>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    [Pure]
    public static Message Info(string code, string text) => new(MessageLevel.Info, code, text);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Message" /> class with the <see cref="Level"/> <see cref="MessageLevel.Info"/>
    /// and specified <see cref="Text"/>.
    /// </summary>
    /// <param name="text">The <see cref="Text"/> of the message.</param>
    [Pure]
    public static Message Info(string text) => new(MessageLevel.Info, text);

    /// <summary>
    /// The <see cref="MessageLevel">level</see> of the message.
    /// </summary>
    public MessageLevel Level { get; }
    
    /// <summary>
    /// Optional code for the message.
    /// </summary>
    public string? Code { get; }
    
    /// <summary>
    /// The text of the message.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Returns a string representation of this message.
    /// </summary>
    /// <returns>The <see cref="Level"/> and <see cref="Code"/> (if set) followed by the <see cref="Text"/>.</returns>
    public override string ToString() =>
        Code != null
            ? $"{Level} {Code}: {Text}"
            : $"{Level}: {Text}";
}