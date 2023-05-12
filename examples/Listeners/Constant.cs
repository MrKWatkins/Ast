namespace MrKWatkins.Ast.Examples.Listeners;

/// <summary>
/// A constant value.
/// </summary>
public sealed class Constant : Expression
{
    public Constant(int value)
    {
        Value = value;
    }

    public int Value
    {
        get => Properties.GetOrThrow<int>(nameof(Value));
        init => Properties.Set(nameof(Value), value);
    }
}