namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// A constant <c>true</c> or <c>false</c> value.
/// </summary>
public sealed class Constant : Expression
{
    public Constant(bool value)
    {
        Value = value;
    }

    public bool Value
    {
        get => Properties.GetOrThrow<bool>(nameof(Value));
        init => Properties.Set(nameof(Value), value);
    }
}