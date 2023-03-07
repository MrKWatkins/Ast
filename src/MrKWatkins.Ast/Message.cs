namespace MrKWatkins.Ast;

public sealed record Message
{
    public Message(MessageLevel level, string text)
    {
        Level = level;
        Text = text;
    }
    
    public Message(MessageLevel level, string code, string text)
    {
        Level = level;
        Text = text;
        Code = code;
    }

    [Pure]
    public static Message Error(string code, string text) => new(MessageLevel.Error, code, text);
    
    [Pure]
    public static Message Error(string text) => new(MessageLevel.Error, text);

    [Pure]
    public static Message Warning(string code, string text) => new(MessageLevel.Warning, code, text);
    
    [Pure]
    public static Message Warning(string text) => new(MessageLevel.Warning, text);

    [Pure]
    public static Message Info(string code, string text) => new(MessageLevel.Info, code, text);
    
    [Pure]
    public static Message Info(string text) => new(MessageLevel.Info, text);

    public MessageLevel Level { get; }
    
    public string? Code { get; }
    
    public string Text { get; }

    public override string ToString() =>
        Code != null
            ? $"{Level} {Code}: {Text}"
            : $"{Level}: {Text}";
}