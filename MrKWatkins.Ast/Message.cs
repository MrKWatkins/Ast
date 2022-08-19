namespace MrKWatkins.Ast;

public sealed record Message
{
    internal Message(MessageLevel level, string text, string? code = null)
    {
        Level = level;
        Text = text;
        Code = code;
    }

    public MessageLevel Level { get; }
    
    public string? Code { get; }
    
    public string Text { get; }

    public override string ToString() =>
        Code != null
            ? $"[{Level}] {Code}: {Text}"
            : $"[{Level}] {Text}";
}